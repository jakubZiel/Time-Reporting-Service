using System.IO;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using project.Models.Services;

namespace project.Models
{
    public class Report
    {
        public bool frozen;
        public string origin;
        public List<AcceptedRecord> accepted;
    
        public Report(bool frozen, string origin, List<AcceptedRecord> accepted)
        {
            this.frozen = frozen;
            this.origin = origin;
            this.accepted = accepted;
        }

        public Report()
        {
            accepted = new List<AcceptedRecord>();
        }
    }
    
    public class AcceptedRecord
    {
        public int id;
        public int time;

        public AcceptedRecord(int id, int time){
            this.id = id;
            this.time = time;
        }    
    }
}