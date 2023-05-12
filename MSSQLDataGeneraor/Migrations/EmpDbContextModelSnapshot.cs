﻿// <auto-generated />
using System;
using MSSQLDataGenerator.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MSSQLDataGenerator.Migrations
{
    [DbContext(typeof(EmpDbContext))]
    partial class EmpDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MSSQLDataGenerator.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("MSSQLDataGenerator.Models.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Date_of_Joining")
                        .HasColumnType("datetime2");

                    b.Property<int>("Department_id_Fk")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Location_id_Fk")
                        .HasColumnType("int");

                    b.Property<int>("Pay_Scale_Fk")
                        .HasColumnType("int");

                    b.Property<string>("Phone_number")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Department_id_Fk");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Location_id_Fk");

                    b.HasIndex("Pay_Scale_Fk");

                    b.HasIndex("Phone_number")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("MSSQLDataGenerator.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("City")
                        .IsUnique();

                    b.ToTable("locations");
                });

            modelBuilder.Entity("MSSQLDataGenerator.Models.PayScale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("MaxSalary")
                        .HasColumnType("bigint");

                    b.Property<long>("MinSalary")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("PayScales");
                });

            modelBuilder.Entity("MSSQLDataGenerator.Models.Employee", b =>
                {
                    b.HasOne("MSSQLDataGenerator.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("Department_id_Fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MSSQLDataGenerator.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("Location_id_Fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MSSQLDataGenerator.Models.PayScale", "PayScale")
                        .WithMany()
                        .HasForeignKey("Pay_Scale_Fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Location");

                    b.Navigation("PayScale");
                });
#pragma warning restore 612, 618
        }
    }
}
