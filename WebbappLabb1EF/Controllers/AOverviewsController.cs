using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebbappLabb1EF.Data;
using WebbappLabb1EF.Models;
using WebbappLabb1EF.Utility;

namespace WebbappLabb1EF.Controllers
{
    [Authorize(Roles = $"{SD.Role_Admin}")]
    public class AOverviewsController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        public AOverviewsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var overviews = await (
                from lr in _context.LeaveRequests
                join e in _context.Employees on lr.FkEmployeeId equals e.EmployeeId
                join l in _context.Leaves on lr.FkLeaveId equals l.LeaveId
                group new { lr, e, l } by new { lr.CreatedAt.Month, e.Name } into grouped
                select new AdminOverview
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(grouped.Key.Month),
                    Name = grouped.Key.Name,
                    TypeOfLeave = string.Join(", ", grouped.Select(x => x.l.TypeOfLeave)),
                    StartDate = grouped.Min(x => x.lr.StartDate),
                    EndDate = grouped.Max(x => x.lr.EndDate),
                    CreatedAt = grouped.Min(x => x.lr.CreatedAt)
                }
            ).ToListAsync();

            var months = overviews.Select(x => x.Month).Distinct().OrderBy(x => DateTime.ParseExact(x, "MMMM", CultureInfo.CurrentCulture));

            var viewModel = new AdminOverviewVM
            {
                Months = months,
                Overviews = overviews
            };

            return View(viewModel);
        }
    }
}
