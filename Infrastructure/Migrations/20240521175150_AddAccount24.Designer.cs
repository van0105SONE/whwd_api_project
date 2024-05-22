﻿// <auto-generated />
using System;
using Infrastructure.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContexts))]
    [Migration("20240521175150_AddAccount24")]
    partial class AddAccount24
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Model.Account.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccountNo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("AccountTypesId")
                        .HasColumnType("uuid");

                    b.Property<double>("Balance")
                        .HasColumnType("double precision");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BookingNO")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreateById")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OwnById")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectPlanId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdateById")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountTypesId");

                    b.HasIndex("CreateById");

                    b.HasIndex("OwnById");

                    b.HasIndex("ProjectPlanId");

                    b.HasIndex("UpdateById");

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("Infrastructure.Model.Account.AccountType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("accountTypes");
                });

            modelBuilder.Entity("Infrastructure.Model.Account.SourceType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("sourceTypes");
                });

            modelBuilder.Entity("Infrastructure.Model.Account.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreateById")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FromAccount")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SourceTypesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TransactionTypeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdateById")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreateById");

                    b.HasIndex("SourceTypesId");

                    b.HasIndex("TransactionTypeId");

                    b.HasIndex("UpdateById");

                    b.ToTable("transactions");
                });

            modelBuilder.Entity("Infrastructure.Model.Account.TransactionType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("transactionTypes");
                });

            modelBuilder.Entity("Infrastructure.Model.Address.District", b =>
                {
                    b.Property<string>("districtCode")
                        .HasColumnType("text");

                    b.Property<string>("ProvinceCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("districtName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("districtCode");

                    b.HasIndex("ProvinceCode");

                    b.ToTable("districts");
                });

            modelBuilder.Entity("Infrastructure.Model.Address.Province", b =>
                {
                    b.Property<string>("ProvinceCode")
                        .HasColumnType("text");

                    b.Property<string>("ProvinceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProvinceCode");

                    b.ToTable("provinces");
                });

            modelBuilder.Entity("Infrastructure.Model.Address.Village", b =>
                {
                    b.Property<string>("villageCode")
                        .HasColumnType("text");

                    b.Property<string>("districtCode")
                        .HasColumnType("text");

                    b.Property<string>("villageName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("villageCode");

                    b.HasIndex("districtCode");

                    b.ToTable("villages");
                });

            modelBuilder.Entity("Infrastructure.Model.Student.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreateById")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GloveSize")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<int>("ShirtSize")
                        .HasColumnType("integer");

                    b.Property<int>("ShoesSize")
                        .HasColumnType("integer");

                    b.Property<int>("SkirtSize")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdateById")
                        .HasColumnType("text");

                    b.Property<DateTime>("birthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("fname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("level")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("lname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreateById");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UpdateById");

                    b.ToTable("students");
                });

            modelBuilder.Entity("Infrastructure.Model.University.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("facultyId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("facultyId");

                    b.ToTable("departments");
                });

            modelBuilder.Entity("Infrastructure.Model.University.Faculty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("universityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("universityId");

                    b.ToTable("faculty");
                });

            modelBuilder.Entity("Infrastructure.Model.University.Major", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("majors");
                });

            modelBuilder.Entity("Infrastructure.Model.University.University", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("university");
                });

            modelBuilder.Entity("Infrastructure.Model.Users.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("BornVillagevillageCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("CurrentVillagevillageCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MajorId")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Occupation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpiry")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("UserTypeId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BornVillagevillageCode");

                    b.HasIndex("CurrentVillagevillageCode");

                    b.HasIndex("MajorId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("UserTypeId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Model.Users.Position", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PositionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefNo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("Infrastructure.Model.Users.PositionTeam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PositionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("position_teams");
                });

            modelBuilder.Entity("Infrastructure.Model.Users.ProjectTeam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefNO")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("project_teams");
                });

            modelBuilder.Entity("Infrastructure.Model.Users.UserType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("userTypes");
                });

            modelBuilder.Entity("Infrastructure.Model.Work.DonateThing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreateById")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<Guid>("ProjectPlanId")
                        .HasColumnType("uuid");

                    b.Property<int>("Unit")
                        .HasColumnType("integer");

                    b.Property<string>("UnitType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdateById")
                        .HasColumnType("text");

                    b.Property<int>("personAmount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreateById");

                    b.HasIndex("ProjectPlanId");

                    b.HasIndex("UpdateById");

                    b.ToTable("donateThings");
                });

            modelBuilder.Entity("Infrastructure.Model.Work.ProjectPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreateById")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdateById")
                        .HasColumnType("text");

                    b.Property<double>("ValueInBath")
                        .HasColumnType("double precision");

                    b.Property<double>("ValueInDollar")
                        .HasColumnType("double precision");

                    b.Property<double>("valueInKip")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CreateById");

                    b.HasIndex("UpdateById");

                    b.ToTable("projectPlan");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Model.Account.Account", b =>
                {
                    b.HasOne("Infrastructure.Model.Account.AccountType", "AccountTypes")
                        .WithMany()
                        .HasForeignKey("AccountTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", "CreateBy")
                        .WithMany()
                        .HasForeignKey("CreateById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", "OwnBy")
                        .WithMany()
                        .HasForeignKey("OwnById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Work.ProjectPlan", "ProjectPlan")
                        .WithMany()
                        .HasForeignKey("ProjectPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", "UpdateBy")
                        .WithMany()
                        .HasForeignKey("UpdateById");

                    b.Navigation("AccountTypes");

                    b.Navigation("CreateBy");

                    b.Navigation("OwnBy");

                    b.Navigation("ProjectPlan");

                    b.Navigation("UpdateBy");
                });

            modelBuilder.Entity("Infrastructure.Model.Account.Transaction", b =>
                {
                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", "CreateBy")
                        .WithMany()
                        .HasForeignKey("CreateById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Account.SourceType", "SourceTypes")
                        .WithMany()
                        .HasForeignKey("SourceTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Account.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", "UpdateBy")
                        .WithMany()
                        .HasForeignKey("UpdateById");

                    b.Navigation("CreateBy");

                    b.Navigation("SourceTypes");

                    b.Navigation("TransactionType");

                    b.Navigation("UpdateBy");
                });

            modelBuilder.Entity("Infrastructure.Model.Address.District", b =>
                {
                    b.HasOne("Infrastructure.Model.Address.Province", "province")
                        .WithMany()
                        .HasForeignKey("ProvinceCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("province");
                });

            modelBuilder.Entity("Infrastructure.Model.Address.Village", b =>
                {
                    b.HasOne("Infrastructure.Model.Address.District", "district")
                        .WithMany()
                        .HasForeignKey("districtCode");

                    b.Navigation("district");
                });

            modelBuilder.Entity("Infrastructure.Model.Student.Student", b =>
                {
                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", "CreateBy")
                        .WithMany()
                        .HasForeignKey("CreateById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Work.ProjectPlan", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", "UpdateBy")
                        .WithMany()
                        .HasForeignKey("UpdateById");

                    b.Navigation("CreateBy");

                    b.Navigation("Project");

                    b.Navigation("UpdateBy");
                });

            modelBuilder.Entity("Infrastructure.Model.University.Department", b =>
                {
                    b.HasOne("Infrastructure.Model.University.Faculty", "faculty")
                        .WithMany()
                        .HasForeignKey("facultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("faculty");
                });

            modelBuilder.Entity("Infrastructure.Model.University.Faculty", b =>
                {
                    b.HasOne("Infrastructure.Model.University.University", "university")
                        .WithMany()
                        .HasForeignKey("universityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("university");
                });

            modelBuilder.Entity("Infrastructure.Model.University.Major", b =>
                {
                    b.HasOne("Infrastructure.Model.University.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Infrastructure.Model.Users.ApplicationUser", b =>
                {
                    b.HasOne("Infrastructure.Model.Address.Village", "BornVillage")
                        .WithMany()
                        .HasForeignKey("BornVillagevillageCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Address.Village", "CurrentVillage")
                        .WithMany()
                        .HasForeignKey("CurrentVillagevillageCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.University.Major", "Major")
                        .WithMany()
                        .HasForeignKey("MajorId");

                    b.HasOne("Infrastructure.Model.Users.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeId");

                    b.Navigation("BornVillage");

                    b.Navigation("CurrentVillage");

                    b.Navigation("Major");

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("Infrastructure.Model.Users.PositionTeam", b =>
                {
                    b.HasOne("Infrastructure.Model.Users.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Users.ProjectTeam", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", "User")
                        .WithMany("positionTeams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("Team");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Infrastructure.Model.Work.DonateThing", b =>
                {
                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", "CreateBy")
                        .WithMany()
                        .HasForeignKey("CreateById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Work.ProjectPlan", "ProjectPlan")
                        .WithMany()
                        .HasForeignKey("ProjectPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", "UpdateBy")
                        .WithMany()
                        .HasForeignKey("UpdateById");

                    b.Navigation("CreateBy");

                    b.Navigation("ProjectPlan");

                    b.Navigation("UpdateBy");
                });

            modelBuilder.Entity("Infrastructure.Model.Work.ProjectPlan", b =>
                {
                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", "CreateBy")
                        .WithMany()
                        .HasForeignKey("CreateById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", "UpdateBy")
                        .WithMany()
                        .HasForeignKey("UpdateById");

                    b.Navigation("CreateBy");

                    b.Navigation("UpdateBy");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Infrastructure.Model.Users.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Model.Users.ApplicationUser", b =>
                {
                    b.Navigation("positionTeams");
                });
#pragma warning restore 612, 618
        }
    }
}
