using N225.Domain.CommonConst;
using N225.Domain.Entities;
using N225.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace N225.Domain.StaticVlues
{
    public static class PositionsCache
    {
        /// <summary>
        /// ディクショナリー　PositionList
        /// key executionID
        /// </summary>
        public static Dictionary<string, PositionListEntity> _positionsCache =
                                    new Dictionary<string, PositionListEntity>();

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="entity"></param>
        public static void Add(PositionListEntity entity)
        {
            _positionsCache[entity.ExecutionID] = entity;

        }

        /// <summary>
        /// 売買区分取得
        /// </summary>
        /// <param name="executionID"></param>
        /// <returns></returns>
        /// <exception cref="DataNotExistsException"></exception>
        public static string GetSide(string executionID)
        {
            PositionListEntity tpl;

            if (_positionsCache.TryGetValue(executionID, out tpl))
            {
                return tpl.Side.Value;
            }
            else
            {
                throw new DataNotExistsException("ExecutionIDはありません");
            }

        }

        /// <summary>
        /// 保有枚数
        /// </summary>
        /// <param name="executionID"></param>
        /// <returns></returns>
        /// <exception cref="DataNotExistsException"></exception>
        public static double GetLeaveQty(string executionID)
        {
            PositionListEntity tpl;

            if (_positionsCache.TryGetValue(executionID, out tpl))
            {
                return tpl.LeaveQty;
            }
            else
            {
                throw new DataNotExistsException("返済枚数はありません");
            }

        }

        /// <summary>
        /// 要素削除
        /// </summary>
        /// <param name="executionID"></param>
        /// <exception cref="DataNotExistsException"></exception>
        public static void Remove(string executionID)
        {
            PositionListEntity tpl;
            if (_positionsCache.TryGetValue(executionID, out tpl))
            {
                _positionsCache.Remove(executionID);
            }
            else
            {
                throw new DataNotExistsException("ExecutionIDはありません");
            }

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="DataNotExistsException"></exception>
        public static void UpDate(PositionManagerEntity entity)
        {
            string executionID = entity.ExecutionID;
            PositionListEntity tpl;
            if (_positionsCache.TryGetValue(executionID, out tpl))
            {
                tpl.LeaveQty = entity.CumQty;
                tpl.HoldQty = entity.OrderQty;
            }
            else
            {
                throw new DataNotExistsException("ExecutionIDはありません");
            }
        }
        /// <summary>
        /// ExecutionIdの有無
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public static bool IsExecutionId(string id)
        {
            PositionListEntity tpl;
            return _positionsCache.TryGetValue(id, out tpl);
        }

        /// <summary>
        /// 約定枚数変更
        /// </summary>
        /// <param name="executionID"></param>
        /// <param name="qty"></param>
        /// <exception cref="DataNotExistsException"></exception>
        public static void AddHoldQty(string executionID, double qty)
        {
            PositionListEntity tpl;
            if (_positionsCache.TryGetValue(executionID, out tpl))
            {

                tpl.HoldQty = qty;
            }
            else
            {
                throw new DataNotExistsException("ExecutionIDはありません");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strtegy"></param>
        /// <param name="interval"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public static List<string> GetExecutionId(string strtegy, int interval, string side)
        {
            string id = string.Empty;
            string _side = side;
            List<PositionListEntity> Items = new List<PositionListEntity>();

            //strtegy ,intervalと同じものの新しいディクショナリーを作成する。
            
            var NewDic = _positionsCache.Where(x => x.Value.TradeMode.Value == 1 && x.Value.Strategy == strtegy && x.Value.Iterval ==
                          interval && x.Value.Side.Value == _side).ToDictionary(auto => auto.Key, auto => auto.Value.OrderID);
            return NewDic.Keys.ToList();
        }

        public static string GetExecutionId(string strtegy, int interval)
        {
            PositionListEntity tpl;
            string id = string.Empty;

            if (_positionsCache.TryGetValue(strtegy, out tpl))
                id = tpl.ExecutionID;
            return id;
        }
    }
}
