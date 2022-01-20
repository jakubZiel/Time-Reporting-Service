using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace project.Models.EntityFramework
{
    public class Report
    {
        public int ID { get; set; }
        public int  ? EmployeeID { get; set; }
        public DateTime Month { get; set; }
        public bool Frozen { get; set; }
        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        public virtual ICollection<Activity> Reported { get; set; }
    }

}