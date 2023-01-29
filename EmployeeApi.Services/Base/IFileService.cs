using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.Services.Base
{
    public interface IFileService
    {
        public Task<byte[]> MakeEmployeesExcelFile();
        public Task<byte[]> MakeEmployeesCsvFile();
    }
}
