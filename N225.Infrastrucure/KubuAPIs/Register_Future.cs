using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace N225.Infrastrucure.KubuAPIs
{
    public class Register_Future
    {
        public static string Register(string apikey, object symbol)
        {
            //var obj = new
            //{
            //    Symbols = new[]
            //    {
            //        new { Symbol = "8001", Exchange = 1},
            //        new { Symbol = "101" , Exchange = 1 },
            //        new { Symbol = "8316" , Exchange = 1 },
            //        new { Symbol = "5020" , Exchange = 1 },
            //        new { Symbol = "6727" , Exchange = 1 }
            //    }
            //};
            var obj = symbol;
            var url = "http://localhost:18080/kabusapi/register";
            var XAPIkey = apikey;
            string result = string.Empty;
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Put, url);
                request.Headers.Add("ContentType", "application/json");
                request.Headers.Add("X-API-KEY", XAPIkey);
                request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
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
            //戻り値シリアルデータ
            return result;
        }
    }
}
