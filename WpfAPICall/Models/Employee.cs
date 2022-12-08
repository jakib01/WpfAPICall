using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAPICall.Models
{

    public class Datum
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("employee_name")]
        public string? EmployeeName;

        [JsonProperty("employee_salary")]
        public int? EmployeeSalary;

        [JsonProperty("employee_age")]
        public int? EmployeeAge;

        [JsonProperty("profile_image")]
        public string? ProfileImage;
    }

    public class Root
    {
        [JsonProperty("status")]
        public string? Status;

        [JsonProperty("data")]
        public List<Datum> Data;

        [JsonProperty("message")]
        public string? Message;
    }

}
