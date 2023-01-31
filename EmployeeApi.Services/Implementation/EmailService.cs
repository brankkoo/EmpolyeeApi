using EmployeeApi.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApi.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private IFileService _fileService;
        public EmailService(IFileService fileService) 
        {
            _fileService= fileService;
        }

        public async void SendEmail(string email, Guid employeeId)
        {
            var pdf = await _fileService.MakeEmployeePdfFile(employeeId);
            MemoryStream ms = new MemoryStream(pdf);
            Attachment attachment = new Attachment(ms, "Employee Report.pdf", "application/pdf");

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("sender@example.com");
                mail.To.Add(email);
                mail.Subject = "Employee Report";
                mail.Body = "Please find attached your employee report";
                mail.Attachments.Add(attachment);

                using (SmtpClient smtp = new SmtpClient("smtp.example.com", 25))
                {
                    smtp.Credentials = new NetworkCredential("username", "password");
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
        }

    }
}
