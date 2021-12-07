using System;
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using project.Models.EntityFramework;
namespace project.Controllers
{   
    public class IdentityController : BaseController
    {
        public IdentityController(TRSDbContext database) : base(database) { }

        public IActionResult Index()
        {
            List<Employee> viewModel = _database.Employee.ToList();

            ViewData["Title"] = "Login";

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(int id, string password)
        {
            Employee user = _database.Employee.Find(id);
        
            if (user is null){

                ViewData["Title"] = "Wrong Credentials";
                List<Employee> viewModel = _database.Employee.ToList();

                return View("Index", viewModel);
            }
            
            HttpContext.Session.SetString(surnameSessionKey, user.Surname);
            HttpContext.Session.SetString("requestedDate", DateTime.Now.Date.ToString());
            HttpContext.Session.SetString("reportMonth", DateTime.Now.Date.ToString());

            return RedirectToAction("Index", "Activity");    
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return View();
        }   
    } 
}