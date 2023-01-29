using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.Services.Base
{
    public interface IConversionService
    {
        public Task<List<float>> ConvertToEurAndUsd(float rsd);
    }
}
