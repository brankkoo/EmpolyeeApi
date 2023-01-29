using EmployeeApi.Models.Base;
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
        [Route("get-all-employees")]
        public async Task<List<Employee>> GetEmployees([FromQuery]int page, [FromQuery]int size) 
        {
            if (page > 0)
                return await _employeeService.GetEmployees(page, size).ToListAsync();
            else
                throw new Exception("Page number must be higher than zero");
        }
        
        [HttpGet]
        [Route("get-employees-by-ids")]
        public async Task<List<(Employee employee,string url)>> GetEmployeesByIds([FromQuery]Guid[] ids) 
        {
            List<Employee> employees;
            (Employee,string) employeeIdsHateOas;
            List<(Employee, string)> hateOas = new List<(Employee, string)>();

            if (ids.Length > 0)
            {
                employees = await _employeeService.GetByIds(ids).ToListAsync();
                foreach (var employee in employees)
                {
                    employeeIdsHateOas.Item1 = employee;
                    employeeIdsHateOas.Item2 = $"Employee/get-converted-pay?employeeId={employee.EmployeeId}";
                    hateOas.Add(employeeIdsHateOas);
                }
                return hateOas;
            }
            else
                throw new Exception("You must enter at least one Id");
        }

        [HttpPost]
        [Route("Insert-Employee")]
        public Employee InsertEmployee(string name,  string lastName, string Adress,  float pay) 
        {
            return  _employeeService.InsertEmployee(name, lastName, Adress, pay);
        }

        [HttpGet]
        [Route("get-converted-pay")]
        public async Task<List<IPay>> GetConvrtedPays([FromQuery]Guid employeeId) 
        {
          return await _employeeService.ConvertPay(new Guid[] { employeeId });
        }
    }
}
