using EmployeeApi.Models.Base;
using EmployeeApi.Models.Models;
using EmployeeApi.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.Services.Implementation
{
    public static class HtmlConversionService
    {
        public static string ConvertEmployeeToHtml(Employee employee,List<IPay> pays)
        {
            var payEur = pays[1];
            var payUsd = pays[2];
            var htmlString = new StringBuilder();
            
            htmlString.AppendFormat
                (@"
                    <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>{0}{1}</h1></div>
                                <h2>Basic info</h2>
                                <ul>
                                    <li>
                                                Adress: {2}    
                                    </li>
                                    <li>
                                                Pay: {3}rsd     
                                    </li>
                                  
                                </ul>
                               <h2>Detailed Pay info</h2>
                               <ul>
                                    <li>
                                                PIO: {4}rsd
                                    </li>
                                    <li>
                                                Insurance: {5}rsd     
                                    </li>
                                  <li>
                                                UnemploymentPlan: {6}rsd     
                                    </li>
                                        <li>
                                                Tax: {7}rsd     
                                    </li>
                                      <li>
                                                Bruto: {8}rsd     
                                    </li>
                                </ul>
                               <h2>Detailed Pay info EUR</h2>
                               <ul>
                                    <li>
                                                PIO:     {9}eur
                                    </li>
                                    <li>
                                                Insurance: {10}eur     
                                    </li>
                                  <li>
                                                UnemploymentPlan: {11}eur     
                                    </li>
                                        <li>
                                                Tax: {12}eur     
                                    </li>
                                      <li>
                                                Bruto: {13}eur     
                                    </li>
                                    <li>
                                                Neto: {14}eur     
                                    </li>
                                </ul>
                               <h2>Detailed Pay info USD</h2>
                               <ul>
                                    <li>
                                                PIO:     {15}usd
                                    </li>
                                    <li>
                                                Insurance: {16}usd     
                                    </li>
                                  <li>
                                                UnemploymentPlan: {17}usd     
                                    </li>
                                        <li>
                                                Tax: {18}usd     
                                    </li>
                                      <li>
                                                Bruto: {19}usd     
                                    </li>
                                    <li>
                                                Neto: {20}eur     
                                    </li>
                                </ul>
                        </body>
                     </html>
                ",
                new object[] 
                {
                employee.Name,
                employee.LastName,
                employee.Address,
                employee.CalculateEmployeeNeto(),
                employee.Pay.PIO,
                employee.Pay.Insurance,
                employee.Pay.UnemployeementPlan,
                employee.Pay.Tax,
                employee.Pay.BrutoPay,
                payEur.PIO,
                payEur.Insurance,
                payEur.UnemployeementPlan,
                payEur.Tax,
                payEur.BrutoPay,
                payEur.NetoPay,
                payUsd.PIO,
                payUsd.Insurance,
                payUsd.UnemployeementPlan,
                payUsd.Tax,
                payUsd.BrutoPay,
                payUsd.NetoPay 
                });
            return htmlString.ToString();
        }
    }
}
