using N225.Domain.Entities;

namespace N225.WinForm.TradeViewModels
{
    public sealed class OrderListListView : ListViewBase
    {
        private string _state;
        private double _orderQty;
        private double _cumQty;
        private double _price;

        private OrderListEntity _entity;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="entity"></param>
        public OrderListListView(OrderListEntity entity)
        {
            _entity = entity;
            TradeMode = _entity.TradeMode.DisplayValue;
            RecvTime = _entity.RecvTime.DisplayValue;
            Strategy = _entity.Strategy;
            Iterval = _entity.Interval;
            CashMargin = _entity.CashMargin.DisplayValue;
            Side = _entity.Side.DisplayValue;
            State = _entity.ReciveType.DisplayValue;
            OrderQty = _entity.OrderQty;
            CumQty = _entity.CumQty;
            Price = _entity.Price;
            OrderID = _entity.OrderID;
            ExecutionID = _entity.ExecutionID;

        }
        public string TradeMode { get; set; }
        public string RecvTime { get; set; }
        public string Strategy { get; set; }
        public int Iterval { get; set; }
        public string CashMargin { get; set; }
        public string Side { get; set; }
        public string State
        {
            get { return _state; }
            set
            {
                SetPropety(ref _state, value);
            }
        }
        public double OrderQty
        {
            get { return _orderQty; }
            set
            {
                SetPropety(ref _orderQty, value);
            }
        }

        public double CumQty
        {
            get { return _cumQty; }
            set
            {
                SetPropety(ref _cumQty, value);
            }
        }
        public double Price
        {
            get { return _price; }
            set
            {
                SetPropety(ref _price, value);
            }
        }

        public string OrderID { get; set; }
        public string ExecutionID { get; set; }
    }
}


