using System.Linq;
using lab1.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using lab1.Models;

namespace lab1.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string surnameSessionKey = "surname";
        
        protected Context _context;

        protected BaseController(IContext context)
        {
            _context = (Context) context;
        }

        protected int sessionToEmployeeId()
        {
            string data = HttpContext.Session.GetString(surnameSessionKey);
            return _context.employees.Find(emp => emp.surname == data).id;            
        }
        protected string findFile(int employeeId, int activityId)
        {    
            List<string> keys = _context.activities.Keys.ToList();

            string[] split = getFileName(employeeId, DateTime.Now.Date).Split('-');
            string pattern = split[0] + "-" + split[1] + "*";
            
            keys = keys.Where(key => Regex.Match(key, pattern).Success).ToList(); 

            string found = keys.Find(key => _context.activities[key].Find(activity => activity.id == activityId) != null);

            return found;
        }

        protected string getFileName(int employeeId, DateTime date)
        {
            Employee employee = _context.employees.Find(emp => emp.id == employeeId);
            return $"{employee.name}-{employee.surname}-{date.Month}-{date.Year}.json";
        }
    }
}