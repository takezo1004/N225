using N225.Domain;
using N225.Domain.CommonConst;
using N225.Domain.Elements;
using N225.Domain.Entities;
using N225.Domain.Repositories;
using N225.Domain.ValueObjects;
using N225.Infrastrucure.KubuAPIs;

namespace N225.Infrastrucure.KabuSuit
{
    public class SendOrderFuture
    {

        /// <summary>
        /// 注文発注と戻り値(entity)作成
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static OrderListEntity SendOrder(SendOrderEntity entity)
        {
            string _symbol = entity.Symbol;
            int _tradeMode = entity.TradeMode;
            string _strategy = entity.Strtegy;
            int _interval = entity.Interval;
            string _orderID = string.Empty;        // = "20220908A02N52347765"; //testコード
            string _executionID = entity.ExecutionID;   //返済


            if (entity.TradeType == TradeType.NewOrder)
            {
                _orderID = Sendorder_Future_new.SendOrder(Shared.XAPIkey, entity);
            }
            else if (entity.TradeType == TradeType.ExitOrder)
            {
                _orderID = Sendorder_Future_Exit.SendOrderExit(Shared.XAPIkey, entity);
            }

            OrderEntity _entity = new OrderEntity()
            {
                Product = "3",
                OrderID = _orderID,
                Symbol = _symbol,
            };

            //view用とCache用照会検索用OrderInquiryListを作成する
            string ret = Orders_Future.Orders(Shared.XAPIkey, _entity);

            OrderListEntity _orderEntiry = OrderResult.GetOrderEntity(ret);

            _orderEntiry.TradeMode = new TradeMode(_tradeMode);
            _orderEntiry.Strategy = _strategy;
            _orderEntiry.Interval = _interval;
            _orderEntiry.OrderID = _orderID;

            //返済の場合約定番号を保存する
            if (_orderEntiry.CashMargin == CashMargin.Exit)
            {
                _orderEntiry.ExecutionID = _executionID;
            }

            _orderEntiry.ReciveType = new ReciveType(1);   //受付に設定する

            return _orderEntiry;

        }
    }
}
