using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAPICall.Models;

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

                //var dataJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var dataJson = JsonConvert.DeserializeObject<List<Employee>>(await response.Content.ReadAsStringAsync());
                    //string json = JsonConvert.SerializeObject(_data.ToArray());

                    //write string to file
                    System.IO.File.WriteAllText(@"D:\path.txt", await response.Content.ReadAsStringAsync());
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
    }

}
