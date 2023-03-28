using N225.Domain.Entities;
using N225.Domain.Modules.Utils;
using N225.Domain.StaticVlues;
using N225.Domain.ValueObjects;
using N225.WinForm.TradeViewModels;
using N225.Infrastrucure;
using N225.WinForm.Strategys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Threading;
using System.Linq;

namespace N225.WinForm.Modules
{
    public sealed class StrategyManeger
    {
        public static Dispatcher Dispatcher { get; set; } = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StrategyManeger()
        {

        }
        /// <summary>
        /// formにStrategyを表示する
        /// </summary>
        /// <param name="StrategyList"></param>
        public static void AddList(BindingList<StrategyViewList> StrategyList)
        {
            Dictionary<String, StrategyViewEntity> list =
                                           StrategyListCash.IsList();

            if (list == null)
            {
                return;
            }
            foreach (KeyValuePair<string, StrategyViewEntity> kvp in list)
            {
                var _value = kvp.Value;

                StrategyViewList viewList = new StrategyViewList(
                                _value.Name,
                                _value.Interval.ToString(),
                                _value.DateTime,
                                _value.TradeType,
                                _value.Side,
                                _value.Price,
                                _value.Description);

                //Dispatcher.Invoke(delegate ()
                //{
                StrategyList.Add(viewList);
                //});
            }
        }

        /// <summary>
        /// strategy signal 受信データを該当keyのgridviewに書きこみ表示する
        /// </summary>
        /// <param name="strategyViews"></param>
        /// <param name="fileld"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void UpDate(BindingList<StrategyViewList> strategyViews, InputOrder fileld)
        {
            int row = strategyViews.Count;

            Dispatcher.Invoke(delegate ()
            {
                for (int i = 0; i < row; i++)
                {
                    if (strategyViews.ElementAt(i).Name == fileld.Strategy &&
                        strategyViews.ElementAt(i).Interval == Convert.ToString(fileld.Interval))
                    {
                        strategyViews.ElementAt(i).DateTime = DateTime.Now.ToString("MM/dd HH:mm");
                        strategyViews.ElementAt(i).Side = new Domain.ValueObjects.Side(fileld.Side).DisplayValue;
                        strategyViews.ElementAt(i).TradeType = new CashMargin(fileld.TradeType + 1).DisplayValue;
                        strategyViews.ElementAt(i).Price = Convert.ToString(fileld.Price);

                    }
                }
            });
            //cash更新
            StrategyListCash.UpDate(fileld);
        }

        /// <summary>
        /// checkの取得
        /// </summary>
        /// <param name="strtegy"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static bool IsTrade(string strtegy, int interval)
        {
            return StrategyListCash.GetCheck(strtegy, interval);
        }

        internal static void TradeSignShow(string strtegy, int interval,
                    string dataTime, string signal, int tradeType, string side)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// CSVファイルから読み込みStratgyキャッシュに書き込む
        /// </summary>
        /// <param name="path"></param>
        public static void CsvRead(string path = "N225/Csvfile/Strategy.csv")
        {
            string dir = DirectoryUtils.LocalApplicationDataPath();
            path = dir + "/" + path;

            List<StrategyViewEntity> list = new List<StrategyViewEntity>();
            List<StrategyViewEntity> rcordes = Csv.Reader(ref list, path);
            for (int i = 0; i < rcordes.Count; i++)
            {
                StrategyViewEntity item = rcordes[i];
                StrategyListCash.Add(item);
            }
        }
        /// <summary>
        /// キャッシュデータをCSVファイルに保存する。
        /// </summary>
        /// <param name="path"></param>
        public static void SaveToCsv(string path = "N225/Csvfile/Strategy.csv")
        {
            string dir = DirectoryUtils.LocalApplicationDataPath();
            path = dir + "/" + path;

            List<StrategyViewEntity> Items = new List<StrategyViewEntity>();
            Dictionary<string, StrategyViewEntity> list = StrategyListCash.IsList();
            if (list != null)
            {
                foreach (KeyValuePair<string, StrategyViewEntity> kvp in list)
                {
                    Items.Add(kvp.Value);
                }
                Csv.Writer(Items, path);
            }
        }

        
    }
}
