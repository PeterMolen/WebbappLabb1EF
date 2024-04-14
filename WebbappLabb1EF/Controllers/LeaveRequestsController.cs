using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebbappLabb1EF.Data;
using WebbappLabb1EF.Models;
using WebbappLabb1EF.Utility;

namespace WebbappLabb1EF.Controllers
{
    [Authorize(Roles = $"{SD.Role_Admin},{SD.Role_Employee}")]
    public class LeaveRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveRequests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeaveRequests.Include(l => l.Employee).Include(l => l.Leave);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests
                .Include(l => l.Employee)
                .Include(l => l.Leave)
                .FirstOrDefaultAsync(m => m.LeaveRequestId == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        // GET: LeaveRequests/Create
        public IActionResult Create()
        {
            ViewData["FkEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name");
            ViewData["FkLeaveId"] = new SelectList(_context.Leaves, "LeaveId", "TypeOfLeave");
            return View();
        }

        // POST: LeaveRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveRequestId,StartDate,EndDate,Note,CreatedAt,FkEmployeeId,FkLeaveId")] LeaveRequest leaveRequest)
        {
            if (ModelState.IsValid)
            {
                leaveRequest.CreatedAt = DateTime.Now;
                _context.Add(leaveRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", leaveRequest.FkEmployeeId);
            ViewData["FkLeaveId"] = new SelectList(_context.Leaves, "LeaveId", "TypeOfLeave", leaveRequest.FkLeaveId);
            return View(leaveRequest);
        }

        // GET: LeaveRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            ViewData["FkEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", leaveRequest.FkEmployeeId);
            ViewData["FkLeaveId"] = new SelectList(_context.Leaves, "LeaveId", "TypeOfLeave", leaveRequest.FkLeaveId);
            return View(leaveRequest);
        }

        // POST: LeaveRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveRequestId,StartDate,EndDate,Note,CreatedAt,FkEmployeeId,FkLeaveId")] LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.LeaveRequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    leaveRequest.CreatedAt = DateTime.Now;
                    _context.Update(leaveRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveRequestExists(leaveRequest.LeaveRequestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkEmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", leaveRequest.FkEmployeeId);
            ViewData["FkLeaveId"] = new SelectList(_context.Leaves, "LeaveId", "TypeOfLeave", leaveRequest.FkLeaveId);
            return View(leaveRequest);
        }

        // GET: LeaveRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests
                .Include(l => l.Employee)
                .Include(l => l.Leave)
                .FirstOrDefaultAsync(m => m.LeaveRequestId == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        // POST: LeaveRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest != null)
            {
                _context.LeaveRequests.Remove(leaveRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveRequestExists(int id)
        {
            return _context.LeaveRequests.Any(e => e.LeaveRequestId == id);
        }


        //readonlyIndex
        public IActionResult ReadOnlyIndex()
        {
            
            var model = _context.LeaveRequests
                .Include(lr => lr.Employee)
                .Include(lr => lr.Leave)
                .ToList();

          
            return View("ReadOnlyIndex", model);
        }
    }
}
