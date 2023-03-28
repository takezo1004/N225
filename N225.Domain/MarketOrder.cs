using N225.Domain.CommonConst;
using N225.Domain.Repositories;
using N225.Domain.StaticVlues;
using N225.Domain.ValueObject;

namespace N225.Domain
{
    public sealed class MarketOrder : IOrder
    {
        /// <summary>
        /// 成行注文データ作成
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public SendOrderEntity CreateOrderFiled(SendOrderEntity entity)
        {
            Exchange exchange = new Exchange();
            string _side;

            entity.Exchange = exchange.DerivExchange;
            entity.Price = 0;
            entity.AfterHitPrice = 0;       //成行
            entity.ExpireDay = 0;      //当日のみ有効
            entity.FrontOrderType = FrontOrderType.Market;
            entity.TriggerPrice = 0;

            if (entity.TradeType == TradeType.ExitOrder)
            {
                _side = PositionsCache.GetSide(entity.ExecutionID);
                if (_side == Side.Buy)
                {
                    entity.Side = Side.Sell;
                }
                else if (_side == Side.Sell)
                {
                    entity.Side = Side.Buy;
                }
            }

            //stop条件設定　marketOrder の設定はいらない
            if (entity.Side == Side.Buy)       //買い
            {
                entity.UnderOver = 2;
            }
            else if (entity.Side == Side.Sell)
            {
                entity.UnderOver = 1;  //売り
            }
            return entity;
        }
    }
}
