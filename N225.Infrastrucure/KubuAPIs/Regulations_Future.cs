using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace N225.Infrastrucure.KubuAPIs
{
    class Regulations_Future
    {
        static void Regulations(string apikey, string[] args)
        {
            var url = "http://localhost:18080/kabusapi/regulations/5401@1";
            var XAPIkey = apikey;
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("X-API-KEY", XAPIkey);
                HttpResponseMessage response = client.SendAsync(request).Result;
                Console.WriteLine("{0} \n {1}", JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result), response.Headers);
                Console.ReadKey();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("{0} {1}", e, e.Message);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} {1}", ex, ex.Message);
                Console.ReadKey();
            }

            Console.ReadKey();
        }
    }
}
