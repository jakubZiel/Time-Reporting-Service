using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using project.Models.EntityFramework;

namespace project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : BaseController
    {   
        private string reportIdSessionKey = "reportMonth";
        
        public ReportController(TRSDbContext database) : base(database) { }

        public class EmployeeSummary
        {
            public Dictionary<int, int> contributions { get; set; }
            public DateTime reportMonth { get; set; }
            public List<Project> projects { get; set; }
        }

        [HttpGet]
        public IActionResult EmployeeMonthSummary(int employeeId, DateTime reportMonth)
        {
            List<Project> projects = _database.Project
                .Where(p => p.Activities.Where(a => a.DateCreated.Year == reportMonth.Year && a.DateCreated.Month == reportMonth.Month && a.EmployeeID == employeeId).Any())
                .ToList();

            Dictionary<int, int> projectContributions = new Dictionary<int, int>();

            var groupedActivities = _database.Activity
                .Where(a => a.DateCreated.Year == reportMonth.Year && a.DateCreated.Month == reportMonth.Month)
                .ToList()
                .GroupBy(a => a.ProjectID)
                .ToList();

            groupedActivities.ForEach(group =>
            {
                projectContributions.Add(group.Key, group.Sum(a => a.DurationMinutes));
            });

            EmployeeSummary response = new EmployeeSummary()
            {
                contributions = projectContributions,
                projects = projects,
                reportMonth = reportMonth
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("affected_projects")]
        public IActionResult getEmployeesContributedActivities(int employeeId, int projectId)
        {
            List<Activity> activities = _database.Activity
                .Where(a => a.ProjectID == projectId && a.EmployeeID == employeeId)
                .ToList();
            
            return Ok(activities);
        }

        [HttpGet]
        [Route("owned_projects")]
        public IActionResult getEmployeesProjects(int employeeId)
        {
            List<Project> projects = _database.Project
                .Where(p => p.OwnerID == employeeId)
                .ToList();
            return Ok(projects);
        }

        public class ProjectSummary
        {
            public List<Activity> records { get; set; }
            public Project project { get; set; }
            public int usedBudget { get; set; }

        }

        [HttpGet]
        [Route("project_activities")]
        public IActionResult getProjectsActivities(int projectId)
        {
            Project project = _database.Project.Find(projectId);

            if (project is null)
                return NotFound();

            List<Activity> records = _database.Activity
                .Where(a => a.ProjectID == projectId)
                .ToList();

            return Ok(new ProjectSummary()
            {
                records = records,
                project = project,
                usedBudget = calculateProjectBudget(projectId)
            });
        }


        [HttpGet]
        [Route("reports")]
        public IActionResult getAllReports(int employeeId)
        {
            List<Report> reports = _database.Report
                .Where(r => r.EmployeeID == employeeId)
                .ToList();

            return Ok(reports);
        }

        [HttpGet]
        [Route("month_activities")]
        public IActionResult getMonthActivities(int reportId, int employeeId)
        {
            Report report = _database.Report.Find(reportId);

            if (report is null)
                return NotFound();

            List<Activity> activities = _database.Activity
                .Where(a =>
                a.DateCreated.Month == report.Month.Month &&
                a.DateCreated.Year == report.Month.Year &&
                a.EmployeeID == employeeId)
                .ToList();

            return Ok(activities);
        }

        [HttpPut]
        [Route("accept_record")]
        public IActionResult EditReportRecord(int activityId, int reportId, int newValue) 
        {
            Activity activity = _database.Activity.Find(activityId);
            if (activity is null)
            {
                return NotFound();
            }

            activity.AcceptedTime = newValue;
            _database.Activity.Update(activity);
            _database.SaveChanges();

            return Ok(activity);
        }

        [HttpPut]
        [Route("freeze")]
        public IActionResult Freeze(int reportId)
        {
            Report report = _database.Report.Find(reportId);

            if (!report.Frozen)
            {
                report.Frozen = true;
                _database.Report.Update(report);

                report.Reported
                    .ToList()
                    .ForEach(a =>
                    {
                        a.Frozen = true;
                        _database.Activity.Update(a);
                    });

                _database.SaveChanges();
            }
            return Ok();
        }

        private int calculateProjectBudget(int projectId)
        {
            int sum = 0;

            List<Activity> projectActivities = _database.Activity
                .Where(a => a.ProjectID == projectId)
                .ToList();

            projectActivities.ForEach(activity =>
            {
                if (activity.AcceptedTime != null)
                {
                    sum += activity.AcceptedTime.Value;
                }
            });

            return sum;
        }
    }
}