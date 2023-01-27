using EmployeeApi.Models.Models;
using EmployeeApi.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace EmpolyeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) 
        {
            _employeeService= employeeService;
        }


        /// <summary>
        /// Gets Employees 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Employee> GetEmployees(int page, int size) 
        {
            return _employeeService.GetEmployees(page,size);
        }
    }
}
