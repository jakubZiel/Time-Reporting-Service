using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace project.Models.EntityFramework
{
    public class Project
    {
        public int ID {get; set;}
        public int ? OwnerID { get; set; }
        public string Name {get; set;}
        public int TimeBudget {get; set;}
        public bool Active {get; set;}
        public string ? Description {get; set;}

        public ICollection<Activity> Activities { get; set; }
        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
        public Employee Owner { get; set; }
        public ICollection<Tag> Tags { get; set; }

        public class EmployeeConfig : IEntityTypeConfiguration<Project>
        {
            public void Configure(EntityTypeBuilder<Project> builder)
            {
                builder.Property(p => p.Name)
                    .HasMaxLength(40);

                builder.Property(s => s.Name)
                    .HasMaxLength(40);
            }
        }
    }
}