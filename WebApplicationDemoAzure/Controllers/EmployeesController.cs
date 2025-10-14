using Microsoft.AspNetCore.Mvc;
using WebApplicationDemoAzure.Models;
using WebApplicationDemoAzure.Repositories;

namespace WebApplicationDemoAzure.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserMaster model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input");

            // Dummy validation logic
            bool userDetails = await _employeeRepository.GetUserMasterDetailsAsync(model.Email_Id, model.Password);
            if(userDetails)
            {
                // Normally, set up auth cookies or session
                return Ok(new { success = true, message = "Login successful" });
            }
            return Unauthorized(new { success = false, message = "Invalid credentials" });
        }


        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return Json(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeRepository.AddAsync(employee);
                return Json(new { success = true, data = result });
            }
            return Json(new { success = false });
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeRepository.UpdateAsync(employee);
                return Json(new { success = true, data = result });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _employeeRepository.DeleteAsync(id);
            return Json(new { success });
        }
    }
}