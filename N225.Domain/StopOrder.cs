using N225.Domain.CommonConst;
using N225.Domain.Repositories;
using N225.Domain.StaticVlues;
using N225.Domain.ValueObject;

namespace N225.Domain
{
    /// <summary>
    /// 逆指値注文データ作成
    /// </summary>
    public sealed class StopOrder : IOrder
    {
        public SendOrderEntity CreateOrderFiled(SendOrderEntity entity)
        {
            string _side;

            Exchange exchange = new Exchange();

            entity.Exchange = exchange.DerivExchange;
            entity.AfterHitOrderType = 1;
            entity.AfterHitPrice = 0;       //成行
            entity.ExpireDay = 0;      //当日のみ有効
            entity.FrontOrderType = FrontOrderType.Stop;
            entity.Price = 0;

            if (entity.TradeType == TradeType.ExitOrder)
            {
                _side = PositionsCache.GetSide(entity.ExecutionID);
                if (_side == Side.Buy)
                {
                    entity.Side = Side.Sell;
                    entity.UnderOver = 1;
                }
                else if (_side == Side.Sell)
                {
                    entity.Side = Side.Buy;
                    entity.UnderOver = 2;
                }
            }
            if (entity.TradeType == TradeType.NewOrder)
            {
                if (entity.Side == Side.Buy)       //買い
                {
                    entity.UnderOver = 2;
                }
                else if (entity.Side == Side.Sell)
                {
                    entity.UnderOver = 1;  //売り
                }
            }

            return entity;
        }
    }
}
