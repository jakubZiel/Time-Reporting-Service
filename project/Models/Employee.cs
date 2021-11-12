namespace lab1.Models
{
    public class Employee : EmployeeView
    {
        public string password {get; set;}      

        public Employee(int id, string name, string surname, string password)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.password = password;
        }
    }

    public class EmployeeView
    {
        public int id {get; set;}
        public string name {get; set;}
        public string surname {get; set;}

        public EmployeeView(Employee employee){
            id = employee.id;
            name = employee.name;
            surname = employee.surname;
        }

        public EmployeeView() {}
    }
}


