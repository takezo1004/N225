﻿using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace N225.Infrastrucure.KubuAPIs
{
    // Response model
    public class TokenResponse
    {
        public string ResultCode { get; set; }
        public string Token { get; set; }
    }

    public static class GenerateToken
    {
        // Get Token API
        public static string GetToken(string apiPassword)
        {
            HttpClient client = new HttpClient();
            string Token = string.Empty;
            var obj = new
            {
                APIPassword = apiPassword
            };

            var url = "http://localhost:18080/kabusapi/token";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.SendAsync(request).Result;

                var JsonResult = JsonConvert.DeserializeObject<TokenResponse>(response.Content.ReadAsStringAsync().Result);

                Token = JsonResult.Token;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("{0} {1}", e, e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} {1}", ex, ex.Message);
            }
            return Token;
        }
    }
}
