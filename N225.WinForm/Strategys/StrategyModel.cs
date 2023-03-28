using N225.Domain.Entities;
using N225.Domain.Exceptions;
using N225.Domain.StaticVlues;
using N225.WinForm.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace N225.WinForm.Strategys
{
    public class StrategyModel : ViewModelBase
    {
        string _name = String.Empty;
        string _interval = String.Empty;
        string _description = string.Empty;
        string _restoreName = String.Empty;
        string _restoreInterval = String.Empty;
        int _rowIndex;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public StrategyModel()
        {
            //ListView();
        }

        public string Name
        {
            get { return _name; }
            set
            {
                SetPropety(ref _name, value);
            }
        }

        public string Interval
        {
            get { return _interval; }
            set
            {
                SetPropety(ref _interval, value);
            }
        }

        public string DateTime { get; set; } = String.Empty;
        public string TradeType { get; set; } = String.Empty;
        public string Side { get; set; } = String.Empty;
        public string Price { get; set; } = String.Empty;
        public string Description
        {
            get { return _description; }
            set
            {
                SetPropety(ref _description, value);
            }
        }

        public BindingList<StrategyViewList> Views { get; set; } = new BindingList<StrategyViewList>();

        public string Username { get; set; }
        public string Password { get; set; }
        public string APIPassword { get; set; }

        /// <summary>
        /// StrategyDataGridに表示
        /// </summary>
        public void ListView()
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


                Views.Add(viewList);
            }

        }

        /// <summary>
        /// StrategyDataGridに１行表示
        /// </summary>
        public void AddView()
        {
            Valid();
            if (Name != _restoreName && string.IsNullOrEmpty(_restoreName) == false)
            {
                Views.RemoveAt(_rowIndex);
                StrategyListCash.Remove(_restoreName, Convert.ToInt32(_restoreInterval));
            }

            StrategyViewList list = new StrategyViewList(
                                        Name, Interval, Description);

            StrategyViewEntity listentity = new StrategyViewEntity()
            { Name = Name, Interval = Convert.ToInt32(Interval), Description = Description };
            //同じkeyでの2重登録の場合エラーとなる
            StrategyListCash.Add(listentity);
            Views.Add(list);

            Name = String.Empty;
            Interval = String.Empty;
            Description = String.Empty;
            _restoreName = String.Empty;
        }

        /// <summary>
        /// 選択行をプロパティに設定
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="name"></param>
        /// <param name="interval"></param>
        public void ReloadText(int rowIndex, string name, int interval)
        {
            _rowIndex = rowIndex;
            StrategyViewEntity entity = StrategyListCash.GetsEntity(name, interval);
            Name = entity.Name;
            _restoreName = entity.Name;
            Interval = entity.Interval.ToString();
            _restoreInterval = Interval;
            Description = entity.Description;
        }

        /// <summary>
        /// 一行削除
        /// </summary>
        /// <param name="row"></param>
        public void Remove(int row)
        {
            string name = Views.ElementAt(row).Name;
            int interval = Convert.ToInt32(Views.ElementAt(row).Interval);
            Views.RemoveAt(row);

            StrategyListCash.Remove(name, interval);
            Name = string.Empty;
            Interval = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// Checkマークの更新
        /// </summary>
        /// <param name="check"></param>
        /// <param name="name"></param>
        /// <param name="interval"></param>
        internal void UpdateCheck(bool check, string name, int interval)
        {
            StrategyListCash.UpdateCheck(check, name, interval);
        }

        /// <summary>
        /// Input data check
        /// </summary>
        /// <exception cref="KabuException"></exception>
        private void Valid()
        {
            if (string.IsNullOrEmpty(Name) == false && Convert.ToInt32(Interval) > 0)
            {
                return;
            }
            throw new KabuException("正しくデータを入力してください");
        }
    }
}
