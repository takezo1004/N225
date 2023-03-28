using N225.Domain.ValueObjects;

namespace N225.Domain.Entities
{
    /// <summary>
    /// 新規・返済注文データフィールド
    /// </summary>
    public class OrderListEntity
    {
        public OrderListEntity()
        {
        }
        public OrderListEntity(int tradeMode, string recvTime, string strategy,
                            int interval, int cashMargin, string side, int reciveType,
                            double orderQty, double cumQty, double price,
                            string orderID, string executionID)
        {
            TradeMode = new TradeMode(tradeMode);
            RecvTime = new RecvTime(recvTime);
            Strategy = strategy;
            Interval = interval;
            CashMargin = new CashMargin(cashMargin);
            Side = new ValueObjects.Side(side);
            ReciveType = new ReciveType(reciveType);
            OrderQty = orderQty;
            CumQty = cumQty;
            Price = price;
            OrderID = orderID;
            ExecutionID = executionID;
        }
        public TradeMode TradeMode { get; set; }
        public RecvTime RecvTime { get; set; }
        public string Strategy { get; set; }
        public int Interval { get; set; }
        public CashMargin CashMargin { get; set; }
        public ValueObjects.Side Side { get; set; }
        public ReciveType ReciveType { get; set; }
        public double OrderQty { get; set; }
        public double CumQty { get; set; }
        public double Price { get; set; }
        public string OrderID { get; set; }
        public string ExecutionID { get; set; }
    }
}
