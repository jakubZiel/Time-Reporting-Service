using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace project.Models.EntityFramework
{
    public class Employee
    {
        public int ID {get; set;}
        public string Name {get; set; }
        public string Surname {get; set; }
        public string Password { get; set; }

        public ICollection<Activity> Activities { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }

    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(s => s.Surname)
                .HasMaxLength(40);
            builder.Property(s => s.Name)
                .HasMaxLength(40);
            builder.Property(s => s.Password)
                .HasMaxLength(50);
        }
    }
}