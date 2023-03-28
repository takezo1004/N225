//using Codeplex.Data;
using Codeplex.Data;
using N225.Domain.Entities;
using N225.Domain.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace N225.Domain.KabuElements
{
    [JsonObject("PositionsElement")]
    public class PositionsElement
    {
        [JsonProperty("ExecutionID")]
        public string ExecutionID { get; set; }

        [JsonProperty("AccountType")]
        public int AccountType { get; set; }

        [JsonProperty("Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("SymbolName")]
        public string SymbolName { get; set; }

        [JsonProperty("Exchange")]
        public int Exchange { get; set; }

        [JsonProperty("ExchangeName")]
        public string ExchangeName { get; set; }

        [JsonProperty("SecurityType")]
        public int SecurityType { get; set; }

        [JsonProperty("ExecutionDay")]
        public int ExecutionDay { get; set; }

        [JsonProperty("Price")]
        public double Price { get; set; }

        [JsonProperty("LeavesQty")]
        public double LeavesQty { get; set; }

        [JsonProperty("HoldQty")]
        public double HoldQty { get; set; }

        [JsonProperty("Side")]
        public string Side { get; set; }

        [JsonProperty("Expenses")]
        public double Expenses { get; set; }

        [JsonProperty("Commission")]
        public double Commission { get; set; }

        [JsonProperty("CommissionTax")]
        public double CommissionTax { get; set; }

        [JsonProperty("ExpireDay")]
        public int ExpireDay { get; set; }

        [JsonProperty("MarginTradeType")]
        public int MarginTradeType { get; set; }

        [JsonProperty("CurrentPrice")]
        public double CurrentPrice { get; set; }

        [JsonProperty("Valuation")]
        public double Valuation { get; set; }

        [JsonProperty("ProfitLoss")]
        public object ProfitLoss { get; set; }

        [JsonProperty("ProfitLossRate")]
        public object ProfitLossRate { get; set; }
    }
    public class ErrorCode
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
    public class PositionsResult
    {
        private const int PositionCol = 21;

        private static List<PositionListEntity> PositionDataToList(string JsonString)
        {

            //デシリアライズ
            List<PositionsElement> PositionList;
            PositionList =
                    JsonConvert.DeserializeObject<List<PositionsElement>>(JsonString);

            int PositionRows = PositionList.Count;
            if (PositionRows == 0)
                return null;

            List<PositionListEntity> positionResultList = new List<PositionListEntity>();
            for (int i = 0; i < PositionRows; i++)
            {
                positionResultList.Add(new PositionListEntity(
                                0,
                            Convert.ToInt32(PositionList[i].ExecutionDay),
                            string.Empty,
                            0,
                            PositionList[i].Side,
                            PositionList[i].LeavesQty,
                            PositionList[i].HoldQty,
                            PositionList[i].Price,
                            Convert.ToDouble(PositionList[i].ProfitLoss),
                            PositionList[i].ExecutionID,
                            string.Empty
                        ));
            }
            return positionResultList;
        }
        public static List<PositionListEntity> PositionResultCheck(string value)
        {
            //エラーコードが有るかチェックする

            var objectJson = DynamicJson.Parse(value);

            if (objectJson.IsDefined("Code"))
            {

                Console.WriteLine("error code:{0} Message:{1}",
                                objectJson["Code"], objectJson["Message"]);
                string error = "Eoor Code:" + objectJson["Code"] + " " + objectJson["Message"];

                throw new APIResponsesException(error);

            }

            return PositionDataToList(value);

        }

    }
}
