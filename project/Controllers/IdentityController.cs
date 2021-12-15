using System;
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using project.Models.EntityFramework;

namespace project.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : BaseController
    {
        public IdentityController(TRSDbContext database) : base(database) { }
     
        [HttpGet]
        public ActionResult<List<Employee>> getEmployees()
        {
            return Ok(_database.Employee.ToList());
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(int id, string password)
        {
            Employee user = _database.Employee.Find(id);

            if (user is null)
                return NotFound();

            if (user.Password != password)
            {
                return NotFound();
            }

            HttpContext.Session.SetString(surnameSessionKey, user.Surname);
            HttpContext.Session.SetString(requestedDateKey, DateTime.Now.Date.ToString());
            HttpContext.Session.SetString(reportMonthKey, DateTime.Now.Date.ToString());
            HttpContext.Session.SetString(employeeIdSessionKey, user.ID.ToString());

            return Ok();
        }
        
        /*
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(int id, string password)
        {
            Employee user = _database.Employee.Find(id);

            if (user is null)
                return NotFound();

            if (user.Password != password){

                ViewData["Title"] = "Wrong Credentials";
                List<Employee> viewModel = _database.Employee.ToList();

                return View("Index", viewModel);
            }
            
            HttpContext.Session.SetString(surnameSessionKey, user.Surname);
            HttpContext.Session.SetString(requestedDateKey, DateTime.Now.Date.ToString());
            HttpContext.Session.SetString(reportMonthKey, DateTime.Now.Date.ToString());
            HttpContext.Session.SetString(employeeIdSessionKey, user.ID.ToString());

            return RedirectToAction("Index", "Activity");    
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return View();
        }
        */
    } 
}