using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAPICall.Services
{
    internal class ApiService
    {
        public bool CsvExportData(List<Models.ApiModel> data) {

            ExportCsv(data, "data");

            return true;
        }

        //public static class ExportData
        //{
        //    public static void ExportCsv<T>(List<T> genericList, string fileName)
        //    {
        //        var sb = new StringBuilder();
        //        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        //        var finalPath = Path.Combine("D:", fileName + ".csv");
        //        var header = "";
        //        var info = typeof(T).GetProperties();
        //        if (!File.Exists(finalPath))
        //        {
        //            var file = File.Create(finalPath);
        //            file.Close();
        //            foreach (var prop in typeof(T).GetProperties())
        //            {
        //                header += prop.Name + ", ";
        //            }
        //            header = header.Substring(0, header.Length - 2);
        //            sb.AppendLine(header);
        //            TextWriter sw = new StreamWriter(finalPath, true);
        //            sw.Write(sb.ToString());
        //            sw.Close();
        //        }
        //        foreach (var obj in genericList)
        //        {
        //            sb = new StringBuilder();
        //            var line = "";
        //            foreach (var prop in info)
        //            {
        //                line += prop.GetValue(obj, null) + ", ";
        //            }
        //            line = line.Substring(0, line.Length - 2);
        //            sb.AppendLine(line);
        //            TextWriter sw = new StreamWriter(finalPath, true);
        //            sw.Write(sb.ToString());
        //            sw.Close();
        //        }
        //    }
        //}

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
}
