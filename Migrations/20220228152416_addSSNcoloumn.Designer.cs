﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Testing_Poc_Healthcare.DBContexts;

namespace Testing_Poc_Healthcare.Migrations
{
    [DbContext(typeof(HealthCareDBContext))]
    [Migration("20220228152416_addSSNcoloumn")]
    partial class addSSNcoloumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Testing_Poc_Healthcare.Models.BenefitMaster", b =>
                {
                    b.Property<int>("BenefitID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("InsuranceName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("InsuranceType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PlanProductName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("BenefitID");

                    b.ToTable("Benefitmaster");
                });

            modelBuilder.Entity("Testing_Poc_Healthcare.Models.InsuranceInfo", b =>
                {
                    b.Property<int>("InsuranceInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("InsuranceType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAcive")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("PlanDuration")
                        .HasColumnType("int");

                    b.Property<DateTime>("PlanEndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PlanName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PlanStartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("InsuranceInfoId");

                    b.ToTable("InsuranceInfos");
                });

            modelBuilder.Entity("Testing_Poc_Healthcare.Models.PatientAddress", b =>
                {
                    b.Property<int>("PatientAddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Address2")
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("PatientAddressID");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientAddresses");
                });

            modelBuilder.Entity("Testing_Poc_Healthcare.Models.PatientInfo", b =>
                {
                    b.Property<int>("PatientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MiddleName")
                        .HasColumnType("longtext");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SSN")
                        .HasColumnType("int");

                    b.HasKey("PatientID");

                    b.ToTable("PatientInfos");
                });

            modelBuilder.Entity("Testing_Poc_Healthcare.Models.PatientInsurance", b =>
                {
                    b.Property<int>("PatientInsuranceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("InsuranceId")
                        .HasColumnType("int");

                    b.Property<int>("IsActive")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Term")
                        .HasColumnType("int");

                    b.HasKey("PatientInsuranceId");

                    b.HasIndex("InsuranceId");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientInsurances");
                });

            modelBuilder.Entity("Testing_Poc_Healthcare.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Testing_Poc_Healthcare.Models.UserInfo", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Gender")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("Testing_Poc_Healthcare.Models.UserRoles", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserRoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Testing_Poc_Healthcare.Models.PatientAddress", b =>
                {
                    b.HasOne("Testing_Poc_Healthcare.Models.PatientInfo", "PatientInfo")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PatientInfo");
                });

            modelBuilder.Entity("Testing_Poc_Healthcare.Models.PatientInsurance", b =>
                {
                    b.HasOne("Testing_Poc_Healthcare.Models.InsuranceInfo", "InsuranceInfo")
                        .WithMany()
                        .HasForeignKey("InsuranceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testing_Poc_Healthcare.Models.PatientInfo", "PatientInfo")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InsuranceInfo");

                    b.Navigation("PatientInfo");
                });

            modelBuilder.Entity("Testing_Poc_Healthcare.Models.UserRoles", b =>
                {
                    b.HasOne("Testing_Poc_Healthcare.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testing_Poc_Healthcare.Models.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("UserInfo");
                });
#pragma warning restore 612, 618
        }
    }
}