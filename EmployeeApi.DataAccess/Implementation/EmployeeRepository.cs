using EmployeeApi.DataAccess.Base;
using EmployeeApi.DataContext.Contexts;
using EmployeeApi.Models.Models;

namespace EmployeeApi.DataAccess.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeContext _context;
        public EmployeeRepository(EmployeeContext context) 
        {
            _context = context;
        }
        public IEnumerable<Employee> GetEmployees()
        {
           return _context.Employees;
        }

        public Employee InsertEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
