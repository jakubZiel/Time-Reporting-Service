using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models.EntityFramework
{
    public class EmployeeProject
    {
        public int ID { get; set; }
        public int ?EmployeeID { get; set; }

        public int ?ProjectID { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }
    }
}
