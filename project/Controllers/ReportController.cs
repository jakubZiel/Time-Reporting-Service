using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using project.Models.Services;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using project.Models.EntityFramework;

namespace project.Controllers
{
    public class ReportController : BaseController
    {   
        private string reportIdSessionKey = "reportMonth";
        
        private readonly TRSDbContext _database;
        
        public ReportController(IContext context, TRSDbContext database) : base(context)
        {
            _database = database;
        }

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

            string reportName = getFileName(employeeId, reportMonth);
            Report report = _database.Report.Where(r => r.Month == reportMonth).Single();

            HashSet<string> projectIds = new HashSet<string>();

            if (report is null)
                return View(null);


            List<Project> projects = _database.Project.ToList();

            Dictionary<int, int> projectContributions = new Dictionary<int, int>();

            List<IGrouping<int, Activity>> groupedActivites = _database.Activity
                .Where(a => a.DateCreated == reportMonth)
                .GroupBy(a => a.ProjectID)
                .ToList();

            groupedActivites.ForEach(group =>
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

            string fileName = getFileName(employeeId, month);

            List<Activity> activities = _database.Activity
                .Where(a => a.ProjectID == projectId)
                .ToList();

        
            try {
                ViewData["report"] = activities.Select(a => new AcceptedRecord(a.ID, a.));

            }catch{
                ViewData["report"] = null;
            }
            ViewData["projectId"] = projectId;
            ViewData["isProjectActive"] = _context.projects.Find(proj => proj.id == projectId).active;
            
            return View(activities);
        }

        public IActionResult ManageReports()
        {
            int employeeId = sessionToEmployeeId();
            ViewData["Title"] = "Managed Projects";
            
            ViewData["reportMonth"] = DateTime.Parse(HttpContext.Session.GetString(reportIdSessionKey));

            return View("Index", _database.Project.Where(project => project.OwnerID == employeeId).ToList());
        }

        public IActionResult InspectReports(string projectId)
        {    
            List<string> keys = _context.activities.Keys.ToList();

            List<Activity> records = new List<Activity>();
            List<string> recordKeys = new List<string>();

            keys.ForEach(key => {
                
                List<Activity> partialRecords = _context.activities[key]
                    .Where(act => act.projectId == projectId)
                    .ToList();
                
                partialRecords.ForEach(act => recordKeys.Add(key));
                records = records.Concat(partialRecords).ToList();
            });
            
            List<AcceptedRecord> acceptedRecords = new List<AcceptedRecord>();

            keys.ForEach(key => {
                if (_context.reports.ContainsKey(key)){
                    acceptedRecords =  acceptedRecords.Concat(_context.reports[key].accepted).ToList();
                }
            });

            ViewData["report"] = acceptedRecords;
            ViewData["budget"] = _context.projects.Find(project => project.id == projectId).timeBudget;
            ViewData["usedBudget"] = calculateProjectBudget(projectId);
            ViewData["recordKeys"] = recordKeys;
            ViewData["projectId"] = projectId;
            
            List<bool> frozenRecords = new List<bool>();

            for (int i = 0; i < recordKeys.Count; i++){
                if (_context.reports.ContainsKey(recordKeys[i]))
                    frozenRecords.Add(_context.reports[recordKeys[i]].frozen);
                else 
                    frozenRecords.Add(false);
            }

            ViewData["frozenRecords"] = frozenRecords;
            ViewData["isProjectActive"] = _context.projects.Find(proj => proj.id == projectId).active;
            
             return View("Inspect", records);  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditReport(int activityId, string reportId, int newValue)
        {   
            Report report = _context.reports[reportId];

            int index = report.accepted.FindIndex(record => record.id == activityId);

            if (index > 0)
                report.accepted[index].time = newValue; 
            else 
                report.accepted.Add(new AcceptedRecord(activityId, newValue));

            _context.saveReport(reportId);

            string projId = _context.activities[reportId].Find(act => act.id == activityId).projectId;
            
            return RedirectToAction("InspectReports", new {projectId = projId});
        }
    
        public IActionResult CheckReports()
        {
            List<string> keys = _context.activities.Keys.ToList();

            int employeeId = sessionToEmployeeId();
            DateTime month = DateTime.Parse(HttpContext.Session.GetString(reportIdSessionKey));

            string fileName = getFileName(employeeId, month);
            
            List<string> split = fileName.Split('-').ToList();
            string prefix = split[0] + "-" + split[1] + "*";

            keys = keys.Where(key => Regex.Match(key, prefix).Success).ToList(); 

            List<Tuple<bool, string>> reportPairs = new List<Tuple<bool, string>>();

            keys.ForEach(key => {
                if (_context.reports.ContainsKey(key))
                    reportPairs.Add(new Tuple<bool, string>(true, key));
                else
                    reportPairs.Add(new Tuple<bool, string>(false, key));
            });

            return View(reportPairs);
        }

        public IActionResult InspectMonth(string report)
        {
            try {
                ViewData["report"] = _context.reports[report].accepted;

            }catch{
                ViewData["report"] = null;
            }
            ViewData["projectId"] = report;
            
            return View("Inspect", _context.activities[report]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Freeze(string reportId){
            if (!_context.reports.ContainsKey(reportId))
            {
                _context.add(new Report(true, reportId, new List<AcceptedRecord>()));
                _context.activities[reportId].ForEach(activity =>
                {
                    activity.active = false;
                });
                _context.saveActivities(reportId);
                
                _context.saveReport(reportId);
            }
            return RedirectToAction("CheckReports");
        }

        private int calculateProjectBudget(string projectId)
        {    
            List<string> reports = _context.reports.Keys.ToList();
            int sum = 0;

            reports.ForEach(report => {
                
                _context.reports[report].accepted.ForEach(record => {
                    var found =_context.activities[report].Find(activity => activity.id == record.id);
                    if (found != null && found.projectId == projectId)
                        sum += record.time;
                });
            });
            return sum;
        }
    }
}