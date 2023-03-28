using N225.Domain.Entities;
using N225.Domain.Exceptions;
using N225.Domain.ValueObjects;
using System;
using System.Collections.Generic;


namespace N225.Domain.StaticVlues
{
    public class StrategyListCash
    {
        static StrategyViewEntity tpl;

        /// <summary>
        /// ディクショナリー　Strategy
        /// </summary>

        public static Dictionary<String, StrategyViewEntity> _strategyVew =
                                new Dictionary<string, StrategyViewEntity>();

        /// <summary>
        /// ディクショナリーのコピーを取得
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, StrategyViewEntity> IsList()
        {
            if (_strategyVew.Count > 0)
            {
                //_strategyVewをコピーして渡す
                Dictionary<string, StrategyViewEntity> copyList =
                            new Dictionary<string, StrategyViewEntity>(_strategyVew);
                return copyList;
            }
            return null;
        }

        /// <summary>
        /// 要素数
        /// </summary>
        /// <returns></returns>
        public static int GetCount()
        {
            return _strategyVew.Count;
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="Exception"></exception>
        public static void Add(StrategyViewEntity entity)
        {
            string key = entity.Name + Convert.ToString(entity.Interval);
            try
            {
                _strategyVew.Add(key, entity);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 要素削除
        /// </summary>
        /// <param name="name"></param>
        /// <param name="interval"></param>
        public static void Remove(string name, int interval)
        {
            string key = name + Convert.ToString(interval);
            _strategyVew.Remove(key);
        }

        /// <summary>
        /// 要素取得
        /// </summary>
        /// <param name="name"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        /// <exception cref="KabuException"></exception>
        public static StrategyViewEntity GetsEntity(string name, int interval)
        {
            string key = name + Convert.ToString(interval);
            if (_strategyVew.TryGetValue(key, out tpl))
            {
                return tpl;
            }
            else
            {
                throw new KabuException("StrategyListCash Nameに該当するデータはありません");
            }
        }

        /// <summary>
        /// checkデータ取得
        /// </summary>
        /// <param name="name"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        /// <exception cref="KabuException"></exception>
        public static bool GetCheck(string name, int interval)
        {
            string key = name + Convert.ToString(interval);
            if (_strategyVew.TryGetValue(key, out tpl))
            {
                return tpl.Check;
            }
            else
            {
                throw new KabuException("StrategyListCash Nameに該当するデータはありません");
            }
        }

        /// <summary>
        /// strategy signal 受信データを該当keyのディクショナリーに保存する
        /// </summary>
        /// <param name="fileld"></param>
        /// <exception cref="KabuException"></exception>
        public static void UpDate(InputOrder fileld)
        {
            string key = fileld.Strategy + Convert.ToString(fileld.Interval);
            
            if (_strategyVew.TryGetValue(key, out tpl))
            {
                tpl.DateTime = DateTime.Now.ToString("MM/dd HH:mm");
                tpl.TradeType = new CashMargin(fileld.TradeType + 1).DisplayValue;
                tpl.Side = new Domain.ValueObjects.Side(fileld.Side).DisplayValue;
                tpl.Price = Convert.ToString(fileld.Price);

            }
            else
            {
                throw new KabuException("StrategyListCash 該当するstrategyはありません");
            }
        }

        /// <summary>
        /// checkデータ更新
        /// </summary>
        /// <param name="check"></param>
        /// <param name="name"></param>
        /// <param name="interval"></param>
        /// <exception cref="KabuException"></exception>
        public static void UpdateCheck(bool check, string name, int interval)
        {
            string key = name + Convert.ToString(interval);
            if (_strategyVew.TryGetValue(key, out tpl))
            {
                tpl.Check = check;

            }
            else
            {
                throw new KabuException("StrategyListCash Nameに該当するデータはありません");
            }
        }
    }
}
