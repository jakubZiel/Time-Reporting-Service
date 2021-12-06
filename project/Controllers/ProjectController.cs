using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using project.Models.EntityFramework;

namespace project.Controllers
{
    public class ProjectController : BaseController
    {
        public ProjectController(TRSDbContext database) : base(database) { }

        public IActionResult Index()
        {
            int employeeId = sessionToEmployeeId();
            ViewData["employeeId"] = employeeId;

            return View(_database.Project.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult InspectView(int id)
        {
            ViewData["editable"] = false;
            Project project = _database.Project.Find(id);

            return View(project);
        }

        public IActionResult EditView(int id)
        {
            ViewData["editable"] = true;
            Project project = _database.Project.Find(id);

            if (project is null || !project.Active)
            {
                return RedirectToAction("Index");
            }
            
            return View("InspectView", project);
        }


        public IActionResult EditViewConc(int id)
        {
            Project project = _database.Project.Find(id);
            ViewData["editable"] = true;

            if (project == null)
            {
                ViewData["concurrency"] = "Project has been already deleted";
                return View("InspectView");
            }
  
            ViewData["concurrency"] = "Project has been already updated, but you can try to update it again.";
            return View("InspectView", project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            int employeeId = sessionToEmployeeId();
                        
            project.Active = true;            
            project.OwnerID = employeeId;
            
            _database.Add(project);
            _database.SaveChanges();

            return RedirectToAction("Index");           
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Project project, string sub)
        {
            Project original = _database.Project.Find(project.ID);

            if (original is not null)
            {

                if (!project.Timestamp.SequenceEqual(original.Timestamp))
                {
                    return RedirectToAction("EditViewConc", new { id = original.ID });
                }

                original.Name = project.Name;
                original.Active = project.Active;
                original.Description = project.Description;
                original.TimeBudget = project.TimeBudget;


                if (sub != null)
                    original.Tags.Add(new Tag() { ProjectID = original.ID, Name = sub}) ;

                _database.Update(original);
                _database.SaveChanges();

                return RedirectToAction("EditView", new { id = original.ID });
            }
            else
            {
                return RedirectToAction("EditViewConc", new { id = original.ID });
            }
             
        }
    }
}