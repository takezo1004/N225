using Codeplex.Data;
using N225.Domain;
using N225.Domain.Elements;
using N225.Domain.Entities;
using N225.Domain.Exceptions;
using N225.Domain.StaticVlues;
using N225.Infrastrucure.KubuAPIs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;

namespace N225.WinForm.BackgrondWorkers
{
    public class InquiryTimer
    {
        public event EventHandler<OrdersResultModel> InquiryOrderEventHandler;
        public event EventHandler<bool> OutquiryStatEventHandler;
        private Timer _timer;
        private bool _isWork = false;

        /// <summary>
        /// BackGrand Timer設定
        /// </summary>
        public InquiryTimer()
        {
            _timer = new Timer(Callback);
        }
        public void Start()
        {
            _timer.Change(1000, 1000);
        }
        public void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// BackGrand　照会処理
        /// </summary>
        /// <param name="o"></param>
        private void Callback(object o)
        {
            if (_isWork)
            {
                return;
            }
            try
            {
                _isWork = true;
                //注文中のデータが有るかチェック OrderInquiryList
                Dictionary<string, OrderListEntity> list =
                    OrderInquiryList.IsList();
                if (list == null)
                {
                    OutquiryStatEventHandler(this, false);
                    return;
                }

                OrderEntity entity;

                int count = list.Count;

                foreach (KeyValuePair<string, OrderListEntity> kvp in list)
                {
                    entity = new OrderEntity()
                    {
                        Product = "3",
                        OrderID = kvp.Key
                    };
                    List<OrdersResultModel> _ordersList = Oorder(entity);

                    if (_ordersList != null)
                    {
                        OrdersResultModel _order = _ordersList[0];
                        //int _timeForce = _order.TimeInForce;        //1:fas 2:fak 3:fok
                        //int _orderState = _order.OrderState;
                        ////ラストのDetailをチェックする
                        int _count = _order.Details.Count;
                        int _state = _order.Details[_count - 1].State;
                        int _reciveType = _order.Details[_count - 1].RecType;
                        if (_reciveType != kvp.Value.ReciveType.Value)
                        {
                            InquiryOrderEventHandler(this, _order);
                        }
                        OutquiryStatEventHandler(this, true);
                    }
                    else
                    {
                        OutquiryStatEventHandler(this, false);
                    }
                }
            }
            finally
            {
                _isWork = false;
            }
        }
        /// <summary>
        /// オーダー照会
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="APIResponsesException"></exception>
        private List<OrdersResultModel> Oorder(OrderEntity entity)
        {
            string retVlue = Orders_Future.Orders(Shared.XAPIkey, entity);

            var objectJson = DynamicJson.Parse(retVlue);

            if (objectJson.IsDefined("Code"))
            {
                string error = "Eoor Code:" + objectJson["Code"] + " " + objectJson["Message"];

                throw new APIResponsesException(error);
            }

            //デシリアライズ
            return JsonConvert.DeserializeObject<List<OrdersResultModel>>(retVlue);
        }
    }
}
