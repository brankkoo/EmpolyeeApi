using EmployeeApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.Services.Base
{
    public interface IEmployeeService
    {
        public List<Employee> GetEmployees(int page, int size);

        public Employee GetById(Guid id);

        public Employee InsertEmployee(Employee employee);
    }
}
