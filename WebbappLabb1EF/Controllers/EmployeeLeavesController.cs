using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebbappLabb1EF.Data;
using WebbappLabb1EF.Models;
using WebbappLabb1EF.Utility;

namespace WebbappLabb1EF.Controllers
{
    [Authorize(Roles = $"{SD.Role_Admin},{SD.Role_Employee}")]
    public class EmployeeLeavesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeLeavesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //LeaveId
        //public async Task<IActionResult> Index(int? leaveId)
        //{
        //    var leaves = await _context.Leaves.ToListAsync();

        //    var query = from lr in _context.LeaveRequests
        //                join e in _context.Employees on lr.FkEmployeeId equals e.EmployeeId
        //                join l in _context.Leaves on lr.FkLeaveId equals l.LeaveId
        //                select new { lr, e, l };

        //    if (leaveId.HasValue)
        //    {
        //        query = query.Where(x => x.l.LeaveId == leaveId.Value);
        //    }

        //    var employees = await query
        //        .Select(x => new EmployeeWithLeave
        //        {
        //            Name = x.e.Name,
        //            TypeOfLeave = x.l.TypeOfLeave
        //        }).ToListAsync();

        //    var viewModel = new LeaveEmployeeVM
        //    {
        //        Leaves = leaves,
        //        Employees = employees
        //    };

        //    return View(viewModel);
        //}

        //EmployeeName:
        public async Task<IActionResult> Index(string employeeName)
        {
            var leaves = await _context.Leaves.ToListAsync();

            var groupedLeaveRequests = await (
                from lr in _context.LeaveRequests
                join e in _context.Employees on lr.FkEmployeeId equals e.EmployeeId
                join l in _context.Leaves on lr.FkLeaveId equals l.LeaveId
                where string.IsNullOrEmpty(employeeName) || e.Name == employeeName
                group new { lr, e, l } by e.Name into grouped
                select new
                {
                    EmployeeName = grouped.Key,
                    LeaveTypes = string.Join(", ", grouped.Select(x => x.l.TypeOfLeave))
                }
            ).ToListAsync();

            var employees = groupedLeaveRequests.Select(group => new EmployeeWithLeave
            {
                Name = group.EmployeeName,
                TypeOfLeave = group.LeaveTypes
            }).ToList();

            var viewModel = new LeaveEmployeeVM
            {
                Leaves = leaves,
                Employees = employees
            };

            return View(viewModel);
        }
    }
}
