using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
namespace project.Models.EntityFramework
{
    public class Activity
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }
        public int? ReportID { get; set; }

        public DateTime DateCreated { get; set; }
        public int? AcceptedTime { get; set; }
        public int DurationMinutes { get; set; }
        public string Name { get; set; }
        public bool Frozen { get; set; }
        public string? Description { get; set; }
        public string Tag { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        public virtual Project Project { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Report Report { get; set; }

        public class Config : IEntityTypeConfiguration<Activity>
        {
            public void Configure(EntityTypeBuilder<Activity> builder)
            {
                builder.Property(s => s.Tag)
                    .HasMaxLength(40);
                builder.Property(s => s.Name)
                    .HasMaxLength(40);
                builder.Property(a => a.DateCreated)
                    .HasDefaultValueSql("getdate()");
                builder.Property(a => a.Timestamp)
                    .IsRowVersion();

                builder.HasData(
                        new Activity() { ID = 1, ProjectID = 1, EmployeeID = 1, DurationMinutes = 30, Name = "API debugging", Description = "checking if everything is ok with the API", Tag = "debugging", DateCreated = DateTime.Now.Date}
                    );
            }
        }

    }
}
