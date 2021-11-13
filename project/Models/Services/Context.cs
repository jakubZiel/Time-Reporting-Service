using System.Collections;
using System;
using System.Collections.Generic;
using project.Models;
using Newtonsoft.Json;
using System.IO;

namespace project.Models.Services
{   
    public interface IContext
    {
        public Dictionary<string, T> loadHash <T>(string directoryPath);
        public List<T> load<T>(string directoryPath);
    }

    public class Context : IContext
    {   
        private int activitiesSeq;
        private int projectSeq;
        private int employeeSeq;
        public Dictionary<string, List<Activity>> activities;
        public Dictionary<string, Report> reports;
        public List<Project> projects;
        public List<Employee> employees;
        private string basePath = Environment.CurrentDirectory + "/DataBase";
        private readonly string activitiesDataBaseFilePath = "/Activities";
        private readonly string projectsDataBaseDirectory = "/Projects";
        private readonly string employeesDataBaseDirectory = "/Employees";
        private readonly string reportsDataBaseDirectory = "/Reports";
        
        public Context()
        {
            load();
        }

        public void load()
        {
            employees = load<Employee>(basePath + employeesDataBaseDirectory);
            employeeSeq = loadSequence(basePath + employeesDataBaseDirectory + "/contextSeq");

            projects = load<Project>(basePath + projectsDataBaseDirectory);
            projectSeq = loadSequence(basePath + projectsDataBaseDirectory + "/contextSeq");

            reports = loadHash<Report>(basePath + reportsDataBaseDirectory);

            activities = loadHash<List<Activity>>(basePath + activitiesDataBaseFilePath + "/shards");
            activitiesSeq = loadSequence(basePath + activitiesDataBaseFilePath + "/contextSeq");
        }
        
        public List<T> load<T>(string directoryPath)
        {
            string data = File.ReadAllText(directoryPath + "/" + "base.json");
            List<T> load = JsonConvert.DeserializeObject<List<T>>(data);
            return load;
        }

        int loadSequence(string filePath)
        { 
            string seqData = File.ReadAllText(filePath);
            int sequence = Int32.Parse(seqData);            
            return sequence;
        }
        
        public Dictionary<string, T> loadHash<T>(string directoryPath)
        {
            DirectoryInfo directory = new DirectoryInfo(directoryPath);      
            
            List<FileInfo> files = new List<FileInfo>(directory.GetFiles());
            List<string> fileNames = files.ConvertAll(file => file.Name);

            Dictionary<string, T> hash = new Dictionary<string, T>();
            
            fileNames.ForEach(fileName => {
                string data = File.ReadAllText(directoryPath + "/" + fileName);

                T records = JsonConvert.DeserializeObject<T>(data);
                hash.Add(fileName, records);
            });
            return hash;     
        }
        public void saveActivities(string fileName)
        {
            string data = JsonConvert.SerializeObject(activities[fileName], Formatting.Indented);

            File.WriteAllText(basePath + activitiesDataBaseFilePath + "/shards/" + fileName, data);
            File.WriteAllText(basePath + activitiesDataBaseFilePath + "/contextSeq", activitiesSeq.ToString());
        }
        
        public void saveReport(string reportFile)
        {
            string filePath = basePath + reportsDataBaseDirectory + "/" +  reportFile;

            Report report = reports[reportFile];
            string data = JsonConvert.SerializeObject(report, Formatting.Indented);

            File.WriteAllText(filePath, data);           
        }

        public void saveProjects()
        {
            string data = JsonConvert.SerializeObject(projects, Formatting.Indented);   
            File.WriteAllText(basePath + projectsDataBaseDirectory + "/base.json", data);
            File.WriteAllText(basePath + projectsDataBaseDirectory + "/contextSeq", projectSeq.ToString());
        }

        public void add(Activity activity, string fileName)
        {
            activity.id = activitiesSeq;

            if (activities.ContainsKey(fileName))
                activities[fileName].Add(activity);
            else{
                List<Activity> list = new List<Activity>();
                list.Add(activity);
                activities.Add(fileName, list);
            }
            activitiesSeq++;
        }

        public void add(Project project)
        {
            project.id = "project" + projectSeq;
            projectSeq++;
            projects.Add(project);
        }

        public void add(Report report)
        {
            if (!reports.ContainsKey(report.origin))
            {
                reports.Add(report.origin, report);
            }
        }
    }
}