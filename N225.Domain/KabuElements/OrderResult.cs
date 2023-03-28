using Codeplex.Data;
using N225.Domain.Entities;
using N225.Domain.Exceptions;
using N225.Domain.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace N225.Domain.Elements
{
    [JsonObject("OrdersResultModel")]
    public class OrdersResultModel
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("State")]
        public int State { get; set; }

        [JsonProperty("OrderState")]
        public int OrderState { get; set; }

        [JsonProperty("OrdType")]
        public int OrdType { get; set; }

        [JsonProperty("RecvTime")]
        public string RecvTime { get; set; }

        [JsonProperty("Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("SymbolName")]
        public string SymbolName { get; set; }

        [JsonProperty("Exchange")]
        public int Exchange { get; set; }

        [JsonProperty("ExchangeName")]
        public string ExchangeName { get; set; }

        [JsonProperty("TimeInForce")]
        public int TimeInForce { get; set; }

        [JsonProperty("Price")]
        public object Price { get; set; }

        [JsonProperty("OrderQty")]
        public object OrderQty { get; set; }

        [JsonProperty("CumQty")]
        public object CumQty { get; set; }

        [JsonProperty("Side")]
        public string Side { get; set; }

        [JsonProperty("CashMargin")]
        public int CashMargin { get; set; }

        [JsonProperty("AccountType")]
        public int AccountType { get; set; }

        [JsonProperty("DelivType")]
        public int DelivType { get; set; }

        [JsonProperty("ExpireDay")]
        public int ExpireDay { get; set; }

        [JsonProperty("MarginTradeType")]
        public int MarginTradeType { get; set; }


        [JsonProperty("Details")]
        public List<OrderDetails> Details { get; set; }
    }

    [JsonObject("OrderDetails")]
    public class OrderDetails
    {
        [JsonProperty("SeqNum")]
        public int SeqNum { get; set; }

        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("RecType")]
        public int RecType { get; set; }

        [JsonProperty("ExchangeID")]
        public string ExchangeID { get; set; }

        [JsonProperty("State")]
        public int State { get; set; }

        [JsonProperty("TransactTime")]
        public string TransactTime { get; set; }

        [JsonProperty("OrdType")]
        public object OrdType { get; set; }

        [JsonProperty("Price")]
        public object Price { get; set; }

        [JsonProperty("Qty")]
        public object Qty { get; set; }

        [JsonProperty("ExecutionID")]
        public string ExecutionID { get; set; }

        [JsonProperty("ExecutionDay")]
        public string ExecutionDay { get; set; }

        [JsonProperty("DelivDay")]
        public int DelivDay { get; set; }

        [JsonProperty("Commission")]
        public object Commission { get; set; }

        [JsonProperty("CommissionTax")]
        public object CommissionTax { get; set; }
    }

    public class OrderResult
    {
        private const int OrdersColDetails = 33;
        private const int OrdersColNoDetails = 19;

        public static List<OrderListEntity> OrdersDataToList(string stringJson)
        {

            List<OrdersResultModel> OrdersList;
            //受信データデシリアライズ
            OrdersList =
                    JsonConvert.DeserializeObject<List<OrdersResultModel>>(stringJson);

            int OrderRows = OrdersList.Count;
            if (OrderRows == 0)
                return null;

            List<OrderListEntity> OrderResultList = new List<OrderListEntity>();


            for (int i = 0; i < OrderRows; i++)
            {
                //count = OrdersList[i].Details.Count;
                OrdersResultModel _ordersList = OrdersList[i];
                OrderListEntity _entity = CreateEntity(_ordersList);

                OrderResultList.Add(_entity);
            }
            return OrderResultList;
        }
        /// <summary>
        /// OrdersResultModelからOrderResultListEntityを作成しOrdersCacheに追加する
        /// </summary>
        /// <param name="ordersList"></param>
        /// <returns></returns>
        public static OrderListEntity CreateEntity(OrdersResultModel ordersList)
        {
            int _count = ordersList.Details.Count;
            int _reciveType = ordersList.Details[_count - 1].RecType;
            OrderListEntity _entity = new OrderListEntity()
            {
                TradeMode = new TradeMode(0),
                RecvTime = new RecvTime(ordersList.RecvTime),
                Strategy = string.Empty,
                Interval = 0,
                CashMargin = new CashMargin(ordersList.CashMargin),
                Side = new Domain.ValueObjects.Side(ordersList.Side),
                ReciveType = new ReciveType(_reciveType),
                OrderQty = Convert.ToDouble(ordersList.OrderQty),
                CumQty = Convert.ToDouble(ordersList.CumQty),
                Price = Convert.ToDouble(ordersList.Details[_count - 1].Price),
                OrderID = ordersList.ID,
                ExecutionID = ordersList.Details[_count - 1].ExecutionID

            };

            return _entity;

        }

        public static List<OrderListEntity> GetOrderListEntity(string value)
        {
            CheckedData(value);

            return OrdersDataToList(value);

        }
        public static OrderListEntity GetOrderEntity(string value)
        {
            CheckedData(value);

            var _orderResult = JsonConvert.DeserializeObject<List<OrdersResultModel>>(value);

            return CreateEntity(_orderResult[0]);

        }
        private static void CheckedData(string value)
        {
            var objectJson = DynamicJson.Parse(value);

            if (objectJson.IsDefined("Code"))
            {
                string error = "Eoor Code:" + objectJson["Code"] + " " + objectJson["Message"];

                throw new APIResponsesException(error);
            }
        }
    }
}
