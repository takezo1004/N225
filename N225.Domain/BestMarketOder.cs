using N225.Domain.CommonConst;
using N225.Domain.Repositories;
using N225.Domain.StaticVlues;
using N225.Domain.ValueObject;

namespace N225.Domain
{
    internal class BestMarketOder : IOrder
    {
        /// <summary>
        /// 最適指値注文データ作成
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public SendOrderEntity CreateOrderFiled(SendOrderEntity entity)
        {
            string _side;
            Exchange exchange = new Exchange();

            entity.Exchange = exchange.DerivExchange;
            entity.AfterHitPrice = 0;       //成行
            entity.ExpireDay = 0;      //当日のみ有効
            entity.FrontOrderType = FrontOrderType.BestMarket;
            entity.TriggerPrice = 0;

            if (entity.TradeType == TradeType.ExitOrder)
            {
                _side = PositionsCache.GetSide(entity.ExecutionID);
                if (_side == Side.Buy)
                {
                    entity.Side = Side.Sell;
                    entity.Price = Shared.AskPrice;  //売り
                }
                else if (_side == Side.Sell)
                {
                    entity.Side = Side.Buy;
                    entity.Price = Shared.BidPrice;
                }
            }
            else
            {

                if (entity.Side == Side.Buy)       //買い
                {
                    entity.Price = Shared.BidPrice;
                }
                else if (entity.Side == Side.Sell)
                {
                    entity.Price = Shared.AskPrice;  //売り
                }

            }

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
