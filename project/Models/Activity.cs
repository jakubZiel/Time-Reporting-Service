using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace  project.Models
{
    public class Activity
    {   
        public int id {get; set;}
        public DateTime dateCreated {get; set;}
        public int durationMinutes {get; set;}
        public string name {get; set;}
        public string projectId {get; set;}
        public bool active {get; set;}
        public string description  {get; set;}
        public string subActivity {get; set;}

        public Activity (int id, int durationMinutes, string name, string projectId, bool active, string description, string subActivity)
        {
            this.id = id;
            dateCreated = DateTime.Now;
            this.durationMinutes = durationMinutes;
            this.name = name;
            this.projectId = projectId;
            this.active = active;
            this.description = description;
            this.subActivity = subActivity;
        }

        public Activity()
        {
            dateCreated = System.DateTime.Now;
        }
    }  
}