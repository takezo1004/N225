using System.Collections.Generic;

namespace N225.Domain.ValueObjects
{
    public sealed class TradeMode : ValueObject<TradeMode>
    {

        /// <summary>
        /// 手動
        /// </summary>
        public static readonly TradeMode Manual = new TradeMode(0);
        /// <summary>
        /// 自動
        /// </summary>
        public static readonly TradeMode Auto = new TradeMode(1);


        public TradeMode(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public string DisplayValue
        {
            get
            {
                // if文を上記コードを使って書き換える
                ///Valueでなくthisを使う
                if (this == Manual)
                {
                    return "手動";
                }
                return "自動";
            }
        }
        /// <summary>
        /// 比較する
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsAuto()
        {
            return this == Auto;
        }


        protected override bool EqualsCore(TradeMode other)
        {
            return this.Value == other.Value;
        }

        //４つの区分を返す関数を記述する
        public static List<TradeMode> ToList()
        {
            return new List<TradeMode>
            {
                //列挙体で記述
                Manual,
                Auto,
            };
        }
    }


}
