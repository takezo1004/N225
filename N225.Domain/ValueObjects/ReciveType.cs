namespace N225.Domain.ValueObjects
{
    public sealed class ReciveType : ValueObject<ReciveType>
    {
        public static readonly ReciveType Expired = new ReciveType(3);
        public static readonly ReciveType OnOrder = new ReciveType(4);
        public static readonly ReciveType Cance = new ReciveType(6);
        public static readonly ReciveType Revocation = new ReciveType(7);
        public static readonly ReciveType Deals = new ReciveType(8);

        /// <summary>
        /// ValueObject ReciveType コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public ReciveType(int value)
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
                if (this == Expired)
                {
                    return "期限切";
                }
                else if (this == OnOrder)
                {
                    return "発注中";
                }
                else if (this == Cance)
                {
                    return "取消";
                }
                else if (this == Revocation)
                {
                    return "失効";
                }
                else if (this == Deals)
                {
                    return "約定";
                }
                return "無効";
            }
        }
        protected override bool EqualsCore(ReciveType other)
        {
            return this.Value == other.Value;
        }
    }
}
