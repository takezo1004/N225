using Codeplex.Data;
using N225.Domain.Exceptions;
using N225.Domain.Repositories;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace N225.Infrastrucure.KubuAPIs
{
    public class Sendorder_Future_new
    {
        public static string SendOrder(string apiKey, SendOrderEntity entity)
        {

            var obj = new
            {
                Password = entity.Pssword,
                Symbol = entity.Symbol,
                Exchange = entity.Exchange,
                TradeType = entity.TradeType,
                TimeInForce = entity.TimeInForce,
                Side = entity.Side,
                Qty = entity.Qty,
                FrontOrderType = entity.FrontOrderType,
                Price = entity.Price,
                ExpireDay = entity.ExpireDay,

                ReverseLimitOrder = new
                {
                    TriggerPrice = entity.TriggerPrice,
                    UnderOver = entity.UnderOver,
                    AfterHitOrderType = entity.AfterHitOrderType,
                    AfterHitPrice = entity.AfterHitPrice
                }

            };
            var url = "http://localhost:18080/kabusapi/sendorder/future";

            string OrderId;
            double result;
            var XAPIkey = apiKey;
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("ContentType", "application/json");
                request.Headers.Add("X-API-KEY", XAPIkey);
                request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.SendAsync(request).Result;

                string content = response.Content.ReadAsStringAsync().Result;

                var objectJson = DynamicJson.Parse(content);

                if (objectJson.IsDefined("Code"))
                {
                    // API Error
                    string error = "Eoor Code:" + objectJson["Code"] + " " + objectJson["Message"];

                    throw new APIResponsesException(error);
                }
                result = objectJson.Result;
                OrderId = objectJson.OrderId;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("{0} {1}", e, e.Message);
                throw e;
            }
            return OrderId;
        }
    }
}
