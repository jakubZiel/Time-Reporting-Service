﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using project.Models.EntityFramework;

namespace project.Migrations
{
    [DbContext(typeof(TRSDbContext))]
    [Migration("20211202003624_remove-field-reported-time")]
    partial class removefieldreportedtime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("project.Models.EntityFramework.Activity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AcceptedTime")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<bool>("Frozen")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int");

                    b.Property<int?>("ReportID")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("ReportID");

                    b.ToTable("Activity");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            DateCreated = new DateTime(2021, 12, 2, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "checking if everything is ok with the API",
                            DurationMinutes = 30,
                            EmployeeID = 1,
                            Frozen = false,
                            Name = "API debugging",
                            ProjectID = 1,
                            Tag = "debugging"
                        });
                });

            modelBuilder.Entity("project.Models.EntityFramework.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ID");

                    b.ToTable("Employee");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Jakub",
                            Password = "123",
                            Surname = "Zielinski"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Piotr",
                            Password = "1234",
                            Surname = "Lewandowski"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Waldemar",
                            Password = "12345",
                            Surname = "Grabski"
                        },
                        new
                        {
                            ID = 4,
                            Name = "Krzysztof",
                            Password = "123456",
                            Surname = "Chabko"
                        });
                });

            modelBuilder.Entity("project.Models.EntityFramework.EmployeeProject", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("ProjectID");

                    b.ToTable("EmployeeProject");
                });

            modelBuilder.Entity("project.Models.EntityFramework.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int?>("OwnerID")
                        .HasColumnType("int");

                    b.Property<int>("TimeBudget")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Project");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Active = true,
                            Description = "Some React fullstack application",
                            Name = "ReactApp",
                            OwnerID = 1,
                            TimeBudget = 1500
                        },
                        new
                        {
                            ID = 2,
                            Active = true,
                            Description = "Some Vue.Js frontend application",
                            Name = "VueApp",
                            OwnerID = 2,
                            TimeBudget = 2200
                        },
                        new
                        {
                            ID = 3,
                            Active = true,
                            Description = "Some Spring Boot backend application",
                            Name = "Spring Boot App",
                            OwnerID = 4,
                            TimeBudget = 1600
                        });
                });

            modelBuilder.Entity("project.Models.EntityFramework.Report", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<bool>("Frozen")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Month")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("project.Models.EntityFramework.Tag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProjectID");

                    b.ToTable("Tag");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "coding",
                            ProjectID = 1
                        },
                        new
                        {
                            ID = 2,
                            Name = "debuging",
                            ProjectID = 1
                        },
                        new
                        {
                            ID = 3,
                            Name = "database",
                            ProjectID = 2
                        },
                        new
                        {
                            ID = 4,
                            Name = "coding",
                            ProjectID = 2
                        },
                        new
                        {
                            ID = 5,
                            Name = "drinking",
                            ProjectID = 3
                        },
                        new
                        {
                            ID = 6,
                            Name = "coding",
                            ProjectID = 3
                        });
                });

            modelBuilder.Entity("project.Models.EntityFramework.Activity", b =>
                {
                    b.HasOne("project.Models.EntityFramework.Employee", "Employee")
                        .WithMany("Activities")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project.Models.EntityFramework.Project", "Project")
                        .WithMany("Activities")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project.Models.EntityFramework.Report", "Report")
                        .WithMany("Reported")
                        .HasForeignKey("ReportID");

                    b.Navigation("Employee");

                    b.Navigation("Project");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("project.Models.EntityFramework.EmployeeProject", b =>
                {
                    b.HasOne("project.Models.EntityFramework.Employee", "Employee")
                        .WithMany("EmployeeProjects")
                        .HasForeignKey("EmployeeID");

                    b.HasOne("project.Models.EntityFramework.Project", "Project")
                        .WithMany("EmployeeProjects")
                        .HasForeignKey("ProjectID");

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("project.Models.EntityFramework.Project", b =>
                {
                    b.HasOne("project.Models.EntityFramework.Employee", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerID");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("project.Models.EntityFramework.Report", b =>
                {
                    b.HasOne("project.Models.EntityFramework.Employee", "Employee")
                        .WithMany("Reports")
                        .HasForeignKey("EmployeeID");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("project.Models.EntityFramework.Tag", b =>
                {
                    b.HasOne("project.Models.EntityFramework.Project", null)
                        .WithMany("Tags")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("project.Models.EntityFramework.Employee", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("EmployeeProjects");

                    b.Navigation("Reports");
                });

            modelBuilder.Entity("project.Models.EntityFramework.Project", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("EmployeeProjects");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("project.Models.EntityFramework.Report", b =>
                {
                    b.Navigation("Reported");
                });
#pragma warning restore 612, 618
        }
    }
}
