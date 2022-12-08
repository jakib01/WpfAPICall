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
                    string[] lines = File.ReadAllLines(@"D:\data.csv");
                    string str = "";
                    foreach (string s in lines)
                    {
                        str = str + s + "\n";
                    }

                    StringBuilder sb = new StringBuilder();
                    using (var p = ChoCSVReader.LoadText(str).WithFirstLineHeader())
                    {
                        using (var w = new ChoJSONWriter(sb))w.Write(p);
                    }
                    System.IO.File.WriteAllText(@"D:\string.txt", sb.ToString());
                    //Root fromCsvJson = JsonConvert.DeserializeObject<Root>(sb.ToString());

                    //using (var reader = new StreamReader(@"D:\data.csv"))
                    //{
                    //    List<string> listA = new List<string>();
                    //    while (!reader.EndOfStream)
                    //    {
                    //        var line = reader.ReadLine();
                    //        var values = line.Split(',');

                    //        listA.Add(values[0]);
                    //    }
                    //}

                    //using (TextFieldParser parser = new TextFieldParser(@"D:\data.csv"))
                    //{
                    //    parser.TextFieldType = FieldType.Delimited;
                    //    parser.SetDelimiters(",");
                    //    while (!parser.EndOfData)
                    //    {
                    //        //Process row
                    //        string[] fields = parser.ReadFields();
                    //        foreach (string field in fields)
                    //        {
                    //        }
                    //    }
                    //}


                    //foreach (var item in myDeserializedJson.data)
                    //{
                    //    Console.WriteLine("id: {0}, name: {1}, salary: {2}", item.id, item.employee_name, item.employee_salary);
                    //}

                    //write string to file
                    //System.IO.File.WriteAllText(@"D:\path.csv", await response.Content.ReadAsStringAsync());
                    //massage.Content = "Success";
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

    }

}
