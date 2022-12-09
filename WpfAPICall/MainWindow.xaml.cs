
using ChoETL;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAPICall.Models;
using Path = System.IO.Path;

namespace WpfAPICall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://dummy.restapiexample.com/api/v1/employees");
                var status = response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var dataJson = await response.Content.ReadAsStringAsync();

                    Root myDeserializedJson = JsonConvert.DeserializeObject<Root>(dataJson);

                    lvUsers.ItemsSource = myDeserializedJson.data;

                    ExportData.ExportCsv(myDeserializedJson.data, "data");


                    var csv = new List<string[]>();
                    var lines = System.IO.File.ReadAllLines(@"D:data.csv"); // csv file location

                    // loop through all lines and add it in list as string
                    foreach (string line in lines)
                        csv.Add(line.Split(','));

                    //split string to get first line, header line as JSON properties
                    var properties = lines[0].Split(',');

                    var listObjResult = new List<Dictionary<string, string>>();

                    //loop all remaining lines, except header so starting it from 1
                    // instead of 0
                    for (int i = 1; i < lines.Length; i++)
                    {
                        var objResult = new Dictionary<string, string>();
                        for (int j = 0; j < properties.Length; j++)
                            objResult.Add(properties[j], csv[i][j]);

                        listObjResult.Add(objResult);
                    }

                    var collectionWrapper = new
                    {

                        data = listObjResult

                    };

                    // convert dictionary into JSON
                    var json = JsonConvert.SerializeObject(collectionWrapper);

                    Root1 myDeserializedClass = JsonConvert.DeserializeObject<Root1>(json);

                    lvUsers1.ItemsSource = myDeserializedClass.data;
                }
                else
                {
                    //massage.Content = $"Server error code{response.StatusCode}";
                }
            }
        }


        private void Generator_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {

        }

        public static class ExportData
        {
            public static void ExportCsv<T>(List<T> genericList, string fileName)
            {
                var sb = new StringBuilder();
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var finalPath = Path.Combine("D:", fileName + ".csv");
                var header = "";
                var info = typeof(T).GetProperties();
                if (!File.Exists(finalPath))
                {
                    var file = File.Create(finalPath);
                    file.Close();
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        header += prop.Name + ", ";
                    }
                    header = header.Substring(0, header.Length - 2);
                    sb.AppendLine(header);
                    TextWriter sw = new StreamWriter(finalPath, true);
                    sw.Write(sb.ToString());
                    sw.Close();
                }
                foreach (var obj in genericList)
                {
                    sb = new StringBuilder();
                    var line = "";
                    foreach (var prop in info)
                    {
                        line += prop.GetValue(obj, null) + ", ";
                    }
                    line = line.Substring(0, line.Length - 2);
                    sb.AppendLine(line);
                    TextWriter sw = new StreamWriter(finalPath, true);
                    sw.Write(sb.ToString());
                    sw.Close();
                }
            }
        }

        public class Datum
        {
            public int id { get; set; }
            public string employee_name { get; set; }
            public int employee_salary { get; set; }
            public int employee_age { get; set; }
            public string profile_image { get; set; }
        }

        public class Root
        {
            public string status { get; set; }
            public List<Datum> data { get; set; }
            public string message { get; set; }
        }

        
        public class Datum1
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

        public class Root1
        {
            public List<Datum1> data { get; set; }
        }

    }

}
