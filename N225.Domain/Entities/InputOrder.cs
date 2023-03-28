namespace N225.Domain.Entities
{
    /// <summary>
    /// 注文データ入力
    /// </summary>
    public class InputOrder
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InputOrder()
        {
        }

        /// <summary>
        /// 注文データ入力
        /// </summary>
        /// <param name="tradeMode"></param>
        /// <param name="strategy"></param>
        /// <param name="interval"></param>
        /// <param name="selectedOrder"></param>
        /// <param name="tradeType"></param>
        /// <param name="side"></param>
        /// <param name="symbol"></param>
        /// <param name="timeInForce"></param>
        /// <param name="qty"></param>
        /// <param name="price"></param>
        /// <param name="stopPrice"></param>
        /// <param name="excutionId"></param>
        public InputOrder(
                    int tradeMode,
                    string strategy,
                    int interval,
                    int selectedOrder,
                    int tradeType,
                    string side,
                    string symbol,
                    int timeInForce,
                    double qty,
                    double price,
                    double stopPrice,
                    string excutionId)
        {
            TradeMode = tradeMode;
            Strategy = strategy;
            Interval = interval;
            SelectedOrder = selectedOrder;
            TradeType = tradeType;
            Side = side;
            Symbol = symbol;
            TimeInForce = timeInForce;
            Qty = qty;
            Price = price;
            StopPrice = stopPrice;
            ExecutionId = excutionId;
        }
        public int TradeMode { get; set; }
        public string Strategy { get; set; }
        public int Interval { get; set; }
        public int SelectedOrder { get; set; }
        public int TradeType { get; set; }
        public string Side { get; set; }
        public string Symbol { get; set; }
        public int TimeInForce { get; set; }
        public double Qty { get; set; }
        public double Price { get; set; }
        public double StopPrice { get; set; }
        public string ExecutionId { get; set; }
    }
}
