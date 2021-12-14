using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using project.Models.EntityFramework;

namespace project.Controllers
{
    public class ActivityController : BaseController
    {
        public ActivityController(TRSDbContext database ) : base(database) { }

        public IActionResult Index(int id = 0)
        {   
            int employeeId = sessionToEmployeeId();
            
            string data = HttpContext.Session.GetString(surnameSessionKey);
            ViewData["userInfo"] = data;

            DateTime requestedDate = DateTime.Parse(HttpContext.Session.GetString("requestedDate"));
                    
            if (id != 1 || requestedDate != DateTime.Today.Date){    
                requestedDate = requestedDate.AddDays(id);
                HttpContext.Session.SetString("requestedDate", requestedDate.ToString());
            }

            ViewData["CurrentDate"] = requestedDate;
            
            List<Activity> todayActivities = _database.Activity
                .Where(a => a.EmployeeID == employeeId && requestedDate.Date == a.DateCreated.Date)
                .ToList();

            return View(todayActivities);
        }

        public IActionResult Create(int id)
        {
            Project project = _database.Project.Find(id);
            ViewData["project"] = project;

            return View();
        }
        
        public IActionResult EditView(int id)
        {    
            int employeeId = sessionToEmployeeId();       

            Activity activity = _database.Activity.Where(a => a.ID == id).Single<Activity>();


            if (activity == null)
                return NotFound();      
            
            Project project = _database.Project.Where(p => p.ID == activity.ProjectID).Single<Project>();
            
            ViewData["project"] = project;
            Console.WriteLine(activity.Timestamp[7]);
            return View(activity);
        }

        public IActionResult EditViewConc(int id)
        {
            Activity activity = _database.Activity.Find(id);

            if (activity == null)
            {
                ViewData["concurrency"] = "Activity has been already deleted";
                return View("EditView");
            }

            ViewData["concurrency"] = "Activity has been already updated, but you can try to update it again.";
            ViewData["project"] = activity.Project;
            return View("EditView", activity);
        }

        public IActionResult DeleteView(int id)
        {
            int employeeId = sessionToEmployeeId();

            Activity activity = _database.Activity.Find(id);
            if (activity is null){
                return NotFound();
            }
            
            return View(activity);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]   
        public IActionResult Create(Activity activity)
        {    

            int employeeId = sessionToEmployeeId();
            DateTime month = DateTime.Today.Date;

            activity.Frozen = _database.Report.Where(r => r.Month.Date.Month == month.Month && r.Frozen && r.Month.Year == month.Year && r.EmployeeID == employeeId).Any();
            activity.DateCreated = DateTime.Now.Date;
            activity.EmployeeID = employeeId;


            if (!_database.Report.Where(r => r.Month.Month == month.Month && r.Month.Year == month.Year && r.EmployeeID == employeeId).Any())
            {
                _database.Report.Add(new Report() { Month = month, Frozen = false, EmployeeID = employeeId });
                _database.SaveChanges();
            }

            activity.ReportID = _database.Report.Where(r => r.Month.Month == month.Month && r.Month.Year == month.Year && r.EmployeeID == employeeId).First().ID;

            _database.Activity.Add(activity);
            _database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public IActionResult Delete(int id)
        {    
            Activity activity = _database.Activity.Find(id);

            if (activity is null)
            {
                ViewData["concurrency"] = "Activity has been already delted";
                return View("DeleteView", null);
            }
            _database.Activity.Remove(activity);
            _database.SaveChanges();

            return RedirectToAction("Index"); 
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Activity body)
        { 
            Activity original = _database.Activity.Find(body.ID);

            if (original == null)
            {
                return RedirectToAction("EditViewConc", new { id = body.ID });
            }
            if (!body.Timestamp.SequenceEqual(original.Timestamp))
            {
                return RedirectToAction("EditViewConc", new { id = original.ID });
            }
            original.Name = body.Name;
            original.DurationMinutes = body.DurationMinutes;
            original.Description = body.Description;
            original.Tag = body.Tag;

            _database.Update(original);
            _database.SaveChanges();
        

            return RedirectToAction("Index");   
        }
    }
}