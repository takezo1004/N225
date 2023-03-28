using N225.WinForm.TradeViewModels;
using System;

namespace N225.WinForm.Strategys
{
    public sealed class StrategyViewList : ListViewBase
    {
        string _dateTime = string.Empty;
        string _price = string.Empty;
        string _tradeType = string.Empty;
        string _side = string.Empty;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="name"></param>
        /// <param name="interval"></param>
        /// <param name="dateTime"></param>
        /// <param name="signale"></param>
        /// <param name="tradeType"></param>
        /// <param name="side"></param>
        /// <param name="description"></param>
        public StrategyViewList(string name, string interval, string dateTime = "", string tradeType = "",
                                                string side = "", string price = "", string description = "")
        {
            Name = name;
            Interval = interval;

            DateTime = dateTime;
            Price = price;
            TradeType = tradeType;
            Side = side;
            Description = description;


        }

        public string Name { get; set; }
        public string Interval { get; set; } = String.Empty;

        public string DateTime
        {
            get { return _dateTime; }
            set
            {
                SetPropety(ref _dateTime, value);
            }
        }

        public string TradeType
        {
            get { return _tradeType; }
            set
            {
                SetPropety(ref _tradeType, value);
            }
        }
        public string Side
        {
            get { return _side; }
            set
            {
                SetPropety(ref _side, value);
            }
        }
        public string Price
        {
            get { return _price; }
            set
            {
                SetPropety(ref _price, value);
            }
        }
        public string Description { get; set; } = String.Empty;
    }
}
