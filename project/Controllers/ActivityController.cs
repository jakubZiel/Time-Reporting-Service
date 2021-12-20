using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using project.Models.EntityFramework;

namespace project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : BaseController
    {
        public ActivityController(TRSDbContext database ) : base(database) { }
        
        [HttpGet]
        public ActionResult<List<Activity>> All()
        {
            string data = HttpContext.Session.GetString(surnameSessionKey);
            ViewData["userInfo"] = data;

            return Ok(_database.Activity.ToArray());
        }

        public class Body
        {
            public DateTime Date { get; set; }
            public int Id { get; set; }
        }

        [HttpPost]
        [Route("day")]
        public ActionResult<List<Activity>> GetActivities([FromBody] Body body)
        {
            if (body is null)
                return NoContent();
            List<Activity> activities = _database.Activity
                .Where(a => a.EmployeeID == body.Id && body.Date.Date == a.DateCreated.Date)
                .ToList();
            return Ok(activities);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Activity activity)
        {
            DateTime month = DateTime.Today.Date;

            activity.Frozen = _database.Report.Where(r => r.Month.Date.Month == month.Month && r.Frozen && r.Month.Year == month.Year && r.EmployeeID == activity.EmployeeID).Any();
            activity.DateCreated = DateTime.Now.Date;


            if (!_database.Report.Where(r => r.Month.Month == month.Month && r.Month.Year == month.Year && r.EmployeeID == activity.EmployeeID).Any())
            {
                _database.Report.Add(new Report() { Month = month, Frozen = false, EmployeeID = activity.EmployeeID });
                _database.SaveChanges();
            }

            activity.ReportID = _database.Report.Where(r => r.Month.Month == month.Month && r.Month.Year == month.Year && r.EmployeeID == activity.EmployeeID).First().ID;

            _database.Activity.Add(activity);
            _database.SaveChanges();

            return Ok(activity);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Activity activity = _database.Activity.Find(id);

            if (activity is null)
            {
                return NotFound();             
            }

            _database.Activity.Remove(activity);
            _database.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Activity body)
        {
            Activity original = _database.Activity.Find(body.ID);

            if (original == null)
            {
                return NotFound();
            }
            if (!body.Timestamp.SequenceEqual(original.Timestamp))
            {
                return NotFound();
            }
            original.Name = body.Name;
            original.DurationMinutes = body.DurationMinutes;
            original.Description = body.Description;
            original.Tag = body.Tag;

            _database.Update(original);
            _database.SaveChanges();


            return Ok(original);
        }
    }
}