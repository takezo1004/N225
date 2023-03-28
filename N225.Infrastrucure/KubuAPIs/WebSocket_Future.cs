using N225.Domain.Elements;
using N225.Domain.Entities;
using N225.Domain.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace N225.Infrastrucure.KubuAPIs
{
    public class WebSocket_Future
    {
        public event EventHandler<WebSocketEntity> MessageEventHandler;
        public event EventHandler DisconnectedEventHndler;
        public event EventHandler CnnectedEventHndler;
        public WebSocket_Future()
        {

        }
        public void StartWebSocket()
        {
            var url = "ws://localhost:18080/kabusapi/websocket";
            //var uri = wsDomain + CustomRibbon._port + "/kabusapi/websocket";
            var ws = new ClientWebSocket();
            var con = ws.ConnectAsync(new Uri(url), CancellationToken.None);
            // 接続完了待ち
            con.Wait();

            if (con.Status == TaskStatus.RanToCompletion)
            {
                //ExcelFunctionController._websocketStream = true;
            }

            CnnectedEventHndler(this, new EventArgs());

            // 受信タスク開始
            Task.Run(() => RecvWebScoketData(ws));
        }

        private async Task RecvWebScoketData(ClientWebSocket ws)
        {
            //JsonConvert.DeserializeObjectに設定を指定して、null値の処理方法
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            // 受信バッファ
            // 1回のメッセージを受信できるのに十分な大きさ

            var buffer = new byte[4096];

            // websocket情報格納用の配列
            //var segment = new ArraySegment<byte>(buffer);
            //デシリアライズ
            BoardElement boardData = null;

            WebSocketReceiveResult result;
            ArraySegment<byte> segment;

            while (true)
            {
                // websocket情報格納用の配列
                segment = new ArraySegment<byte>(buffer);

                //デシリアライズ
                result = await ws.ReceiveAsync(segment, CancellationToken.None);
                //var message = Encoding.UTF8.GetString(buffer, 0, resultTask.Result.Count);


                //エンドポイントCloseの場合、処理を中断
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "OK",
                           CancellationToken.None);
                    break;
                }

                int count = result.Count;

                while (!result.EndOfMessage)
                {
                    if (count >= buffer.Length)
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.InvalidPayloadData,
                          "That's too long", CancellationToken.None);
                        return;
                    }
                    segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                    result = await ws.ReceiveAsync(segment, CancellationToken.None);

                    count += result.Count;
                }
                //メッセージを取得
                var message = Encoding.UTF8.GetString(buffer, 0, count);
                boardData =
                        JsonConvert.DeserializeObject<BoardElement>(message, settings);
                if (boardData != null)
                {
                    WebSocketEntity entity = new WebSocketEntity(
                                                   Convert.ToDouble(boardData.CurrentPrice),
                                                    boardData.AskPrice,
                                                    boardData.BidPrice);
                    MessageEventHandler(this, entity);
                }
                else
                {
                    //切断された
                    throw new APIResponsesException("WebSocket: Kabu Station Close");

                }

            }

            //kabu stationが閉じられた時の処理
            DisconnectedEventHndler(this, new EventArgs());
            return;
        }
    }
}
