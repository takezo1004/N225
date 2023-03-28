using Codeplex.Data;
using N225.Domain.Exceptions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace N225.Infrastrucure.KubuAPIs
{
    class CancelOrder_Future
    {
        public static string CancelOrder(string apikey, string orderId, string pasword)
        {
            var obj = new
            {
                OrderId = orderId,
                Password = pasword
            };
            var url = "http://localhost:18080/kabusapi/cancelorder";

            string OrderId;
            double result;
            var XAPIkey = apikey;
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Put, url);
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
                throw e;
            }
            return OrderId;
        }
    }
}
