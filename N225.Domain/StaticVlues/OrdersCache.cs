using N225.Domain.Entities;
using N225.Domain.Exceptions;
using N225.Domain.ValueObjects;
using System.Collections.Generic;

namespace N225.Domain.StaticVlues
{
    public class OrdersCache
    {
        static OrderListEntity tpl;

        /// <summary>
        /// ディクショナリー　OrderList
        /// </summary>

        public static Dictionary<string, OrderListEntity> _ordersCache =
                                    new Dictionary<string, OrderListEntity>();

        /// <summary>
        /// ディクショナリー　追加
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="DataNotExistsException"></exception>
        public static void Add(OrderListEntity entity)
        {
            if (entity != null && string.IsNullOrEmpty(entity.OrderID) == false)
            {
                _ordersCache.Add(entity.OrderID, entity);
            }
            else
            {
                throw new DataNotExistsException("OrdersCache:Add Entity追加出来ません");
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="DataNotExistsException"></exception>
        public static void UpDate(OrederManagerEntity entity)
        {
            string _orderId = entity.Id;
            //_reciveType, 0, Convert.ToInt32(e.CumQty)
            if (_ordersCache.TryGetValue(_orderId, out tpl))
            {
                tpl.ReciveType = new ReciveType(entity.ReciveType);
                tpl.OrderQty = entity.OrderQty;
                tpl.CumQty = entity.CumQty;
                tpl.Price = entity.Price;
            }
            else
            {
                throw new DataNotExistsException("OrdersCache OrderIDはありません");
            }

        }

        /// <summary>
        /// 約定番号(ExecutionID)取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DataNotExistsException"></exception>
        public static string GetExecutionID(string id)
        {
            string orderID = id;


            if (_ordersCache.TryGetValue(orderID, out tpl))
            {
                return tpl.ExecutionID;
            }
            else
            {
                throw new DataNotExistsException("OrdersCache OrderIDはありません");
            }

        }

        /// <summary>
        /// ReciveType取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DataNotExistsException"></exception>
        public static int GetState(string id)
        {
            string orderID = id;


            if (_ordersCache.TryGetValue(orderID, out tpl))
            {
                return tpl.ReciveType.Value;
            }
            else
            {
                throw new DataNotExistsException("OrdersCache OrderIDはありません");
            }

        }

        /// <summary>
        /// 要素取得
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        /// <exception cref="DataNotExistsException"></exception>
        public static OrderListEntity GetsEntity(string orderID)
        {
            if (_ordersCache.TryGetValue(orderID, out tpl))
            {
                return tpl;
            }
            else
            {
                throw new DataNotExistsException("OrdersCache OrderIDに該当するデータはありません");
            }
        }
    }
}
