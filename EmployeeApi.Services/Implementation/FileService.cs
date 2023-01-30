using ClosedXML.Excel;
using CsvHelper;
using DinkToPdf;
using DinkToPdf.Contracts;
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
        private IEmployeeService _employeeService;
        private IConverter _converter;

        public FileService(IEmployeeRepository repo, IEmployeeService employeeService, IConverter converter)
        {
            _repo = repo;
            _employeeService = employeeService;
            _converter = converter;
        }

        public async Task<byte[]> MakeEmployeesExcelFile()
        {
            var employees = await _repo.GetEmployees().ToListAsync();

            using (var wbook = new XLWorkbook())
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

                using (var memoryStream = new MemoryStream())
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
            using (var memStream = new MemoryStream())
            {
                var writer = new StreamWriter(memStream);
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(employees);
                }
                return memStream.ToArray();
            }
        }

        public async Task<byte[]> MakeEmployeePdfFile(Guid employeeId)
        {
            var employee = await _repo.GetEmployees().Where(e => e.EmployeeId == employeeId).FirstOrDefaultAsync();
            var paysConverted = await _employeeService.ConvertPay(new Guid[] { employeeId });
            var html = HtmlConversionService.ConvertEmployeeToHtml(employee, paysConverted);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = $"{employee.Name}{employee.LastName} Report"

            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = html

            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            return _converter.Convert(pdf);
        }
    }
}
