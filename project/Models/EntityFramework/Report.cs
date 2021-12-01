using System;
using System.Collections.Generic;

namespace project.Models.EntityFramework
{
    public class Report
    {
        public int ID { get; set; }
        public int  ? EmployeeID { get; set; }
        public DateTime month { get; set; }
        
        public Employee Employee { get; set; }
        public ICollection<Activity> Reported { get; set; }
    }
}