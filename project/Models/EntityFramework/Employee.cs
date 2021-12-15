using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models.EntityFramework
{
    public class Employee
    {
      
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID {get; set;}
        public string Name {get; set; }
        public string Surname {get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public virtual ICollection<Activity> Activities { get; set; }
        [JsonIgnore]
        public virtual ICollection<Report> Reports { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; }
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

            builder.HasData(
                    new Employee()
                    {
                        ID = 1,
                        Name = "Jakub",
                        Surname = "Zielinski",
                        Password = "123"
                    },
                    new Employee()
                    {
                        ID = 2,
                        Name = "Piotr",
                        Surname = "Lewandowski",
                        Password = "1234"
                    },
                    new Employee()
                    {
                        ID = 3,
                        Name = "Waldemar",
                        Surname = "Grabski",
                        Password = "12345"
                    },
                    new Employee()
                    {
                        ID = 4,
                        Name = "Krzysztof",
                        Surname = "Chabko",
                        Password = "123456"
                    }
                );
            
    
        }
    }
}