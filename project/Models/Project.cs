using System.Collections.Generic;
namespace project.Models
{
    public class Project
    {
        public string id {get; set;}
        public string name {get; set;}
        public int ownerId {get; set;}
        public int timeBudget {get; set;}
        public bool active {get; set;}
        public string description {get; set;}
        public List<string> subActivities {get; set;} 
    
        public Project() {
            subActivities = new List<string>();
        }
    }
}