using Codeplex.Data;
using N225.Domain.Entities;
using N225.Domain.Exceptions;
using Newtonsoft.Json;


namespace N225.Domain.Elements
{
    [JsonObject("SymbolNameElement")]
    public class SymbolNameElement
    {
        [JsonProperty("Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("SymbolName")]
        public string SymbolName { get; set; }
    }

    public class SymbolNameRessult
    {
        private static SymbolNameResultEntity SymbolNameToList(string stringJson)
        {
            SymbolNameElement SymbolNameList = null;
            //デシリアライズ
            SymbolNameList =
                    JsonConvert.DeserializeObject<SymbolNameElement>(stringJson);

            if (SymbolNameList.Symbol == string.Empty)
                return null;

            return new SymbolNameResultEntity(SymbolNameList.Symbol,
                                            SymbolNameList.SymbolName);
        }

        public static SymbolNameResultEntity SymbolNameCheck(string json)
        {
            var objectJson = DynamicJson.Parse(json);

            // レスポンス項目に「Code」がある場合、APIエラーがあったと判断
            if (objectJson.IsDefined("Code"))
            {
                string error = "Eoor Code:" + objectJson["Code"] + " " + objectJson["Message"];

                throw new APIResponsesException(error);
            }

            return SymbolNameToList(json);
        }
    }
}
