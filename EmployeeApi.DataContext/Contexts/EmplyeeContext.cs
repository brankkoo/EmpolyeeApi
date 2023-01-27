using EmployeeApi.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.DataContext.Contexts
{
    public class EmployeeContext : DbContext
    {

        public EmployeeContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Pay> Pays { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Employee>(employee =>
            {
                employee.HasKey(employee => employee.EmployeeId);
                employee.HasData
                    (
                    new Employee { EmployeeId = new Guid("7f558ec4-4b30-4467-89b1-b2f1a991ab55"),
                        Name = "John",
                        LastName = "Doe",
                        Address = "addd",
                         },
                     new Employee
                    {
                        EmployeeId = new Guid("c56524ec-677c-4391-887c-90b8204d9bbe"),
                        Name = "Tom",
                        LastName = "Johnes",
                        Address = "addd",
                        }
                    );

                employee.HasOne(employee => employee.Pay)
                .WithOne(employee => employee.Employee)
                .HasForeignKey<Pay>(employee => employee.PayId);

            });

            modelBuilder.Entity<Pay>(pay =>
            {

                
                pay.HasKey(pay => pay.PayId);
                pay.Property(nameof(Pay.PIO))
                .HasComputedColumnSql("[BrutoPay]*24/100", stored: true);

                pay.Property(nameof(Pay.Insurance))
               .HasComputedColumnSql("[BrutoPay]*5.15/100", stored: true);

                pay.Property(nameof(Pay.Tax))
               .HasComputedColumnSql("[BrutoPay]*10/100", stored: true);

                pay.Property(nameof(Pay.UnemployeementPlan))
               .HasComputedColumnSql("[BrutoPay]*0.75/100", stored: true);

                
            });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }
    }
}