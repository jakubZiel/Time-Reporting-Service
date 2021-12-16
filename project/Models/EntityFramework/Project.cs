using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
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
        public byte[] Timestamp { get; set; }
        [JsonIgnore]
        public virtual ICollection<Activity> Activities { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
        [JsonIgnore]
        public virtual Employee Owner { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        public class EmployeeConfig : IEntityTypeConfiguration<Project>
        {
            public void Configure(EntityTypeBuilder<Project> builder)
            {
                builder.Property(p => p.Name)
                    .HasMaxLength(40);

                builder.Property(s => s.Name)
                    .HasMaxLength(40);
                builder.Property(a => a.Timestamp)
                    .IsRowVersion();

                builder.HasData(
                       new Project() { 
                            ID = 1,
                            OwnerID = 1,
                            Name = "ReactApp",
                            TimeBudget = 1500,
                            Active = true,
                            Description = "Some React fullstack application"
                       },
                       new Project() 
                       {
                           ID = 2,
                           OwnerID = 2,
                           Name = "VueApp",
                           TimeBudget = 2200,
                           Active = true,
                           Description = "Some Vue.Js frontend application"
                       },
                       new Project()
                       {
                           ID = 3,
                           OwnerID = 4,
                           Name = "Spring Boot App",
                           TimeBudget = 1600,
                           Active = true,
                           Description = "Some Spring Boot backend application"
                       }                       
                    );
            }
        }
    }
}