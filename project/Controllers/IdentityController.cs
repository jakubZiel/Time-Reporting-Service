using System;
using System.Net;
using lab1.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using lab1.Models;
using Microsoft.AspNetCore.Http;

namespace lab1.Controllers
{   
    public class IdentityController : BaseController
    {
        public IdentityController(IContext context) : base(context){}

        public IActionResult Index()
        {
            List<EmployeeView> viewModel = _context.employees.ConvertAll(employee => new EmployeeView(employee));

            ViewData["Title"] = "Login";

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(int id, string password)
        {
            Employee user = _context.employees.Find(employee => employee.id == id && employee.password == password);
        
            if (user is null){

                ViewData["Title"] = "Wrong Credentials";
                List<EmployeeView> viewModel = _context.employees.ConvertAll(employee => new EmployeeView(employee));

                return View("Index", viewModel);
            }
            
            HttpContext.Session.SetString(surnameSessionKey, user.surname);
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