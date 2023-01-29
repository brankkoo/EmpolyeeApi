using ClosedXML.Excel;
using CsvHelper;
using EmployeeApi.DataAccess.Base;
using EmployeeApi.Models.Models;
using EmployeeApi.Services.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.Services.Implementation
{
    public class FileService : IFileService
    {
        private IEmployeeRepository _repo;
        public FileService(IEmployeeRepository repo) 
        {
            _repo= repo;
        }

        public async Task<byte[]> MakeEmployeesExcelFile() 
        {
            var employees = await _repo.GetEmployees().ToListAsync();

            using(var wbook = new XLWorkbook()) 
            {
                var wokrSheet = wbook.Worksheets.Add("Employees");
                wokrSheet.Cell("A1").Value = "Name";
                wokrSheet.Cell("B1").Value = "Last Name";
                wokrSheet.Cell("C1").Value = "Adress";
                wokrSheet.Cell("D1").Value = "BrutoPay";
                
                int j = 2;
                for (int i = 0; i < employees.Count; i++)
                {
                    wokrSheet.Cell($"A{j}").Value = employees[i].Name;
                    wokrSheet.Cell($"B{j}").Value = employees[i].LastName;
                    wokrSheet.Cell($"C{j}").Value = employees[i].Address;
                    wokrSheet.Cell($"D{j}").Value = employees[i].Pay.BrutoPay;
                    j++;
                }

                using(var memoryStream = new MemoryStream()) 
                {
                    wbook.SaveAs(memoryStream);
                    var content = memoryStream.ToArray();
                    return content;
                }

            }
        }

        public async Task<byte[]> MakeEmployeesCsvFile() 
        {
            var employees = await _repo.GetEmployees().ToListAsync();
            using(var memStream = new MemoryStream())
            {
                var writer = new StreamWriter(memStream);
                using(var csv = new CsvWriter(writer,CultureInfo.InvariantCulture)) 
                {
                    csv.WriteRecords(employees);
                }
                return memStream.ToArray();
            }
        }
    }
}
