using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.Models.Models
{
    public class Pay
    {
        
        public Guid PayId { get; set; }

        public Employee Employee { get; set; }
        
        public float PIO { get; set; } //24%

        public float Insurance { get; set; } //5.15%

        public float UnemployeementPlan { get; set; } //0.75%

        public float Tax { get; set; } //10%

        public float BrutoPay { get; set; }

        public float NetoPay { get; set; }
    }
}
