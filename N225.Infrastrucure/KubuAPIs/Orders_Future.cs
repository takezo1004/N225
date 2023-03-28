using N225.Domain.Entities;
using System;
using System.Net.Http;

namespace N225.Infrastrucure.KubuAPIs
{
    public sealed class Orders_Future
    {
        public static string Orders(string apikey, OrderEntity entity)
        {
            string product = entity.Product;
            string id = entity.OrderID;
            string updTime = entity.Updtime;
            string details = entity.Details;
            string symbol = entity.Symbol;
            string state = entity.State;
            string side = entity.Side;
            string cashMargin = entity.Cashmargin;

            var XAPIkey = apikey;

            var builder = new UriBuilder("http://localhost:18080/kabusapi/orders");
            var param = System.Web.HttpUtility.ParseQueryString(builder.Query);
            if (!string.IsNullOrEmpty(product))
            {
                param["product"] = product;
            }
            if (!string.IsNullOrEmpty(id))
            {
                param["id"] = id;
            }
            if (!string.IsNullOrEmpty(updTime))
            {
                param["updtime"] = updTime;
            }
            if (!string.IsNullOrEmpty(details))
            {
                param["details"] = details;
            }
            if (!string.IsNullOrEmpty(symbol))
            {
                param["symbol"] = symbol;
            }
            if (!string.IsNullOrEmpty(state))
            {
                param["state"] = state;
            }
            if (!string.IsNullOrEmpty(side))
            {
                param["side"] = side;
            }
            if (!string.IsNullOrEmpty(cashMargin))
            {
                param["cashmargin"] = cashMargin;
            }
            builder.Query = param.ToString();

            string url = builder.ToString();
            string result;
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("X-API-KEY", XAPIkey);
                HttpResponseMessage response = client.SendAsync(request).Result;
                result = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine("{0} \n {1}", JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result), response.Headers);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("{0} {1}", e, e.Message);
                throw e;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} {1}", ex, ex.Message);
                throw ex;
            }

            return result;
        }
    }
}
