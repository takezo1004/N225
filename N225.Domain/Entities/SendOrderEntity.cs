namespace N225.Domain.Repositories
{
    /// <summary>
    /// 注文API用新規・返済注文データフィールド
    /// </summary>
    public class SendOrderEntity
    {
        public SendOrderEntity(int tradeMode,
                            string strtegy,
                            int interval,
                            string password,
                            string symbol,
                            int exchange,
                            int tradeType,
                            int timeInForce,
                            string side,
                            double qty,
                            int frontOrderType,
                            double price,
                            int expireDay,
                            double triggerPrice,
                            int underOver,
                            int afterHitOrderType,
                            double afterHitPrice,
                            string executionID = "")
        {

            TradeMode = tradeMode;
            Strtegy = strtegy;
            Interval = interval;
            Pssword = password;
            Symbol = symbol;
            Exchange = exchange;
            TradeType = tradeType;
            TimeInForce = timeInForce;
            Side = side;
            Qty = qty;
            FrontOrderType = frontOrderType;
            Price = price;
            ExpireDay = expireDay;
            TriggerPrice = triggerPrice;
            UnderOver = underOver;
            AfterHitOrderType = afterHitOrderType;
            AfterHitPrice = afterHitPrice;
            ExecutionID = executionID;
        }
        public int TradeMode { get; set; }
        public string Strtegy { get; set; }
        public int Interval { get; set; }
        public string Pssword { get; set; }
        public string Symbol { get; set; }
        public int Exchange { get; set; }
        public int TradeType { get; set; }
        public int TimeInForce { get; set; }
        public string Side { get; set; }
        public double Qty { get; set; }
        public int FrontOrderType { get; set; }
        public double Price { get; set; }
        public int ExpireDay { get; set; }
        public double TriggerPrice { get; set; }
        public int UnderOver { get; set; }
        public int AfterHitOrderType { get; set; }
        public double AfterHitPrice { get; set; }
        public string ExecutionID { get; private set; }
    }
}