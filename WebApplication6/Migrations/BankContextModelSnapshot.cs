﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication6.Models;

#nullable disable

namespace WebApplication6.Migrations
{
    [DbContext(typeof(BankContext))]
    partial class BankContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("WebApplication6.Models.BankBranch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BranchManager")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("EmployeeCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LocationURL")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BankBranches");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BranchManager = "Abdulrahman",
                            EmployeeCount = 7,
                            LocationName = "Alshamyia",
                            LocationURL = "https://maps.app.goo.gl/k5DD4wvKX38Y6RFQ7"
                        });
                });

            modelBuilder.Entity("WebApplication6.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BankBranchId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CivilId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BankBranchId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("WebApplication6.Models.Employee", b =>
                {
                    b.HasOne("WebApplication6.Models.BankBranch", "BankBranch")
                        .WithMany("Employees")
                        .HasForeignKey("BankBranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankBranch");
                });

            modelBuilder.Entity("WebApplication6.Models.BankBranch", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}