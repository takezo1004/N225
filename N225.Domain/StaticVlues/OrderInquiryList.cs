using N225.Domain.Entities;
using N225.Domain.Exceptions;
using N225.Domain.ValueObjects;
using System.Collections.Generic;

namespace N225.Domain.StaticVlues
{
    public static class OrderInquiryList
    {
        /// <summary>
        /// 照会検索用ディクショナリー
        /// </summary>
        private static Dictionary<string, OrderListEntity> _orderInquiryList =
                                    new Dictionary<string, OrderListEntity>();
        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="DataNotExistsException"></exception>
        public static void Add(OrderListEntity entity)
        {
            if (entity != null && string.IsNullOrEmpty(entity.OrderID) == false)
            {
                _orderInquiryList.Add(entity.OrderID, entity);
            }
            else
            {
                throw new DataNotExistsException("OrderInquiryList:Add Entity追加出来ません");
            }

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="DataNotExistsException"></exception>
        public static void UpDate(OrederManagerEntity entity)
        {
            OrderListEntity tpl;
            string _orderId = entity.Id;
            //_reciveType, 0, Convert.ToInt32(e.CumQty)
            if (_orderInquiryList.TryGetValue(_orderId, out tpl))
            {
                tpl.ReciveType = new ReciveType(entity.ReciveType);
                tpl.OrderQty = entity.OrderQty;
                tpl.CumQty = entity.CumQty;
                tpl.Price = entity.Price;
            }
            else
            {
                throw new DataNotExistsException("OrderInquiryList:UpDate OrderIDはありません");
            }

        }

        /// <summary>
        /// 要素削除
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="DataNotExistsException"></exception>
        public static void Remove(OrederManagerEntity entity)
        {
            OrderListEntity tpl;
            string _orderId = entity.Id;
            if (_orderInquiryList.TryGetValue(_orderId, out tpl))
            {
                _orderInquiryList.Remove(_orderId);
            }
            else
            {
                throw new DataNotExistsException("OrderInquiryList:Remove OrderIDはありません");
            }

        }

        /// <summary>
        /// ディクショナリー取得
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, OrderListEntity> IsList()
        {
            if (_orderInquiryList.Count > 0)
            {
                //_orderInquiryListをコピーして渡す
                Dictionary<string, OrderListEntity> copyList =
                            new Dictionary<string, OrderListEntity>(_orderInquiryList);
                return copyList;
            }
            return null;
        }

        /// <summary>
        /// ReciveType取得
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        /// <exception cref="DataNotExistsException"></exception>
        public static int GetState(string orderID)
        {
            OrderListEntity tpl;

            if (_orderInquiryList.TryGetValue(orderID, out tpl))
            {
                return tpl.ReciveType.Value;
            }
            else
            {
                throw new DataNotExistsException("OrderInquiryList:GetState  該当するOrderIDはありません");
            }

        }

        /// <summary>
        /// 約定番号(ExecutionId)取得
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        /// <exception cref="DataNotExistsException"></exception>
        public static string GetExecutionId(string orderID)
        {
            OrderListEntity tpl;
            if (_orderInquiryList.TryGetValue(orderID, out tpl))
            {
                return tpl.ExecutionID;
            }
            else
            {
                throw new DataNotExistsException("OrderInquiryList:GetState  該当するOrderIDはありません");
            }
        }
    }
}
