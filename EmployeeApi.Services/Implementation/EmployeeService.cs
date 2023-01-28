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

        public async IAsyncEnumerable<Employee> GetByIds(Guid[] ids)
        {
            var employees = await _adapter.GetEmployees().ToListAsync();
            foreach (var id in ids)
            {
                var employee = employees.Where(e => e.EmployeeId == id).FirstOrDefault();
                if (employee != null)
                {
                    employee.Pay.NetoPay = employee.Pay.BrutoPay - employee.Pay.PIO + employee.Pay.Tax + employee.Pay.UnemployeementPlan + employee.Pay.Insurance;
                    yield return employee;
                }
            }
        }

        public async IAsyncEnumerable<Employee> GetEmployees(int page, int size)
        {
            var employees = await _adapter.GetEmployees().ToListAsync();

            foreach (var employee in employees.Skip((page - 1) * size).Take(size))
            {
                employee.Pay.NetoPay = employee.Pay.BrutoPay - employee.Pay.PIO + employee.Pay.Tax + employee.Pay.UnemployeementPlan + employee.Pay.Insurance;
                yield return employee;
            }
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
