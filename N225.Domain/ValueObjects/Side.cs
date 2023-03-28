using System.Collections.Generic;

namespace N225.Domain.ValueObjects
{
    public sealed class Side : ValueObject<Side>
    {
        /// <summary>
        /// 売
        /// </summary>
        public static readonly Side Sell = new Side("1");
        /// <summary>
        /// 買
        /// </summary>
        public static readonly Side Buy = new Side("2");
        public Side(string value)
        {
            Value = value;
        }
        public string Value { get; }
        public string DisplayValue
        {
            get
            {
                // if文を上記コードを使って書き換える
                ///Valueでなくthisを使う
                if (this == Sell)
                {
                    return "売";
                }
                return "買";
            }
        }
        public bool IsBuy()
        {
            return this == Buy;
        }

        protected override bool EqualsCore(Side other)
        {
            return this.Value == other.Value;
        }

        //４つの区分を返す関数を記述する
        public static List<Side> ToList()
        {
            return new List<Side>
            {
                //列挙体で記述
                Sell,
                Buy,
             };

        }
    }
}
