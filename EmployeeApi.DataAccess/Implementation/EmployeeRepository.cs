using EmployeeApi.DataAccess.Base;
using EmployeeApi.DataContext.Contexts;
using EmployeeApi.Models.Models;
using Microsoft.EntityFrameworkCore;

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
           return _context.Employees.AsEnumerable();
        }

        public Employee InsertEmployee(Employee employee)
        {
            var emp = _context.Employees.Include(b => b.Pay);
            
            _context.Employees.Add(employee);
            
            _context.SaveChanges();
            return employee;
        }
    }
}
