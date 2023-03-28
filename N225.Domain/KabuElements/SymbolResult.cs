using Codeplex.Data;
using N225.Domain.Exceptions;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace N225.Domain.Elements
{
    [DataContract]
    public class SymbolElements
    {
        [DataMember(Name = "Symbol")]
        public string Symbol { get; set; }

        [DataMember(Name = "SymbolName")]
        public string SymbolName { get; set; }

        [DataMember(Name = "DisplayName")]
        public string DisplayName { get; set; }

        [DataMember(Name = "Exchange")]
        public int Exchange { get; set; }

        [DataMember(Name = "ExchangeName")]
        public string ExchangeName { get; set; }

        [DataMember(Name = "BisCategory")]
        public string BisCategory { get; set; }

        [DataMember(Name = "TotalMarketValue")]
        public decimal TotalMarketValue { get; set; }

        [DataMember(Name = "TotalStocks")]
        public double TotalStocks { get; set; }

        [DataMember(Name = "TradingUnit")]
        public double TradingUnit { get; set; }

        [DataMember(Name = "FiscalYearEndBasic")]
        public double FiscalYearEndBasic { get; set; }

        [DataMember(Name = "PriceRangeGroup")]
        public string PriceRangeGroup { get; set; }

        [DataMember(Name = "KCMarginBuy")]
        public bool KCMarginBuy { get; set; }

        [DataMember(Name = "KCMarginSell")]
        public bool KCMarginSell { get; set; }

        [DataMember(Name = "MarginBuy")]
        public bool MarginBuy { get; set; }

        [DataMember(Name = "MarginSell")]
        public bool MarginSell { get; set; }

        [DataMember(Name = "UpperLimit")]
        public double UpperLimit { get; set; }

        [DataMember(Name = "LowerLimit")]
        public double LowerLimit { get; set; }

        [DataMember(Name = "Underlyer")]
        public string Underlyer { get; set; }

        [DataMember(Name = "DerivMonth")]
        public string DerivMonth { get; set; }

        [DataMember(Name = "TradeStart")]
        public int TradeStart { get; set; }

        [DataMember(Name = "TradeEnd")]
        public int TradeEnd { get; set; }

        [DataMember(Name = "StrikePrice")]
        public double StrikePrice { get; set; }

        [DataMember(Name = "PutOrCall")]
        public int PutOrCall { get; set; }

        [DataMember(Name = "ClearingPrice")]
        public decimal ClearingPrice { get; set; }

    }
    public class SymbolResult
    {
        private const int SymbolCol = 24;
        private static SymbolElements SymbolDeserialize(string stringJson)
        {

            SymbolElements SymbolData;


            //デシリアライズ
            SymbolData =
                    JsonConvert.DeserializeObject<SymbolElements>(stringJson);

            if (SymbolData.Symbol == string.Empty)
                return null;
            return SymbolData;
        }

        public static SymbolElements SymbolCheck(string value)
        {
            var objectJson = DynamicJson.Parse(value);

            if (objectJson.IsDefined("Code"))
            {
                // API Error
                string error = "Eoor Code:" + objectJson["Code"] + " " + objectJson["Message"];

                throw new APIResponsesException(error);
            }

            // multidimensional arrays
            SymbolElements ret = SymbolDeserialize(value);
            return ret;
        }
    }
}
