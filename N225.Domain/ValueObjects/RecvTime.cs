namespace N225.Domain.ValueObjects
{
    public sealed class RecvTime : ValueObject<RecvTime>
    {
        /// <summary>
        /// ValueObject RecvTime コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public RecvTime(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public string DisplayValue
        {
            get
            {
                return Value.Split('.')[0].Replace('T', ' ').Replace('-', '/').Substring(2, 14);
            }
        }
        public string yyyyMMdd
        {
            get
            {
                var day = Value.Split('T')[0].Split('-');
                return day[0] + day[1] + day[2];
            }
        }

        protected override bool EqualsCore(RecvTime other)
        {
            //ここは各valueObjectで実装する
            return Value == other.Value;
        }
    }
}
