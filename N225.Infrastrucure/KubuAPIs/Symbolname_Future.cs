using System;
using System.Net.Http;
using System.Web;

namespace N225.Infrastrucure.KubuAPIs
{
    public class Symbolname_Future
    {
        public static string Symbolname(string apiKey, string futureCode, int derivMonth)
        {
            string FutureCode = futureCode;
            int DerivMonth = derivMonth;

            var builder = new UriBuilder("http://localhost:18080/kabusapi/symbolname/future");
            var param = HttpUtility.ParseQueryString(builder.Query);
            var XAPIkey = apiKey;

            if (!string.IsNullOrEmpty(FutureCode))
            {
                param["FutureCode"] = FutureCode;
            }
            param["DerivMonth"] = DerivMonth.ToString();

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
