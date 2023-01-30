using EmployeeApi.DataAccess.Base;
using EmployeeApi.Helper.Helpers;
using EmployeeApi.Models.Base;
using EmployeeApi.Models.CalculatedModels;
using EmployeeApi.Models.Models;
using EmployeeApi.Services.Base;
using EmployeeApi.Services.Extensions;

namespace EmployeeApi.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _adapter;
        private IConversionService _conversionService;
        public EmployeeService(IEmployeeRepository adapter, IConversionService conversionService)
        {
            _adapter = adapter;
            _conversionService = conversionService;
        }

        public async IAsyncEnumerable<Employee> GetByIds(Guid[] ids)
        {
            var employees = await _adapter.GetEmployees().ToListAsync();
            foreach (var id in ids)
            {
                var employee = employees.Where(e => e.EmployeeId == id).FirstOrDefault();
                if (employee != null)
                {
                    employee.Pay.NetoPay = employee.CalculateEmployeeNeto();
                    yield return employee;
                }
            }
        }

        public async IAsyncEnumerable<Employee> GetEmployees(int page, int size)
        {
            var employees = await _adapter.GetEmployees().ToListAsync();

            foreach (var employee in employees.Skip((page - 1) * size).Take(size))
            {
                employee.Pay.NetoPay = employee.CalculateEmployeeNeto();
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
            employee.Pay.NetoPay = employee.CalculateEmployeeNeto();
            return employee;
        }

        public async Task<List<IPay>> ConvertPay(Guid[] employeeId) 
        {
            //TODO : can be more efficient by simply calling an API that retruns the currency pairs counter value and do the calculations here 
            var payRsd = await GetByIds(employeeId).Select(e => e.Pay).FirstOrDefaultAsync();
            List<float> PIOConverted = await _conversionService.ConvertToEurAndUsd(payRsd.PIO);
            List<float> InsuranceConverted = await _conversionService.ConvertToEurAndUsd(payRsd.Insurance);
            List<float> UnemployeementPlanConverted = await _conversionService.ConvertToEurAndUsd(payRsd.UnemployeementPlan);
            List<float> BrutoPayConverted = await _conversionService.ConvertToEurAndUsd(payRsd.BrutoPay);
            List<float> TaxConverted = await _conversionService.ConvertToEurAndUsd(payRsd.Tax);
            List<float> NetoPayConverted = await _conversionService.ConvertToEurAndUsd(payRsd.NetoPay);
            return new List<IPay>
            {
                payRsd,
                new PayEur
                {
                    CurrencyType = Currencies.EUR.ToString(),
                    PIO = PIOConverted[(int)Currencies.EUR],
                    BrutoPay = BrutoPayConverted[(int)Currencies.EUR],
                    NetoPay= NetoPayConverted[(int)Currencies.EUR],
                    Tax= TaxConverted[(int)Currencies.EUR],
                    Insurance= InsuranceConverted[(int)Currencies.EUR],
                    UnemployeementPlan = UnemployeementPlanConverted[(int)Currencies.EUR]
                },
                new PayUsd
                {
                    CurrencyType = Currencies.USD.ToString(),
                    PIO= PIOConverted[1],
                    UnemployeementPlan= UnemployeementPlanConverted[(int)Currencies.USD],
                    Insurance= InsuranceConverted[(int)Currencies.USD],
                    Tax= TaxConverted[(int)Currencies.USD],
                    NetoPay= NetoPayConverted[(int)Currencies.USD],
                    BrutoPay = BrutoPayConverted[(int)Currencies.USD]
                }
            };
        }
    }
}
