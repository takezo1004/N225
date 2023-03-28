namespace N225.Domain.Entities
{
    public class PositionManagerEntity
    {
        public string OrderID { get; set; }
        public int ReciveType { get; set; }
        public string ExecutionID { get; set; }
        public int CashMargin { get; set; }
        public double OrderQty { get; set; }
        public double CumQty { get; set; }
        public double Price { get; set; }
        public int OrderState { get; set; }
    }
}
