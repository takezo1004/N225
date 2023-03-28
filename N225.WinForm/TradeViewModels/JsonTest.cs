using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace N225.WinForm.TradeViewModels
{
    public static class JsonTest
    {

        public static void TestJson()
        {
            var obj = new
            {
                Password = "takao102769",
                Symbol = "1234567",
                TradeType = 2,
                Side = "2",
                Qty = 1,
                price = 20000
            };

            string SerialDate = JsonConvert.SerializeObject(obj);
            Console.WriteLine(SerialDate);

            //dynamic型にしてデータを取得する
            dynamic Deserialize = JsonConvert.DeserializeObject(SerialDate);
            //keyで１データ取得
            string password = Deserialize.Password;

            //JObject型にキャストしてforeachで取得できる
            JObject Deseria = (JObject)JsonConvert.DeserializeObject(SerialDate);
            foreach (var m in Deseria)
            {
                Console.WriteLine($"Key:{m.Key} Value:{m.Value}");
            }
            foreach (var m in Deserialize)
            {
                Console.WriteLine($"data:{m}");
            }

        }
        public static void test2()
        {


            //string data = " {passphrase = 'abcdefg',alert_name = 'test',time = '2021-11-17T10:13:06Z'," +
            //    "exchange = 'OSE',ticker = 'NK225M1!',interval = 5 , bar = {time = '2021-11-17T10:10:00Z',"+                        open= 29705,
            //    "high = 29710,low = 29705,close = 29710,volume = 80 },strategy  = { postion_size =  1,";
            //    // "order_action = 'buy',order_contracts = 2 ,order_price = 29710, order_id = 'VltClsLE'," +
            //    //"market_position = 'long', market_position_size = 1,prev_market_position = 'short'," +
            //    //"prev_market_position_size = 1 }}";


        }
    }
}
