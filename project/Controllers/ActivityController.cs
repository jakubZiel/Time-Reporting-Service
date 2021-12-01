using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using project.Models.Services;
using Microsoft.AspNetCore.Http;
using project.Models.EntityFramework;

namespace project.Controllers
{
    public class ActivityController : BaseController
    {
        private readonly TRSDbContext _database;
        public ActivityController(IContext context, TRSDbContext database ) : base(context)
        {
            _database = database;
        }

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
            
            return View(activity);
        }
        
        public IActionResult DeleteView(int id)
        {
            int employeeId = sessionToEmployeeId();

            Activity activity = _database.Activity.Where(a => a.ID == id).Single<Activity>();
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

            activity.Frozen = _database.Report.Where(r => r.month.Date == month.Date).Any();
            activity.DateCreated = DateTime.Now.Date;
            activity.EmployeeID = employeeId;

            _database.Activity.Add(activity);
            _database.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public IActionResult Delete(int id)
        {    
            int employeeId = sessionToEmployeeId();

            Activity activity =_database.Activity.Where(a => a.ID == id).Single<Activity>();

            _database.Activity.Remove(activity);

            _database.SaveChanges();

            return RedirectToAction("Index"); 
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Activity body)
        { 
            Activity original = _database.Activity.Find(body.ID);

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