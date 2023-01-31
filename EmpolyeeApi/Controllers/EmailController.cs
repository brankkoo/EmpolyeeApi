using EmployeeApi.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EmpolyeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService= emailService;
        }

        [HttpPost]
        [Route("send-email")]
        public  void SendEmail(Guid employeeId, string email) 
        {
            if (employeeId != null || employeeId != Guid.Empty && email.IsNullOrEmpty())
                    _emailService.SendEmail(email, employeeId);
            
        }
    }
}
