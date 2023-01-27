using EmployeeApi.DataAccess.Base;
using EmployeeApi.DataAccess.Implementation;
using EmployeeApi.Models.Models;
using EmployeeApi.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Employee> GetEmployees(int page, int size)
        {
            if (page > 0)
                return _adapter.GetEmployees().Skip(page - 1 * size).Take(size).ToList();
            else
                throw new Exception("Page Num must be higher than 0");
        }

        public Employee InsertEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
