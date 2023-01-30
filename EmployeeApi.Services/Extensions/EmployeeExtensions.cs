using EmployeeApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.Services.Extensions
{
    public static class EmployeeExtensions
    {
        public static float CalculateEmployeeNeto(this Employee employee)
        {
            return employee.Pay.BrutoPay - employee.Pay.PIO + employee.Pay.Tax + employee.Pay.UnemployeementPlan + employee.Pay.Insurance;
        }
    }
}
