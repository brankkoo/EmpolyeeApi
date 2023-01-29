using EmployeeApi.Models.Base;
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
        public  IAsyncEnumerable<Employee> GetEmployees(int page, int size);

        public IAsyncEnumerable<Employee> GetByIds(Guid[] ids);

        public Employee InsertEmployee(string name, string lastName, string Adress, float pay);

        public  Task<List<IPay>> ConvertPay(Guid[] employeeId);
    }
}
