namespace N225.Domain.Entities
{
    /// <summary>
    /// Position用csvファイル読み・書き　ITM
    /// </summary>
    public class PositionCsvItem
    {
        public string TradeMode { get; set; }
        public string ExecutionDay { get; set; }
        public string Strategy { get; set; }
        public int Interval { get; set; }
        public string Side { get; set; }
        public double IeaveQty { get; set; }
        public double HoldQty { get; set; }
        public double Price { get; set; }
        public double Profit { get; set; }
        public string ExecutionID { get; set; }
        public string OrderID { get; set; }
    }
}

