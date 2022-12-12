using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WpfAPICall.MainWindow;

namespace WpfAPICall.Models
{
    public class ApiModel
    {
        public int id { get; set; }
        public string employee_name { get; set; }
        public int employee_salary { get; set; }
        public int employee_age { get; set; }
        public string profile_image { get; set; }

    }

    public class ApiModelRoot
    {
        public string status { get; set; }
        public List<ApiModel> data { get; set; }
        public string message { get; set; }
    }


    public class CsvModel
    {
        public string id { get; set; }

        [JsonProperty(" employee_name")]
        public string employee_name { get; set; }

        [JsonProperty(" employee_salary")]
        public string employee_salary { get; set; }

        [JsonProperty(" employee_age")]
        public string employee_age { get; set; }

        [JsonProperty(" profile_image")]
        public string profile_image { get; set; }
    }

    public class CsvModelRoot
    {
        public List<CsvModel> data { get; set; }
    }


}
