using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAPICall.Models
{
    public class employee
    {
        public List<Employee> Employee { get; set; }
    }

    public class Employee
    {
        //public int Id { get; set; }
        //public string? Name { get; set; }
        //public string? Salary { get; set; }
        //public string? Age { get; set; }


        public int Id { get; set; }
        public string? employee_name { get; set; }
        public string? employee_salary { get; set; }
        public string? employee_age { get; set; }
        public string? profile_image { get; set; }

    }
}
