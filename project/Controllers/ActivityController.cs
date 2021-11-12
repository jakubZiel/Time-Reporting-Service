using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using lab1.Models.Services;
using lab1.Models;
using Microsoft.AspNetCore.Http;

namespace lab1.Controllers
{
    public class ActivityController : BaseController
    {
        public ActivityController(IContext context) : base(context){}

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
            
            if (_context.activities.ContainsKey(getFileName(employeeId, requestedDate))){
                
                List<Activity> todayActivities = 
                        _context.activities[getFileName(employeeId, requestedDate)]
                        .Where(activity => activity.dateCreated.Date == requestedDate.Date).ToList();
                
                return View(todayActivities);
            }else{

                List<Activity> todayActivities = new List<Activity>();
                return View(todayActivities);
            }
        }

        public IActionResult Create(string id = null)
        {
            ViewData["project"] = _context.projects.Find(project => project.id == id);

            return View();
        }
        
        public IActionResult EditView(int id)
        {    
            int employeeId = sessionToEmployeeId();       

            Activity activity = _context.activities[findFile(employeeId, id)].Find(act => act.id == id);

            if (activity == null)
                return NotFound();      
            
            Project project = _context.projects.Find(project =>  activity.projectId == project.id);
            ViewData["project"] = project;
            
            return View(activity);
        }
        
        public IActionResult DeleteView(int id)
        {
            int employeeId = sessionToEmployeeId();

            string fileName = findFile(employeeId, id);

            Activity activity = _context.activities[fileName].Find(act => act.id == id);

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

            activity.active = !_context.reports.ContainsKey(getFileName(employeeId, DateTime.Now.Date));

            _context.add(activity, getFileName(employeeId, month));
            _context.saveActivities(getFileName(employeeId, month));

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public IActionResult Delete(int id)
        {    
            int employeeId = sessionToEmployeeId();
            
            string fileName = findFile(employeeId, id);

            Activity activity = 
                _context.activities[fileName]
                .Find(activity => activity.id == id);   
            
            _context.activities[fileName]
                .Remove(activity);

            _context.saveActivities(fileName);

            return RedirectToAction("Index"); 
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Activity body)
        {
            int employeeId = sessionToEmployeeId();
            
            string fileName = findFile(employeeId, body.id);
            int index = _context.activities[fileName].FindIndex(act => act.id == body.id);

            if ( index != -1){ 
                _context.activities[fileName][index] = body; 
                _context.saveActivities(fileName);        
            }
            return RedirectToAction("Index");   
        }
    }
}