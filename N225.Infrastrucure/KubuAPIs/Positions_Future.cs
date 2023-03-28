using System;
using System.Net.Http;

namespace N225.Infrastrucure.KubuAPIs
{
    public static class Positions_Future
    {
        public static string Positions(string apikey, string product, string symbol)
        {

            var XAPIkey = apikey;


            var builder = new UriBuilder("http://localhost:18080/kabusapi/positions");
            var param = System.Web.HttpUtility.ParseQueryString(builder.Query);
            if (!string.IsNullOrEmpty(product))
            {
                param["Product"] = product;
            }
            if (!string.IsNullOrEmpty(symbol))
            {
                param["symbol"] = symbol;
            }

            builder.Query = param.ToString();
            string url = builder.ToString();
            string result = string.Empty;
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("X-API-KEY", XAPIkey);
                HttpResponseMessage response = client.SendAsync(request).Result;
                result = response.Content.ReadAsStringAsync().Result;
                //result = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
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
            //戻り値シリアルデータ
            return result;
        }
    }
}
