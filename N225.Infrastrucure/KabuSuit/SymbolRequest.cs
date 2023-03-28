using N225.Domain;
using N225.Domain.Elements;
using N225.Domain.Entities;
using N225.Infrastrucure.KubuAPIs;
using System;

namespace N225.Infrastrucure.KabuSuit
{
    /// <summary>
    /// 中心限月（ラージと同じ限月のN225mini Symbol,SymbolNameを取得する
    /// </summary>
    public class SymbolRequest
    {
        /// <summary>
        /// コンストラクタ引数ナシ
        /// </summary>
        public SymbolRequest()
        {
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="futureCode">string 先物コード</param>
        /// <param name="derivMonth">int 限月</param>
        public SymbolRequest(
                              string apiKey, string futureCode = "NK225", int derivMonth = 0)
        {
            APIKey = apiKey;
            FutureCode = futureCode;
            DerivMonth = derivMonth;
        }
        private string APIKey { get; set; }
        private string FutureCode { get; set; }
        public int DerivMonth { get; set; }

        //public  int ContractMonth(string DerivMonth, int TradeEnd)
        public int ContractMonth(string symbol, string symbolName)
        {
            symbol = symbol.Replace("18", "19");

            string[] array = symbolName.Split(' ')[1].Split('/');

            string yyyyMM = "20" + array[0] + array[1];

            return Convert.ToInt32(yyyyMM);
        }

        /// <summary>
        /// 銘柄コードの取得
        /// </summary>
        /// <returns></returns>
        public SymbolNameResultEntity Request()
        {
            string ret;
            string Exchange = "2";

            // ラージの中心限月を取得する
            ret = Symbolname_Future.Symbolname(APIKey, FutureCode, 0);
            SymbolNameResultEntity resultList = SymbolNameRessult.SymbolNameCheck(ret);
            string Symbol = resultList.Symbol;
            string SymbolName = resultList.SymbolName;

            //ラージの中心限月の銘柄情報 限月取得：DerivMonth "2022/09",取引終了日：TradeEnd":20220908：
            ret = Symbol_Future.Symbol(APIKey, Symbol, Exchange);
            SymbolElements result = SymbolResult.SymbolCheck(ret);
            
            //今日の月を取得する
            string f = "yyyyMMdd";
            int TodayMouth = DateTime.Now.Month;

            var timeNow = DateTime.Now.TimeOfDay;

            DateTime TradeEnd = DateTime.ParseExact(Convert.ToString(result.TradeEnd), f, null);
            int TradeEndMonth = TradeEnd.Month;
            int TradeEndYear = TradeEnd.Year;

            DateTime Today = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"));
            double diff = (TradeEnd.Date - Today.Date).TotalDays;

            int compdd = TradeEnd.CompareTo(Today);


            string _derivMonth = string.Empty;

            if (TradeEndMonth == 3)
            {
                if (diff == 0)
                {
                    _derivMonth = "03";
                    if (timeNow > Shared.EndTime)
                    {
                        _derivMonth = "06";
                    }
                }
                else if (diff < 0)
                {
                    _derivMonth = "06";
                }
                else
                    _derivMonth = "03";
            }
            else if (TradeEndMonth == 6)
            {
                if (diff == 0)
                {
                    _derivMonth = "06";
                    if (timeNow > Shared.EndTime)
                    {
                        _derivMonth = "09";
                    }
                }
                else if (diff < 0)
                {
                    _derivMonth = "09";
                }
                else
                    _derivMonth = "06";
            }
            else if (TradeEndMonth == 9)
            {
                if (diff == 0)
                {
                    _derivMonth = "09";
                    if (timeNow > Shared.EndTime)
                    {
                        _derivMonth = "12";
                    }
                }
                else if (diff < 0)
                {
                    _derivMonth = "12";
                }
                else
                    _derivMonth = "09";
            }
            else if (TradeEndMonth == 12)
            {
                if (diff == 0)
                {
                    _derivMonth = "12";
                    if (timeNow > Shared.EndTime)
                    {
                        _derivMonth = "03";
                        TradeEndYear = TradeEndYear + 1;
                    }
                }
                else if (diff < 0)
                {
                    _derivMonth = "03";
                    TradeEndYear = TradeEndYear + 1;
                }
                else
                    _derivMonth = "12";
            }


            string yyyyMM = (Convert.ToString(TradeEndYear) + "0000").Substring(0, 4) + _derivMonth;
            
                int DerivMonth = Convert.ToInt32(yyyyMM);

            // ラージの中心限月を取得する
            ret = Symbolname_Future.Symbolname(APIKey, FutureCode, DerivMonth);
            resultList = SymbolNameRessult.SymbolNameCheck(ret);
            Symbol = resultList.Symbol;
            SymbolName = resultList.SymbolName;

            //int DerivMonth = ContractMonth(result.DerivMonth, result.TradeEnd);
            DerivMonth = ContractMonth(Symbol, SymbolName);

            //m225 mini symbol 取得
            ret = Symbolname_Future.Symbolname(APIKey, "NK225mini", DerivMonth);
            resultList = SymbolNameRessult.SymbolNameCheck(ret);

            return resultList;
        }

    }

}
