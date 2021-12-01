using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace project.Models.EntityFramework
{
    public class TRSDbContext : DbContext
    {
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<EmployeeProject> EmployeeProject { get; set; }
        
        public TRSDbContext(DbContextOptions<TRSDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(e => e.Employee)
                .WithMany(ep => ep.EmployeeProjects)
                .HasForeignKey(ei => ei.EmployeeID);

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(p => p.Project)
                .WithMany(ep => ep.EmployeeProjects)
                .HasForeignKey(pi => pi.ProjectID);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }  
}