using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace N225.Infrastrucure
{
    public static class Csv
    {
        public static void Writer<T>(List<T> records, string Path)
        {
 
            using (var writer = new StreamWriter(Path, false, Encoding.GetEncoding("Shift_JIS")))

            using (var csv = new CsvWriter(writer, new CultureInfo("ja-JP", false)))
            {
                csv.WriteRecords(records);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">からのListを引数とする</param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<T> Reader<T>(ref List<T> list, string path)
        { 
            if (System.IO.File.Exists(path) == false)
            {
                return new List<T>();
            }

 
            using (var reader = new StreamReader(path, Encoding.GetEncoding("Shift_JIS")))
            using (var csv = new CsvReader(reader, new CultureInfo("ja-JP", false)))
            { 
                return csv.GetRecords<T>().ToList();
                //return csv.GetRecords<T>().ToList();
            }
        }
    }
}
