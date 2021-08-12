﻿// <auto-generated />
using System;
using CDBBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CDBBank.Migrations
{
    [DbContext(typeof(CDBBankContext))]
    [Migration("20210809055259_FilterDates")]
    partial class FilterDates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CDBBank.Models.AllUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Balance")
                        .HasColumnType("float");

                    b.Property<string>("BankName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ElectricityConsumerNum")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<long?>("Mobile")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("AllUsers");
                });

            modelBuilder.Entity("CDBBank.Models.BankUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNum")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<double?>("Balance")
                        .HasColumnType("float");

                    b.Property<string>("ConfirmPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DoB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Mobile")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PinCode")
                        .HasColumnType("int");

                    b.Property<string>("SAns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sques")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AccountNum", "Username", "Mobile")
                        .IsUnique()
                        .HasFilter("[AccountNum] IS NOT NULL AND [Username] IS NOT NULL");

                    b.ToTable("BankUsers");
                });

            modelBuilder.Entity("CDBBank.Models.ElectricityBill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("BillAmount")
                        .HasColumnType("real");

                    b.Property<string>("BillNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BillUsersId")
                        .HasColumnType("int");

                    b.Property<DateTime>("GeneratedBillDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("Penalty")
                        .HasColumnType("real");

                    b.Property<int>("UnitsConsumed")
                        .HasColumnType("int");

                    b.Property<float?>("UnitsPrice")
                        .HasColumnType("real");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BillUsersId");

                    b.ToTable("ElectricityBills");
                });

            modelBuilder.Entity("CDBBank.Models.FilterDates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("FilterDates");
                });

            modelBuilder.Entity("CDBBank.Models.NeftRecords", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("BeneficiaryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfirmAccountNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NeftRecords");
                });

            modelBuilder.Entity("CDBBank.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BankUsersId")
                        .HasColumnType("int");

                    b.Property<double>("ClosingBalance")
                        .HasColumnType("float");

                    b.Property<double?>("Deposit")
                        .HasColumnType("float");

                    b.Property<string>("Narration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<double?>("Withdrawal")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BankUsersId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CDBBank.Models.ElectricityBill", b =>
                {
                    b.HasOne("CDBBank.Models.AllUser", "BillUsers")
                        .WithMany()
                        .HasForeignKey("BillUsersId");

                    b.Navigation("BillUsers");
                });

            modelBuilder.Entity("CDBBank.Models.Transaction", b =>
                {
                    b.HasOne("CDBBank.Models.BankUser", "BankUsers")
                        .WithMany()
                        .HasForeignKey("BankUsersId");

                    b.Navigation("BankUsers");
                });
#pragma warning restore 612, 618
        }
    }
}