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

        public  IEnumerable<Employee> GetEmployees(int page, int size)
        {
            
            if (page > 0)
                foreach (var employee in _adapter.GetEmployees().Where(e => e.Pay != null).Skip((page-1)*size).Take(size))
                {
                    employee.Pay.NetoPay = employee.Pay.BrutoPay - employee.Pay.PIO + employee.Pay.Tax + employee.Pay.UnemployeementPlan + employee.Pay.Insurance;
                    yield return employee;
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
            return employee;
        }
    }
}
