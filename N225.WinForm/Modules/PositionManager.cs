using N225.Domain.Elements;
using N225.Domain.Entities;
using N225.Domain.StaticVlues;
using N225.Domain.ValueObjects;
using N225.WinForm.TradeViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;

namespace N225.WinForm.Modules
{
    public class PositionManager
    {
        public static Dispatcher Dispatcher { get; set; } = null;
        public static BindingList<PositionListView> PositionList { get; set; }

        /// <summary>
        /// 約定データをPositionDataGridに表示
        /// </summary>
        /// <param name="_list"></param>
        public static void AddList(List<PositionListEntity> _list)
        {
            PositionListEntity _entity;

            if (_list != null)
            {
                int OrderRows = _list.Count;

                for (int i = 0; i < OrderRows; i++)
                {
                    _entity = _list[i];
                    if (_entity.LeaveQty > 0)
                    {

                        string key = _entity.ExecutionID.ToString();
                        PositionCsvItem item = PositionAuto.GetItem(key);
                        if (item != null)
                        {
                            _entity.TradeMode = new TradeMode(1);
                            _entity.Strategy = item.Strategy;
                            _entity.Iterval = item.Interval;
                        }

                        PositionsCache.Add(_entity);

                        Dispatcher.Invoke(delegate ()
                        {
                            PositionList.Add(new PositionListView(_entity));
                        });
                    }
                }
            }
        }

        /// <summary>
        /// PositionDataGrid、PositionsCache、PositionAutoに１行追加
        /// </summary>
        /// <param name="entity"></param>
        public static void Add(PositionListEntity entity)
        {
            //一行追加する

            if (entity.TradeMode.Value == 1)
            {
                //自動取引の場合positionAutoに登録
                PositionCsvItem auto = new PositionCsvItem()
                {
                    TradeMode = entity.TradeMode.DisplayValue,
                    ExecutionDay = entity.ExecutionDay.ToString(),
                    Strategy = entity.Strategy,
                    Interval = entity.Iterval,
                    Side = entity.Side.DisplayValue,
                    IeaveQty = entity.LeaveQty,
                    HoldQty = entity.HoldQty,
                    Price = entity.Price,
                    Profit = entity.Profit,
                    ExecutionID = entity.ExecutionID,
                    OrderID = entity.OrderID,
                };
                PositionAuto.Add(auto);
            }

            PositionsCache.Add(entity);

            Dispatcher.Invoke(delegate ()
            {
                PositionList.Add(new PositionListView(entity));
            });
        }

        /// <summary>
        /// PositionDataGrid、PositionsCache更新
        /// </summary>
        /// <param name="row"></param>
        /// <param name="positionList"></param>
        /// <param name="data"></param>
        public static void UpDate(int row, BindingList<PositionListView> positionList,
                                                          PositionManagerEntity data)
        {
            Dispatcher.Invoke(delegate ()
            {
                double _leaveQty = positionList.ElementAt(row).LeaveQty;
                double _holdQty = 0;
                double _cumQty = data.CumQty;
                double _ContractQty = _leaveQty - _cumQty;
                positionList.ElementAt(row).LeaveQty = _ContractQty;
                positionList.ElementAt(row).HoldQty = _holdQty;

                //cash更新 PositionManagerEntityを使う CumQty -> LeaveQty,OrderQty ->HoldQtyとする
                data.CumQty = _ContractQty;
                data.OrderQty = _holdQty;
                PositionsCache.UpDate(data);
            });

        }

        /// <summary>
        /// PositionDataGrid、PositionsCache、PositionAutoに１行削除
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tradeMode"></param>
        public static void Deleted(PositionManagerEntity entity, int tradeMode)
        {
            int row = GetPositionListIndex(entity.ExecutionID);

            string key = entity.ExecutionID;

            if (tradeMode == 1)
            {
                PositionAuto.Remove(key);
            }

            PositionsCache.Remove(key);

            Dispatcher.Invoke(delegate ()
            {
                PositionList.RemoveAt(row);
            });
        }

        
        /// <summary>
        /// 自動取引約定番号のリスト取得
        /// </summary>
        /// <param name="strtegy"></param>
        /// <param name="interval"></param>
        /// <param name="side">反対のsideをチェックする</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal static List<string> GetExecutionId(string strtegy, int interval, string side)
        {
            List<string> listid = PositionsCache.GetExecutionId(strtegy, interval, side);
            List<string> listExecution = new List<string>();

            if (listid.Count > 0)
            {
                foreach (var id in listid)
                {
                   
                    if (PositionsCache.IsExecutionId(id))
                    {
                        listExecution.Add(id);
                        
                    }
                   /*
                    else
                    {
                        //PositionAutoにIdがありPositionsCacheにない場合はPositionAuto Id　削除
                        //PositionAuto.Remove(id); 
                    }
                    */
                }
            }
            return listExecution;
        }

        /// <summary>
        /// PositionDataGrid、PositionsCache　約定枚数更新
        /// </summary>
        /// <param name="positionList"></param>
        /// <param name="executionID"></param>
        /// <param name="qty"></param>
        public static void AddHoldQty(BindingList<PositionListView> positionList, string executionID, double qty)
        {
            Dispatcher.Invoke(delegate ()
            {
                for (int i = 0; i < positionList.Count; i++)
                {
                    if (positionList.ElementAt(i).ExecutionID == executionID)
                    {
                        positionList[i].HoldQty = qty;
                        break;
                    }
                }
            });
            PositionsCache.AddHoldQty(executionID, qty);
        }

        /// <summary>
        /// 照会処理スタート時の初期化
        /// </summary>
        /// <param name="entity"></param>
        public static void Initial(OrderListEntity entity)
        {
            if (entity.CashMargin == CashMargin.Exit)
            {
                string executionID = entity.ExecutionID;
                Dispatcher.Invoke(delegate ()
                {
                    for (int i = 0; i < PositionList.Count; i++)
                    {
                        if (PositionList.ElementAt(i).ExecutionID == executionID)
                        {
                            PositionList[i].HoldQty = entity.OrderQty;
                            break;
                        }
                    }
                });
                PositionsCache.AddHoldQty(executionID, entity.OrderQty);
            }
        }

        /// <summary>
        /// 期限切れ
        /// </summary>
        /// <param name="entity"></param>
        public static void Xepired(PositionManagerEntity entity)
        {
            var _entity = entity;
            if (entity.CashMargin == 2)
            {
                return;
            }

            string executionID = entity.ExecutionID;

            int row = GetPositionListIndex(executionID);

            Dispatcher.Invoke(delegate ()
            {
                double _leaveQty = PositionList.ElementAt(row).LeaveQty;
                double _holdQty = 0;
                double _cumQty = _entity.CumQty;
                double _ContractQty = _leaveQty - _cumQty;
                PositionList.ElementAt(row).LeaveQty = _ContractQty;
                PositionList.ElementAt(row).HoldQty = _holdQty;

                //cash更新 PositionManagerEntityを使う CumQty -> LeaveQty,OrderQty ->HoldQtyとする
                _entity.CumQty = _ContractQty;
                _entity.OrderQty = _holdQty;
                PositionsCache.UpDate(_entity);
            });

        }

        /// <summary>
        /// 発注
        /// </summary>
        /// <param name="entity"></param>
        public static void Order(PositionManagerEntity entity)
        {
            return;
        }

        /// <summary>
        /// 訂正
        /// </summary>
        /// <param name="orderId"></param>
        public static void Correction(string orderId)
        {
            return;
        }

        /// <summary>
        /// 注文キャンセル
        /// </summary>
        /// <param name="entity"></param>
        public static void Cancel(PositionManagerEntity entity)
        {
            Xepired(entity);
        }

        /// <summary>
        /// 注文失効
        /// </summary>
        /// <param name="entity"></param>
        public static void Revocation(PositionManagerEntity entity)
        {
            Xepired(entity);
        }

        /// <summary>
        /// 注文分割約定
        /// </summary>
        /// <param name="resultModel"></param>
        /// <param name="data"></param>
        public static void Contrct(OrdersResultModel resultModel, PositionManagerEntity data)
        {
            string _orderID = resultModel.ID;
            OrderListEntity _entity = OrdersCache.GetsEntity(_orderID);
            int tradeMode = _entity.TradeMode.Value;

            if (resultModel.CashMargin == 2)
            {
                foreach (OrderDetails detail in resultModel.Details)
                {
                    if (detail.RecType == 8)
                    {
                        PositionListEntity viewEntity = new PositionListEntity(
                            tradeMode,
                            Convert.ToInt32(_entity.RecvTime.yyyyMMdd),                       //Convert.ToInt32(_entity.RecvTime.Value),
                            _entity.Strategy,
                            _entity.Interval,
                            _entity.Side.Value,
                            Convert.ToDouble(detail.Qty),
                            Convert.ToDouble(0),
                            Convert.ToDouble(detail.Price),
                            0,
                            detail.ExchangeID,
                            _orderID);

                        Add(viewEntity);
                    }

                }
            }
            else
            {
                int count = resultModel.Details.Count;
                string _executionID = resultModel.Details[count - 1].ExecutionID;
                double qty = Convert.ToDouble(resultModel.Details[count - 1].Qty);
                int row = GetPositionListIndex(_executionID);

                double leaveQty = PositionList.ElementAt(row).LeaveQty;
                double holdQty = PositionList.ElementAt(row).HoldQty;


                if (leaveQty > holdQty)
                {
                    Dispatcher.Invoke(delegate ()
                    {
                        double _leaveQty = PositionList.ElementAt(row).LeaveQty;
                        PositionList.ElementAt(row).LeaveQty -= qty;
                        PositionList.ElementAt(row).HoldQty -= qty;

                        //cash更新 PositionManagerEntityを使う CumQty -> LeaveQty,OrderQty ->HoldQtyとする
                        data.CumQty -= qty;
                        data.OrderQty -= qty;
                        PositionsCache.UpDate(data);
                    });
                }
                if (PositionList.ElementAt(row).LeaveQty <= 0)
                {
                    Deleted(data, tradeMode);
                }
            }
        }

        /// <summary>
        /// 注文全約定
        /// </summary>
        /// <param name="entity"></param>
        public static void ContrctAll(PositionManagerEntity entity)
        {
            string _orderID = entity.OrderID;
            string _executionID = entity.ExecutionID;
            int tradeMode = 0;

            if (entity.CashMargin == 2)
            {
                //OrdersCacheからorderデータを取得しpositionデータを作成１行追加する
                OrderListEntity _entity = OrdersCache.GetsEntity(_orderID);
                tradeMode = _entity.TradeMode.Value;

                PositionListEntity viewEntity = new PositionListEntity(
                            tradeMode,
                            Convert.ToInt32(_entity.RecvTime.yyyyMMdd),                       //Convert.ToInt32(_entity.RecvTime.Value),
                            _entity.Strategy,
                            _entity.Interval,
                            _entity.Side.Value,
                            Convert.ToDouble(entity.CumQty),
                            Convert.ToDouble(0),
                            Convert.ToDouble(entity.Price),
                            0,
                            _executionID,
                            _orderID);

                Add(viewEntity);

            }
            else if (entity.CashMargin == 3)
            {
                //返済：position　削除、orderはqty,cumqty　メッセージ
                Deleted(entity, tradeMode);
            }
        }

        /// <summary>
        /// executionIDに該当するPositionDataGridインデックス番号
        /// </summary>
        /// <param name="executionID"></param>
        /// <returns></returns>
        private static int GetPositionListIndex(string executionID)
        {
            int _row = -1;
            for (int i = 0; i < PositionList.Count; i++)
            {
                if (PositionList.ElementAt(i).ExecutionID == executionID)
                {
                    _row = i;
                    break;
                }
            }
            return _row;
        }
    }
}
