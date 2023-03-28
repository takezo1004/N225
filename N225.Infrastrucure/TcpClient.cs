using N225.Domain.Exceptions;
using N225.Domain.KabuElements;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace N225.Infrastrucure
{
    public class TcpClient
    {
        private Socket mySocket = null;
        private MemoryStream myMs;
        private readonly object syncLock = new object();
        private Encoding enc = Encoding.UTF8;

        public delegate void ReceiveEventHndler(object sender, TcpClientModel e);
        public event ReceiveEventHndler OnReciveData;

        public delegate void DisconnectedEventHndler(object sender, EventArgs e);
        public event DisconnectedEventHndler OnDisconnected;

        public delegate void ConnectedEventHndler(EventArgs e);
        public event ConnectedEventHndler OnConnected;

        public bool IsClosed
        {
            get { return mySocket == null; }
        }

        public virtual void Dispose()
        {
            Close();
        }


        public TcpClient()
        {
            mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public TcpClient(Socket sc)
        {
            mySocket = sc;

        }

        private void Close()
        {
            Debug.WriteLine("Close" + " ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            if (mySocket != null)
            {
                mySocket.Shutdown(SocketShutdown.Both);
                mySocket.Close();
                mySocket = null;
            }

            if (myMs != null)
            {
                myMs.Dispose();
                myMs.Close();
                myMs = null;
            }

            OnDisconnected(this, new EventArgs());

        }
        public void Connect(string host, int port)
        {
            IPEndPoint ipEnd = new IPEndPoint(Dns.GetHostAddresses(host)[1], port);

            //mySocket.Connect(ipEnd);

            mySocket.BeginConnect(ipEnd, new AsyncCallback(ConnectCallback), mySocket);


        }

        private void ConnectCallback(IAsyncResult ar)
        {
            Debug.WriteLine("Connecte" + "ThreadID" + Thread.CurrentThread.ManagedThreadId);
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());

                OnConnected(new EventArgs());

                // 受信タスク開始
                StartRecive();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void StartRecive()
        {
            Debug.WriteLine("Connecte" + "ThreadID" + Thread.CurrentThread.ManagedThreadId);
            byte[] rcvBuff = new byte[1024];

            myMs = new MemoryStream();

            mySocket.BeginReceive(rcvBuff, 0, rcvBuff.Length, SocketFlags.None,
                                  new AsyncCallback(ReciveDataCallback), rcvBuff);

        }

        private void ReciveDataCallback(IAsyncResult ar)
        {
            try
            {
                Debug.WriteLine("Connecte" + "ThreadID" + Thread.CurrentThread.ManagedThreadId);

                int len = -1;
                bool aa = IsClosed;

                if (IsClosed)
                {
                    return;
                }
                len = mySocket.EndReceive(ar);

                //切断された
                if (len <= 0)
                {
                    Close();
                    return;
                }

                byte[] rcvBuff = (byte[])ar.AsyncState;
                myMs.Write(rcvBuff, 0, len);


                if (myMs.Length >= 2)
                {
                    string rcvStr = enc.GetString(myMs.ToArray());
                    var message = Encoding.UTF8.GetString(rcvBuff, 0, len);
                    TcpClientModel Tcpdata =
                            JsonConvert.DeserializeObject<TcpClientModel>(rcvStr);

                    //dynamic objectJson = DynamicJson.Parse(rcvStr);
                    //Console.WriteLine(objectJson);
                    //string pass = objectJson["passphrase"];
                    //double time = objectJson["bar"]["open"];
                    //double size = objectJson["strategy"]["postion_size"];

                    OnReciveData(this, Tcpdata);
                }
                myMs.Close();
                myMs = new MemoryStream();
                if (!IsClosed)
                {
                    mySocket.BeginReceive(rcvBuff, 0, rcvBuff.Length, SocketFlags.None,
                                  new AsyncCallback(ReciveDataCallback), rcvBuff);
                }

            }
            catch (Exception ex)
            {
                var exceptonBase = ex as ExceptionBase;
                if (exceptonBase == null)
                {
                    Console.WriteLine("Massege" + ex.Message);
                    Close();
                }
            }
            finally
            {
            }
        }
        /// <summary>
        /// メッセージを送信する
        /// </summary>
        /// <param name="str"></param>
        public void Send(string str)
        {
            Debug.WriteLine("Send" + " ThreadID:" + Thread.CurrentThread.ManagedThreadId);

            if (!IsClosed)
            {
                //文字列をBYTE配列に変換
                byte[] sendBytes = enc.GetBytes(str + "\r\n");
                lock (syncLock)
                {
                    //送信
                    mySocket.Send(sendBytes);
                }
            }
        }
    }
}
