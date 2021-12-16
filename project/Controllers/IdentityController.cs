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
        public IActionResult All()
        {
            return Ok(_database.Employee.ToList());
        }

        [HttpGet]
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
    } 
}