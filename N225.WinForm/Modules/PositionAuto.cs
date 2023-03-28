using N225.Domain.CommonConst;
using N225.Domain.Entities;
using N225.Domain.Exceptions;
using N225.Domain.Modules.Utils;
using N225.Infrastrucure;
using System.Collections.Generic;
using System.Linq;

namespace N225.WinForm.Modules
{
    public static class PositionAuto
    {
        /// <summary>
        /// 自動取引データ保存ディクショナリー
        /// </summary>
        public static Dictionary<string, PositionCsvItem> AutoList =
                                    new Dictionary<string, PositionCsvItem>();

        /// <summary>
        /// Csvファイルからデータ読み込みディクショナリーに保存
        /// </summary>
        /// <param name="path"></param>
        public static void CsvRead(string path = "N225/Csvfile/position.csv")
        {
            string dir = DirectoryUtils.LocalApplicationDataPath();
            path = dir + "/" + path;

            List<PositionCsvItem> list = new List<PositionCsvItem>();
            List<PositionCsvItem> rcordes = Csv.Reader(ref list, path);
            for (int i = 0; i < rcordes.Count; i++)
            {
                PositionCsvItem item = rcordes[i];
                string key = item.ExecutionID.ToString();
                AutoList.Add(key, item);
            }
        }

        /// <summary>
        /// ディクショナリーに保存とCsvファイルに保存
        /// </summary>
        /// <param name="item"></param>
        public static void Add(PositionCsvItem item)
        {
            string key = item.ExecutionID.ToString();
            AutoList.Add(key, item);

            ToCsv();
        }

        /// <summary>
        /// Csvファイルに保存
        /// </summary>
        /// <param name="path"></param>
        private static void ToCsv(string path = "N225/Csvfile/position.csv")
        {
            string dir = DirectoryUtils.LocalApplicationDataPath();
            path = dir + "/" + path;

            List<PositionCsvItem> Items = new List<PositionCsvItem>();
            foreach (KeyValuePair<string, PositionCsvItem> kvp in AutoList)
            {
                Items.Add(kvp.Value);
            }
            Csv.Writer(Items, path);
        }

        /// <summary>
        /// ディクショナリー要素削除
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="DataNotExistsException"></exception>
        public static void Remove(string key)
        {
            if (AutoList.ContainsKey(key))
            {
                AutoList.Remove(key);
                ToCsv();
            }
            else
            {
                throw new DataNotExistsException("PositionAuto: keyはありません");
            }
        }

        /// <summary>
        /// ディクショナリー要素取得
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static PositionCsvItem GetItem(string key)
        {
            if (AutoList.TryGetValue(key, out PositionCsvItem item))
            {
                return item;
            }
            else
            {
                return null;
            }
 
        }
    }
}
