using EmployeeApi.DataAccess.Base;
using EmployeeApi.Models.Models;
using EmployeeApi.Services.Base;


namespace EmployeeApi.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _adapter;
        public EmployeeService(IEmployeeRepository adapter)
        {
            _adapter = adapter;
        }

        public Employee GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public  async Task<List<Employee>> GetEmployees(int page, int size)
        {
            var employees = await _adapter.GetEmployees().ToListAsync();
            if (page > 0)
            {
                foreach (var employee in employees.Skip((page - 1) * size).Take(size))
                {
                    employee.Pay.NetoPay = employee.Pay.BrutoPay - employee.Pay.PIO + employee.Pay.Tax + employee.Pay.UnemployeementPlan + employee.Pay.Insurance;
                }
                return employees;
            }
            else
                throw new Exception("Page Num must be higher than 0");
        }

        public Employee InsertEmployee(string name, string lastName, string Adress, float pay)
        {
            Employee employee = new Employee
            {
                Name = name,
                LastName = lastName,
                Address = Adress,
                Pay = new Pay { BrutoPay = pay }
            };
            
            _adapter.InsertEmployee(employee);
            employee.Pay.NetoPay = employee.Pay.BrutoPay - employee.Pay.PIO + employee.Pay.Tax + employee.Pay.UnemployeementPlan + employee.Pay.Insurance;
            return employee;
        }
    }
}
