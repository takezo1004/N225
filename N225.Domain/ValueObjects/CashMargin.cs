namespace N225.Domain.ValueObjects
{
    /// <summary>
    /// ValueObject CashMargin
    /// </summary>
    public sealed class CashMargin : ValueObject<CashMargin>
    {
        public static readonly CashMargin New = new CashMargin(2);

        public static readonly CashMargin Exit = new CashMargin(3);
        public CashMargin(int value)
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
                if (this == New)
                {
                    return "新規";
                }
                return "返済";
            }
        }

        protected override bool EqualsCore(CashMargin other)
        {
            return this.Value == other.Value;
        }
    }
}
