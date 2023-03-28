using Newtonsoft.Json;

namespace N225.Domain.KabuElements
{
    [JsonObject("TcpClientModel")]
    public class TcpClientModel
    {
        [JsonProperty("passphrase")]
        public string passphrase { get; set; }

        [JsonProperty("alert_name")]
        public string alert_name { get; set; }

        [JsonProperty("time")]
        public string time { get; set; }

        [JsonProperty("exchange")]
        public string exchange { get; set; }

        [JsonProperty("ticker")]
        public string ticker { get; set; }

        [JsonProperty("interval")]
        public double interval { get; set; }


        [JsonProperty("bar")]
        public TcpClientbar bar { get; set; }
        [JsonProperty("strategy")]
        public TcpclientStraregy strategy { get; set; }
    }

    [JsonObject("TcpClientbar")]
    public class TcpClientbar
    {
        [JsonProperty("time")]
        public string time { get; set; }

        [JsonProperty("open")]
        public double open { get; set; }

        [JsonProperty("high")]
        public double high { get; set; }

        [JsonProperty("low")]
        public double low { get; set; }

        [JsonProperty("close")]
        public double close { get; set; }

        [JsonProperty("volume")]
        public double volume { get; set; }

    }

    [JsonObject("TcpclientStraregy")]
    public class TcpclientStraregy
    {
        [JsonProperty("postion_size")]
        public double postion_size { get; set; }

        [JsonProperty("order_action")]
        public string order_action { get; set; }

        [JsonProperty("order_contracts")]
        public double order_contracts { get; set; }

        [JsonProperty("order_price")]
        public double order_price { get; set; }

        [JsonProperty("order_id")]
        public string order_id { get; set; }

        [JsonProperty("market_position")]
        public string market_position { get; set; }

        [JsonProperty("market_position_size")]
        public double market_position_size { get; set; }

        [JsonProperty("prev_market_position")]
        public string prev_market_position { get; set; }

        [JsonProperty("prev_market_position_size")]
        public double prev_market_position_size { get; set; }
    }

}
