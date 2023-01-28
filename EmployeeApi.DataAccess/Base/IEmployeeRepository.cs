using EmployeeApi.DataContext.Contexts;
using EmployeeApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.DataAccess.Base
{
    public interface IEmployeeRepository
    {

        public IAsyncEnumerable<Employee> GetEmployees();

        public Employee InsertEmployee(Employee employee);
    }
}
