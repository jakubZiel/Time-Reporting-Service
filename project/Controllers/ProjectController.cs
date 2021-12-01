using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using project.Models.Services;
using project.Models.EntityFramework;

namespace project.Controllers
{
    public class ProjectController : BaseController
    {
        private TRSDbContext _database;

        public ProjectController(IContext context, TRSDbContext database) : base(context)
        { 
            _database = database;
        }

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
                original.Name = project.Name;
                original.Active = project.Active;
                original.Description = project.Description;
                original.TimeBudget = project.TimeBudget;


                if (sub != null)
                    original.Tags.Add(new Tag() { ProjectID = original.ID, Name = sub}) ;

                _database.Update(original);
                _database.SaveChanges();
            }
            return RedirectToAction("EditView", new {id = original.ID});        
        }
    }
}