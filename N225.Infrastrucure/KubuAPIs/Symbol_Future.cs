using System;
using System.Net.Http;


namespace N225.Infrastrucure.KubuAPIs
{
    public class Symbol_Future
    {
        public static string Symbol(string apiKey, string symbol, string exchange)
        {
            var XAPIkey = apiKey;
            string Symbol = symbol;
            string Exchange = exchange;
            string result = string.Empty;

            var url = "http://localhost:18080/kabusapi/symbol" + "/" + Symbol + "@" + Exchange + "?info=true";
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("X-API-KEY", apiKey);
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
