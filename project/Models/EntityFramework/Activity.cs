using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace project.Models.EntityFramework
{
    public class Activity
    {
        public int ID { get; set; }
        public int ? ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public int ?  ReportID { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime ? ReportedTime { get; set; }
        public DateTime ? AcceptedTime { get; set; }
        public int DurationMinutes {get; set;}
        public string Name {get; set;}
        public bool Frozen {get; set;}
        public string ? Description  {get; set;}
        public string Tag { get; set; }
        
        public Project Project { get; set; }
        public Employee Employee { get; set; }

        public class EmployeeConfig : IEntityTypeConfiguration<Activity>
        {
            public void Configure(EntityTypeBuilder<Activity> builder)
            {
                builder.Property(s => s.Tag)
                    .HasMaxLength(40);
                builder.Property(s => s.Name)
                    .HasMaxLength(40);
                builder.Property(a => a.DateCreated)
                    .HasDefaultValueSql("getdate()");

            }
        }

    }
}
