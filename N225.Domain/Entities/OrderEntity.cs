namespace N225.Domain.Entities
{
    /// <summary>
    /// 商品照会データフィールド
    /// </summary>
    public sealed class OrderEntity
    {
        public OrderEntity(
                    string product = "",
                    string orderID = "",
                    string updtime = "",
                    string details = "",
                    string symbol = "",
                    string state = "",
                    string side = "",
                    string cashmargin = "")
        {
            Product = product;
            OrderID = orderID;
            Updtime = updtime;
            Details = details;
            Symbol = symbol;
            State = state;
            Side = side;
            Cashmargin = cashmargin;
        }
        public string Product { get; set; }
        public string OrderID { get; set; }
        public string Updtime { get; set; }
        public string Details { get; set; }
        public string Symbol { get; set; }
        public string State { get; set; }
        public string Side { get; set; }
        public string Cashmargin { get; set; }

    }
}
