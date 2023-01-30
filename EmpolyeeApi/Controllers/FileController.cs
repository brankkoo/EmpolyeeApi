using EmployeeApi.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpolyeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
      private IFileService _fileService;
        public FileController(IFileService fileService) 
        {
            _fileService = fileService;
        }

        [HttpGet]
        [Route("get-employees-excel")]
        public async Task<IActionResult> GetEmployeesExcel() 
        {
            return File(
                await _fileService.MakeEmployeesExcelFile(), 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Employees.xlsx"
                );
        }

        [HttpGet]
        [Route("get-employees-csv")]
        public async Task<IActionResult> GetEmployeesCsv()
        {
            return File(
                await _fileService.MakeEmployeesCsvFile(),
                "text/csv",
                "Employees.csv"
                );
        }

        [HttpGet]
        [Route("get-employee-detailed-pdf")]
        public async Task<IActionResult> GetEmployeesJson(Guid employeeId) 
        {
            return File(
               await _fileService.MakeEmployeePdfFile(employeeId),
               "application/pdf",
               "Employee Report.pdf"
               );
        }
    }
}
