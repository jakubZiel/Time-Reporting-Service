using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using project.Models.EntityFramework;

namespace project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : BaseController
    {
        public ProjectController(TRSDbContext database) : base(database) { }

    
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_database.Project.ToList());
        }

        [HttpPost]
        public  IActionResult Create(Project project)
        {
            project.Active = true;

            _database.Add(project);
            _database.SaveChanges();

            return Ok();
        }


        [HttpPut]
        public IActionResult Edit(Project body)
        {
             Project original = _database.Project.Find(body.ID);

            if (original is not null)
            {

                if (!body.Timestamp.SequenceEqual(original.Timestamp))
                {
                    return NotFound();
                }

                original.Name = body.Name;
                original.Active = body.Active;
                original.Description = body.Description;
                original.TimeBudget = body.TimeBudget;

                _database.Update(original);
                _database.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound();
            }
             
        }

        [HttpPut]
        [Route("newTag")]
        public IActionResult AddTag(int projectId, [FromBody] string newTag)
        {
            Project project = _database.Project.Find(projectId);

            if (project is null)
                return NotFound();

            if (project.Tags.Where(tag => tag.Name == newTag).Any())
                return BadRequest();

            project.Tags.Add(new Tag() { Name=newTag });
            return Ok();
        }
    }
}