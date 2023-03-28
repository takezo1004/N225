using N225.Domain.ValueObjects;

namespace N225.Domain.Entities
{
    /// <summary>
    /// 約定データ用フィールド
    /// </summary>
    public class PositionListEntity
    {
        public PositionListEntity()
        {
        }

        public PositionListEntity(
                            int tradeMode,
                            int executionDay,
                            string strategy,
                            int interval,
                            string side,
                            double leaveQty,
                            double holdQty,
                            double price,
                            double profit,
                            string executionID,
                            string orderID
                            )
        {
            TradeMode = new TradeMode(tradeMode);
            ExecutionDay = executionDay;
            Strategy = strategy;
            Iterval = interval;
            Side = new ValueObjects.Side(side);
            LeaveQty = leaveQty;
            HoldQty = holdQty;
            Price = price;
            Profit = profit;
            ExecutionID = executionID;
            OrderID = orderID;
        }
        public TradeMode TradeMode { get; set; }
        public int ExecutionDay { get; set; }
        public string Strategy { get; set; }
        public int Iterval { get; set; }
        public ValueObjects.Side Side { get; set; }
        public double LeaveQty { get; set; }
        public double HoldQty { get; set; }
        public double Price { get; set; }
        public double Profit { get; set; }
        public string ExecutionID { get; set; }
        public string OrderID { get; set; }

    }
}
