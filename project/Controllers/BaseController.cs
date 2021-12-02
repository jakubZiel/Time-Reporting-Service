using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using project.Models.EntityFramework;

namespace project.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string surnameSessionKey = "surname";
        
        protected readonly TRSDbContext _database;
        protected BaseController(TRSDbContext database)
        {
            _database = database;
        }

        protected int sessionToEmployeeId()
        {
            string data = HttpContext.Session.GetString(surnameSessionKey);
            return _database.Employee.Where(e => e.Surname == data).Single().ID;          
        }
    }
}