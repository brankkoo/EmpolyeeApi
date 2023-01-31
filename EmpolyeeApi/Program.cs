using DinkToPdf.Contracts;
using DinkToPdf;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using EmployeeApi.DataAccess.Base;
using EmployeeApi.DataAccess.Implementation;
using EmployeeApi.DataContext.Contexts;
using EmployeeApi.Services.Base;
using EmployeeApi.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EmpolyeeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            builder.Services.AddDbContext<EmployeeContext>(options => 
            {
                options.UseSqlServer(builder.Configuration.GetSection("SqlServer")["ConnectionStrings"]);
            });
            builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            builder.Services.AddScoped<IEmailService,EmailService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IConversionService, ConversionService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IEmployeeService,EmployeeService>();
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}