using N225.Domain;
using N225.Domain.Exceptions;
using N225.Domain.StaticVlues;
using N225.Infrastrucure.KubuAPIs;

namespace N225.Infrastrucure.KabuSuit
{
    public class SendOrderCancel
    {
        /// <summary>
        /// 注文取消し処理
        /// </summary>
        /// <param name="orderID"></param>
        /// <exception cref="KabuException"></exception>
        public static void OrderCancel(string orderID)
        {
            //注文中かチェック
            int _recivetype = OrderInquiryList.GetState(orderID);
            if (_recivetype == 8)
            {
                throw new KabuException("キャンセルする注文はありません");
            }

            string _orderID;
            _orderID = CancelOrder_Future.CancelOrder(Shared.XAPIkey, orderID, Shared.Password);

            return;
        }
    }
}
