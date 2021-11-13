using System;
using Microsoft.AspNetCore.Mvc;
using project.Models.Services;
using project.Models;

namespace project.Controllers
{
    public class ProjectController : BaseController
    {        
        public ProjectController(IContext context) : base(context){}

        public IActionResult Index()
        {

            int employeeId = sessionToEmployeeId();
            ViewData["employeeId"] = employeeId;

            return View(_context.projects);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult InspectView(string id)
        {
            ViewData["editable"] = false;
            Project project = _context.projects.Find(project => project.id == id);
            
            return View(project);
        }

        public IActionResult EditView(string id)
        {
            ViewData["editable"] = true;
            Project project = _context.projects.Find(project => project.id == id);

            if (project is null || !project.active)
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
                        
            project.active = true;            
            project.ownerId = employeeId;
            
            _context.add(project);
            _context.saveProjects();

            return RedirectToAction("Index");           
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Project project, string sub)
        {
            int index =_context.projects.FindIndex(proj => proj.id == project.id);

            if (index >= 0)
            {
                project.subActivities = _context.projects[index].subActivities;
                
                if (sub != null)
                    project.subActivities.Add(sub);
                
                _context.projects[index] = project;
                _context.saveProjects();
            }
            return RedirectToAction("EditView", new {id = project.id});        
        }
    }
}