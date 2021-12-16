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
        /*
        public IActionResult Index(int nextMonth = 0)
        {
            ViewData["Title"] = "Affected Projects";
            int employeeId = sessionToEmployeeId();
            DateTime reportMonth = DateTime.Parse(HttpContext.Session.GetString(reportIdSessionKey));

            if (nextMonth != 1 || reportMonth.Date.Year != DateTime.Now.Year || reportMonth.Date.Month != DateTime.Now.Month)
            {
                reportMonth = reportMonth.AddMonths(nextMonth);
                HttpContext.Session.SetString(reportIdSessionKey, reportMonth.ToString());
            }

            ViewData[reportIdSessionKey] = reportMonth;

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

            ViewData["contributions"] = projectContributions;
                
            return View(projects);            
        }

        public IActionResult Inspect(int projectId)
        {   
            int employeeId = sessionToEmployeeId();
            DateTime month = DateTime.Parse(HttpContext.Session.GetString(reportIdSessionKey));

            Project project = _database.Project.Find(projectId);

            List<Activity> activities = _database.Activity
                .Where(a => a.ProjectID == projectId && a.EmployeeID == employeeId)
                .ToList();

            ViewData["projectId"] = project.Name;
            ViewData["isProjectActive"] = project.Active;
            
            return View(activities);
        }

        public IActionResult ManageReports()
        {
            int employeeId = sessionToEmployeeId();
            ViewData["Title"] = "Managed Projects";
            
            ViewData["reportMonth"] = DateTime.Parse(HttpContext.Session.GetString(reportIdSessionKey));

            return View("Index", _database.Project.Where(project => project.OwnerID == employeeId).ToList());
        }

        public IActionResult InspectReports(int projectId)
        {
            List<Activity> records = _database.Activity
                .Where(a => a.ProjectID == projectId)
                .ToList();
            
            List<string> recordKeys = new List<string>();
            Project project = _database.Project.Find(projectId);
            
            ViewData["budget"] = project.TimeBudget;
            ViewData["usedBudget"] = calculateProjectBudget(projectId);
            ViewData["recordKeys"] = recordKeys;
            ViewData["projectId"] = project.Name;
            ViewData["isProjectActive"] = project.Active;
            
            return View("Inspect", records);  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditReport(int activityId, int reportId, int newValue)
        {   
            Activity activity = _database.Activity.Find(activityId);
            
            if (activity is not null)
            {
                activity.AcceptedTime = newValue;
                _database.Activity.Update(activity);
                _database.SaveChanges();
            }

            return RedirectToAction("InspectReports", new {projectId = activity.ProjectID});
        }
    
        public IActionResult CheckReports()
        {
            int employeeId = sessionToEmployeeId();
            DateTime month = DateTime.Parse(HttpContext.Session.GetString(reportIdSessionKey));

            List<Report> reports = _database.Report
                .Where(r => r.EmployeeID == employeeId)
                .ToList();

            return View(reports);
        }

        public IActionResult InspectMonth(int reportId)
        {
            Report report = _database.Report.Find(reportId);
            int employeeId = sessionToEmployeeId();

            ViewData["projectId"] = report.Month.ToString("MM/yyyy");
                
            return View("Inspect", _database.Activity.Where(a => a.DateCreated.Month == report.Month.Month && a.DateCreated.Year == report.Month.Year && a.EmployeeID == employeeId).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Freeze(int reportId){

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
            return RedirectToAction("CheckReports");
        }
        */
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