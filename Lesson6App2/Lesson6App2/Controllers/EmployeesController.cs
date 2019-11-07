using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lesson6App2.Models;

namespace Lesson6App2.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly PersonDBContext _context;

        public EmployeesController(PersonDBContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            //  var personDBContext = _context.Employees.Include(e => e.Deparment).Include(e => e.Job);
            var personDBContext = from emp in _context.Employees
                                    join job in _context.Jobs on emp.JobId equals job.JobId
                                    select new Employees
                                    {
                                        PersonelId = emp.PersonelId,
                                        FirstName = emp.FirstName,
                                        LastName = emp.LastName,
                                        Salary = emp.Salary,
                                        JobId = emp.JobId,
                                        Job = new Jobs
                                        {
                                            JobId = job.JobId,
                                            JobTitle = job.JobTitle
                                        }
                                    };

            return View(await personDBContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.Deparment)
                .Include(e => e.Job)
                .SingleOrDefaultAsync(m => m.PersonelId == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DeparmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId");
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonelId,FirstName,LastName,Salary,JobId,DeparmentId")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeparmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", employees.DeparmentId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", employees.JobId);
            return View(employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var employees = await _context.Employees.SingleOrDefaultAsync(m => m.PersonelId == id);
            if (employees == null)
            {
                return NotFound();
            }

           

          

            ViewData["DeparmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", employees.DeparmentId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", employees.JobId);
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonelId,FirstName,LastName,Salary,JobId,DeparmentId")] Employees employees)
        {
            if (id != employees.PersonelId)
            {
                return NotFound();
            }

            var deptId = _context.Employees
              .Where(x => x.PersonelId == id)
              .Select(x => x.DeparmentId)
              .FirstOrDefault();

            

            if (employees.DeparmentId != deptId)
            {
                DepartmentHistory deptHist = new DepartmentHistory();
                deptHist.DepartmentName = deptId+"";
                _context.DepartmentHistory.Add(deptHist);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.PersonelId))
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
            ViewData["DeparmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", employees.DeparmentId);
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", employees.JobId);
            return View(employees);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(e => e.Deparment)
                .Include(e => e.Job)
                .SingleOrDefaultAsync(m => m.PersonelId == id);
            if (employees == null)
            {
                return NotFound();
            }

            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employees = await _context.Employees.SingleOrDefaultAsync(m => m.PersonelId == id);
            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesExists(int id)
        {
            return _context.Employees.Any(e => e.PersonelId == id);
        }
    }
}
