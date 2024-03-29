﻿// <auto-generated />
using System;
using EmployeeApi.DataContext.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeApi.DataContext.Migrations
{
    [DbContext(typeof(EmployeeContext))]
    partial class EmployeeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeApi.Models.Models.Employee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = new Guid("7f558ec4-4b30-4467-89b1-b2f1a991ab55"),
                            Address = "addd",
                            LastName = "Doe",
                            Name = "John"
                        },
                        new
                        {
                            EmployeeId = new Guid("c56524ec-677c-4391-887c-90b8204d9bbe"),
                            Address = "addd",
                            LastName = "Johnes",
                            Name = "Tom"
                        });
                });

            modelBuilder.Entity("EmployeeApi.Models.Models.Pay", b =>
                {
                    b.Property<Guid>("PayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("BrutoPay")
                        .HasColumnType("real");

                    b.Property<float>("Insurance")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("real")
                        .HasComputedColumnSql("[BrutoPay]*5.15/100", true);

                    b.Property<float>("NetoPay")
                        .HasColumnType("real");

                    b.Property<float>("PIO")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("real")
                        .HasComputedColumnSql("[BrutoPay]*24/100", true);

                    b.Property<float>("Tax")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("real")
                        .HasComputedColumnSql("[BrutoPay]*10/100", true);

                    b.Property<float>("UnemployeementPlan")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("real")
                        .HasComputedColumnSql("[BrutoPay]*0.75/100", true);

                    b.HasKey("PayId");

                    b.ToTable("Pays");
                });

            modelBuilder.Entity("EmployeeApi.Models.Models.Pay", b =>
                {
                    b.HasOne("EmployeeApi.Models.Models.Employee", "Employee")
                        .WithOne("Pay")
                        .HasForeignKey("EmployeeApi.Models.Models.Pay", "PayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("EmployeeApi.Models.Models.Employee", b =>
                {
                    b.Navigation("Pay")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
