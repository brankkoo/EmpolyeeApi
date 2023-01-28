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
        public async IAsyncEnumerable<Employee> GetEmployees()
        {
           
            IAsyncEnumerable<Employee> employees =  _context.Employees.Where(e => e.Pay != null).Include("Pay").AsAsyncEnumerable();
            
            await foreach (var employee in  employees)
            {
              yield return employee;
            }
            
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
