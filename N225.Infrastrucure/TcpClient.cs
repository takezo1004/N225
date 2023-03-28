using N225.Domain.Exceptions;
using N225.Domain.KabuElements;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace N225.Infrastrucure
{
    /// <summary>
    /// TcpClientクラス
    /// </summary>
    public class TcpClient
    {
        private static log4net.ILog _logger = log4net.LogManager.GetLogger(
                          System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private byte[] _buffer = new byte[1024];
        // 受信データのレスポンス
        private string response = string.Empty;
        private Socket client = null;
        private StateObject state = null;
        private string _host;
        private int _port;

        // 受信データイベント
        public event Action<object,EventArgs> Connected;
        public event Action<object, EventArgs> Disconnected;
        public event Action<object, TcpClientModel> DataReceived;
        
        public TcpClient()
        {
               
        }

        /// <summary>
        /// TcpClient スタート
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public async Task StartClient(string host, int port)
        {
            _host = host;
            _port = port;
            
            while (true)
            {
                try
                {
                    //コネクトできたない
                    //_logger.Info(" Not connected ");
                    //Disconnected?.Invoke(this, new EventArgs());
                    
                    //リモートホストに接続する
                    _logger.Info("リモートホストに接続します。");
                    
                    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    client =  await ConnectAsync(host, port,3000);
                    
                    _logger.Info("connect 正常");

                    //イベントで正常接続出来た事を通知する
                    
                    Connected?.Invoke(this,new EventArgs());

                    // ソケット情報を保持する為のオブジェクトを生成
                    state = new StateObject();
                    state.workSocket = client;
                    response = string.Empty;


                    while (client.Connected)
                    {
                        // ソケット情報を保持する為のオブジェクトを生成
                        state = new StateObject();
                        state.workSocket = client;
                        response = string.Empty;

                        int received = await client.ReceiveAsync(new ArraySegment<byte>(state.buffer), SocketFlags.None);
                        if (received == 0)
                        {
                            //Console.WriteLine("Connection closed by remote host.");
                            Disconnected?.Invoke(this, new EventArgs());
                            _logger.Error("TcpClient受信エラー");
                            break;
                        }
                        response = Encoding.UTF8.GetString(state.buffer, 0, received);
                        TcpClientModel Tcpdata =
                            JsonConvert.DeserializeObject<TcpClientModel>(response);
                        
                        DataReceived?.Invoke(this,Tcpdata);

                        _logger.Info("Receive Data Send to ViewModel");

                        //Console.WriteLine($"Received: {response}");
                                 
                    }    
                }
                catch (SocketException ex)
                {
                    //Console.WriteLine($"Error receiving data from server: {ex.Message}");
                    await Task.Delay(2000);
                    Disconnected?.Invoke(this, new EventArgs());
                    _logger.Info(" TcpClint Disconnected ");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// リモートホストに接続する
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="timeoutMs"></param>
        /// <returns></returns>
        public async Task<Socket> ConnectAsync(string host, int port, int timeoutMs)
        {
            while (true)
            {
                try
                {
                    // TCP/IPのソケットを作成
                    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    await client.ConnectAsync(host,port);

                    //リモートホストに接続完了
                    return client;
                }
                catch (SocketException ex)
                {
                    await Task.Delay(timeoutMs);
                }
            }
        }

        /// <summary>
        /// 非同期処理でソケット情報を保持する為のオブジェクト
        /// </summary>
        public class StateObject
        {
            // 受信バッファサイズ
            public const int BufferSize = 1024;

            // 受信バッファ
            public byte[] buffer = new byte[BufferSize];

            // 受信データ
            public StringBuilder sb = new StringBuilder();

            // ソケット
            public Socket workSocket = null;
        }

        /// <summary>
        /// 受信終了メソッド
        /// </summary>
        public void Disconnect()
        {
            if (client.Connected)
            {
                client.Shutdown(SocketShutdown.Both);
            }
            client.Close();
            Disconnected?.Invoke(this, EventArgs.Empty);
        }
    }
}

