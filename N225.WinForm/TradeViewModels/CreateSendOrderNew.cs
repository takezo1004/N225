using N225.Domain;
using N225.Domain.Entities;
using N225.Domain.Modules.Utils;
using N225.Domain.Repositories;

namespace N225.WinForm.TradeViewModels
{
    public static class CreateSendOrderNew
    {
        public static SendOrderEntity SendOrderNewInputCheck(InputOrder ent)
        {
            Validate.ValidateSendOrder(ent.SelectedOrder, ent.TradeType, ent.Side,
                           ent.Symbol, ent.Qty, ent.Price, ent.StopPrice, ent.ExecutionId);

            return new SendOrderEntity(
                            ent.TradeMode, ent.Strategy, ent.Interval, Shared.Password,
                            ent.Symbol, exchange: 2, ent.TradeType, ent.TimeInForce, ent.Side, ent.Qty,
                            frontOrderType: 0, ent.Price, expireDay: 0, triggerPrice: ent.StopPrice,
                            underOver: 0, afterHitOrderType: 1, afterHitPrice: 1, ent.ExecutionId);
        }
    }
}
