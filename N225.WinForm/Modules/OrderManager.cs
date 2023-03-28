using N225.Domain.Elements;
using N225.Domain.Entities;
using N225.Domain.Modules.Utils;
using N225.Domain.StaticVlues;
using N225.Domain.ValueObjects;
using N225.Infrastrucure;
using N225.WinForm.TradeViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;

namespace N225.WinForm.Modules
{
    public class OrderManager
    {

        public static Dispatcher Dispatcher { get; set; } = null;

        public static BindingList<OrderListListView> OrderList { get; set; }

        //注文中のOder List key= orderID
        public static Dictionary<string, OnoderEntity> OnoderList =
                                    new Dictionary<string, OnoderEntity>();
        /// <summary>
        /// オーダーリストをOrderDataGrid,OrdersCache、OrderInquiryListに追加表示する
        /// </summary>
        /// <param name="orderList"></param>
        /// <param name="_list"></param>
        public static void AddList( List<OrderListEntity> _list)
        {
            OrderListEntity _entity;
            List<OnoderEntity> newOnorder = new List<OnoderEntity>();

            if (_list != null)
            {
                int OrderRows = _list.Count;

                for (int i = 0; i < OrderRows; i++)
                {
                    _entity = _list[i];

                    if( ! string.IsNullOrEmpty(_entity.ExecutionID))
                    {
                        string key = _entity.ExecutionID.ToString();
                        PositionCsvItem item = PositionAuto.GetItem(key);
                        if (item != null)
                        {
                            _entity.TradeMode = new TradeMode(1);
                            _entity.Strategy = item.Strategy;
                            _entity.Interval = item.Interval;
                        }
  
                    }
                    //注文中データがある場合はExecutionIDを追加する
                    if (OnoderList.ContainsKey(_entity.OrderID))
                    {
                        _entity.ExecutionID = OnoderList[_entity.OrderID].ExecutionID;
                        newOnorder.Add(OnoderList[_entity.OrderID]);
                    }
                    //一行追加する
                    Dispatcher.Invoke(delegate ()
                    {
                        OrderList.Add(new OrderListListView(_entity));
                    });

                    //注文中データがあるときはOrderInquiryListに追加する
                    if (_entity.ReciveType.Value == 4)
                    {
                        OrderInquiryList.Add(_entity);
                    }
                    
                    OrdersCache.Add(_entity);
                }
                //OnoderListの更新
                OnoderList.Clear();
                if (newOnorder.Count > 0)
                {
                    for(int i = 0; i < newOnorder.Count; i++)
                    {
                        OnoderList.Add(newOnorder[i].OrderID, newOnorder[i]);
                    }
                }
                ToCsv();
            }
        }

        /// <summary>
        /// OrderDataGrid,OrdersCache、OrderInquiryListに一行追加する
        /// </summary>
        /// <param name="orderList"></param>
        /// <param name="_entity"></param>
        public static void Add( OrderListEntity _entity)
        {

            //一行追加する
            Dispatcher.Invoke(delegate ()
            {
                OrderList.Add(new OrderListListView(_entity));
            });

            OrdersCache.Add(_entity);
            OrderInquiryList.Add(_entity);
        }

        /// <summary>
        /// OrderDataGrid,OrdersCache、OrderInquiryList更新
        /// </summary>
        /// <param name="row"></param>
        /// <param name="orderList"></param>
        /// <param name="data"></param>
        public static void UpDate(int row, BindingList<OrderListListView> orderList,
                                                          OrederManagerEntity data)
        {
            Dispatcher.Invoke(delegate ()
            {
                orderList.ElementAt(row).State = new ReciveType(data.ReciveType).DisplayValue;
                orderList.ElementAt(row).OrderQty = data.OrderQty;
                orderList.ElementAt(row).CumQty = data.CumQty;
                orderList.ElementAt(row).Price = data.Price;
            });

            //cash更新
            OrdersCache.UpDate(data);
            OrderInquiryList.UpDate(data);
        }

        /// <summary>
        /// OrderDataGrid 書き換え,OrderInquiryList削除
        /// </summary>
        /// <param name="row"></param>
        /// <param name="orderList"></param>
        /// <param name="data"></param>
        public static void Remove(int row, BindingList<OrderListListView> orderList, OrederManagerEntity data)
        {
            Dispatcher.Invoke(delegate ()
            {
                orderList.ElementAt(row).State = new ReciveType(data.ReciveType).DisplayValue;
                orderList.ElementAt(row).OrderQty = data.OrderQty;
                orderList.ElementAt(row).CumQty = data.CumQty;
                orderList.ElementAt(row).Price = data.Price;
            });
            OrderInquiryList.Remove(data);
        }

        /// <summary>
        /// ExecutionIdを取得
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static string GetExecutionId(string orderId)
        {
            return OrdersCache.GetExecutionID(orderId);
        }

        /// <summary>
        /// OrderDataGrid,OrdersCache、OrderInquiryListの初期化
        /// </summary>
        /// <param name="entity"></param>
        public static void Initial(OrderListEntity entity)
        {

            var _entity = entity;
            //一行追加する
            var viewList = new OrderListListView(_entity);
            Dispatcher.Invoke(delegate ()
            {
                OrderList.Add(viewList);
            });

            OrdersCache.Add(_entity);
            OrderInquiryList.Add(_entity);
            OnoderEntity onorder = new OnoderEntity()
            {
                TradeMode = viewList.TradeMode,
                RecvTime = viewList.RecvTime,
                Strategy = viewList.Strategy,
                Iterval = viewList.Iterval,
                CashMargin = viewList.CashMargin,
                Side = viewList.Side,
                State = viewList.State,
                OrderQty = viewList.OrderQty,
                CumQty = viewList.CumQty,
                Price = viewList.Price,
                OrderID = viewList.OrderID,
                ExecutionID = viewList.ExecutionID
            };

            OnoderList.Add(_entity.OrderID, onorder);
            ToCsv();
        }

        /// <summary>
        /// 注文期限切れ
        /// </summary>
        /// <param name="entity"></param>
        public static void Xepired(OrederManagerEntity entity)
        {
            var _entity = entity;
            string orderID = entity.Id;
            int row = GetOrderListIndex(orderID);

            Dispatcher.Invoke(delegate ()
            {
                OrderList.ElementAt(row).State = new ReciveType(_entity.ReciveType).DisplayValue;
                OrderList.ElementAt(row).OrderQty = _entity.OrderQty;
                OrderList.ElementAt(row).CumQty = _entity.CumQty;
                OrderList.ElementAt(row).Price = _entity.Price;
            });
            OrderInquiryList.Remove(_entity);
        }

        /// <summary>
        /// 注文発注
        /// </summary>
        /// <param name="entity"></param>
        public static void Order(OrederManagerEntity entity)
        {
            var _entity = entity;
            string orderID = entity.Id;
            int row = GetOrderListIndex(orderID);

            Dispatcher.Invoke(delegate ()
            {
                OrderList.ElementAt(row).State = new ReciveType(_entity.ReciveType).DisplayValue;
                OrderList.ElementAt(row).OrderQty = _entity.OrderQty;
                OrderList.ElementAt(row).CumQty = _entity.CumQty;
                OrderList.ElementAt(row).Price = _entity.Price;
            });

            //cash更新
            OrdersCache.UpDate(_entity);
            OrderInquiryList.UpDate(_entity);
        }

        /// <summary>
        /// 注文訂正
        /// </summary>
        /// <param name="resultModel"></param>
        public static void Correction(OrdersResultModel resultModel)
        {
            return;
        }

        /// <summary>
        /// 注文取消
        /// </summary>
        /// <param name="entity"></param>
        public static void Cancel(OrederManagerEntity entity)
        {
            var _entity = entity;
            string orderID = entity.Id;
            int row = GetOrderListIndex(orderID);

            Dispatcher.Invoke(delegate ()
            {
                OrderList.ElementAt(row).State = new ReciveType(_entity.ReciveType).DisplayValue;
                //OrderList.ElementAt(row).OrderQty = _entity.OrderQty;
                //OrderList.ElementAt(row).CumQty = _entity.CumQty;
                OrderList.ElementAt(row).Price = _entity.Price;
            });
            OrderInquiryList.Remove(_entity);

        }

        /// <summary>
        /// 注文失効
        /// </summary>
        /// <param name="entity"></param>
        public static void Revocation(OrederManagerEntity entity)
        {
            Xepired(entity);
        }

        /// <summary>
        /// 注文分割約定
        /// </summary>
        /// <param name="entity"></param>
        public static void Contrct(OrederManagerEntity entity)
        {
            string orderID = entity.Id;
            int row = GetOrderListIndex(orderID);
            Dispatcher.Invoke(delegate ()
            {
                OrderList.ElementAt(row).State = new ReciveType(8).DisplayValue;
                //OrderList.ElementAt(row).OrderQty = _entity.OrderQty;
                OrderList.ElementAt(row).CumQty = Convert.ToDouble(entity.CumQty);
            });

            OrdersCache.UpDate(entity);
            if (entity.OrderQty == entity.CumQty)
            {
                OrderInquiryList.Remove(entity);
            }
        }

        /// <summary>
        /// 注文全約定
        /// </summary>
        /// <param name="entity"></param>
        public static void ContrctAll(OrederManagerEntity entity)
        {
            Order(entity);
            //OrderInquiryListを削除する
            OrderInquiryList.Remove(entity);

        }

        /// <summary>
        /// OrderDataGrid選択行番号の取得
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        private static int GetOrderListIndex(string orderID)
        {
            int _row = -1;
            for (int i = 0; i < OrderList.Count; i++)
            {
                if (OrderList.ElementAt(i).OrderID == orderID)
                {
                    _row = i;
                    break;
                }
            }
            return _row;
        }
        /// <summary>
        /// Csvファイルからデータ読み込みディクショナリーに保存
        /// </summary>
        /// <param name="path"></param>
        public static void CsvRead(string path = "N225/Csvfile/order.csv")
        {
            string dir = DirectoryUtils.LocalApplicationDataPath();
            path = dir + "/" + path;

            List<OnoderEntity> list = new List<OnoderEntity>();
            List<OnoderEntity> rcordes = Csv.Reader(ref list, path);
            for (int i = 0; i < rcordes.Count; i++)
            {
                OnoderEntity item = rcordes[i];
                string key = item.OrderID.ToString();
                OnoderList.Add(key, item);
            }
        }
        /// <summary>
        /// Csvファイルに保存
        /// </summary>
        /// <param name="path"></param>
        private static void ToCsv(string path = "N225/Csvfile/order.csv")
        {
            string dir = DirectoryUtils.LocalApplicationDataPath();
            path = dir + "/" + path;

            List<OnoderEntity> Items = new List<OnoderEntity>();
            foreach (KeyValuePair<string, OnoderEntity> kvp in OnoderList)
            {
                Items.Add(kvp.Value);
            }
            Csv.Writer(Items, path);
        }

    }
}
