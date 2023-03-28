using N225.Domain.Entities;

namespace N225.WinForm.TradeViewModels
{
    /// <summary>
    /// 約定表示用データフィールド
    /// </summary>
    public sealed class PositionListView : ListViewBase
    {
        private PositionListEntity _entity;
        private double _leaveQty;
        private double _holdQty;
        private double _profit;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="entity"></param>
        public PositionListView(PositionListEntity entity)
        {
            _entity = entity;
            TradeMode = entity.TradeMode.DisplayValue;
            DateTime = _entity.ExecutionDay.ToString();
            Strategy = _entity.Strategy.ToString();
            Iterval = _entity.Iterval;
            Side = _entity.Side.DisplayValue;
            LeaveQty = _entity.LeaveQty;
            HoldQty = _entity.HoldQty;
            Price = _entity.Price;
            Profit = _entity.Profit;
            ExecutionID = _entity.ExecutionID.ToString();
        }

        public PositionListView()
        {
        }
        public string TradeMode { get; set; }
        public string DateTime { get; set; }
        public string Strategy { get; set; }
        public int Iterval { get; set; }
        public string Side { get; set; }
        public double LeaveQty
        {
            get { return _leaveQty; }
            set
            {
                SetPropety(ref _leaveQty, value);
            }
        }

        public double HoldQty
        {
            get { return _holdQty; }
            set
            {
                SetPropety(ref _holdQty, value);
            }
        }
        public double Price { get; set; }
        public double Profit
        {
            get { return _profit; }
            set
            {
                SetPropety(ref _profit, value);
            }
        }

        public string ExecutionID { get; set; }



        //private void NotifyPropertyChanged(string p)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(p));
        //}
    }
}
