using CascadingDemo.Data;
using CascadingDemo.Models;
using CascadingDemo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CascadingDemo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDBContext _context;

        public EmployeesController(EmployeeDBContext context)
        {
            _context = context;
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            var viewModel = new EmployeeCreateViewModel
            {
                Countries = new SelectList(_context.Countries.AsNoTracking().ToList(), "CountryId", "CountryName")
            };

            return View(viewModel);
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to the domain model
                var employee = new Employee
                {
                    FullName = viewModel.FullName,
                    Email = viewModel.Email,
                    Phone = viewModel.Phone,
                    Department = viewModel.Department,
                    CountryId = Convert.ToInt32(viewModel.CountryId),
                    StateId = Convert.ToInt32(viewModel.StateId),
                    CityId = Convert.ToInt32(viewModel.CityId)
                };

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate the Countries dropdown on error
            viewModel.Countries = new SelectList(_context.Countries.AsNoTracking().ToList(), "CountryId", "CountryName", viewModel.CountryId);
            return View(viewModel);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            // Map domain model to view model and preload dropdowns
            var viewModel = new EmployeeEditViewModel
            {
                EmployeeId = employee.EmployeeId,
                FullName = employee.FullName,
                Email = employee.Email,
                Phone = employee.Phone,
                Department = employee.Department,
                CountryId = employee.CountryId,
                StateId = employee.StateId,
                CityId = employee.CityId,
                Countries = new SelectList(_context.Countries.AsNoTracking().ToList(), "CountryId", "CountryName", employee.CountryId)
                // States and Cities will be loaded via AJAX in the view for prepopulation
            };

            return View(viewModel);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate Countries on error
                viewModel.Countries = new SelectList(
                    _context.Countries.AsNoTracking().ToList(),
                    "CountryId",
                    "CountryName",
                    viewModel.CountryId
                );
                return View(viewModel);
            }

            var employee = await _context.Employees.FindAsync(viewModel.EmployeeId);
            if (employee == null)
            {
                return NotFound();
            }

            // Map view model values to the domain model
            employee.FullName = viewModel.FullName;
            employee.Email = viewModel.Email;
            employee.Phone = viewModel.Phone;
            employee.Department = viewModel.Department;
            employee.CountryId = viewModel.CountryId!.Value;
            employee.StateId = viewModel.StateId!.Value;
            employee.CityId = viewModel.CityId!.Value;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Employees/Index
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees
                .Include(e => e.Country)
                .Include(e => e.State)
                .Include(e => e.City)
                .AsNoTracking()
                .ToListAsync();
            return View(employees);
        }

        // JSON endpoint: Get States for a Country
        [HttpGet]
        public IActionResult GetStates(int countryId)
        {
            var states = _context.States.AsNoTracking().Where(s => s.CountryId == countryId).ToList();
            return Json(new SelectList(states, "StateId", "StateName"));
        }

        // JSON endpoint: Get Cities for a State
        [HttpGet]
        public IActionResult GetCities(int stateId)
        {
            var cities = _context.Cities.AsNoTracking().Where(c => c.StateId == stateId).ToList();
            return Json(new SelectList(cities, "CityId", "CityName"));
        }
    }
}