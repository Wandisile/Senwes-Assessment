using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SenwesAssignment_API.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SenwesAssignment_API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger,
            IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>Returns a list of all employees</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var employeeData = await _employeeService.GetAsync();
                return Ok(employeeData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
         
        }

        /// <summary>
        /// Get all employees that joined the company in the last 5 years
        /// </summary>
        [Route("RecentlyJoined")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRecentlyJoined()
        {
            try
            {
                var employeeData = await _employeeService.GetAsyncByJoiningDate();
                return Ok(employeeData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Get employee by employee id
        /// </summary>
        [Route("Get/{empId}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetByEmployeeId(int empId)
        {
            try
            {
                var employee = await _employeeService.GetAsyncById(empId);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all employees older than 30
        /// </summary>
        [Route("OlderThanThirty")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetByAge()
        {
            try
            {
                var employee = await _employeeService.GetAsyncByAge();
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get the top 10 highest paid males/females
        /// </summary>
        [Route("HighestPaid")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetByHighestPaid()
        {
            try
            {
                var employee = await _employeeService.GetAsyncHighestPaid();
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Search for an employee to return anyone with the specific name or surname and city
        /// </summary>
        [Route("Search/{name}/{surname}/{city}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SearchEmployee(string city, string name, string surname)
        {
            try
            {
                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(surname))
                {
                    return BadRequest("Either name or surname is required.");
                }

                var employee = await _employeeService.SearchEmployee(name, surname, city);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all employees with a first name of “Treasure” and then return their salary
        /// </summary>
        [Route("Name/Treasure")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetByFirstName()
        {
            try
            {
                var employee = await _employeeService.GetAsyncByFirstName();
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get All City names only
        /// </summary>
        [Route("Cities")]
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            try
            {
                var employee = await _employeeService.GetAsyncCities();
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
