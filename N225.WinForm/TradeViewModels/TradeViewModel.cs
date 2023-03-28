using N225.Domain;
using N225.Domain.CommonConst;
using N225.Domain.Elements;
using N225.Domain.Entities;
using N225.Domain.KabuElements;
using N225.Domain.Modules;
using N225.Domain.Modules.Utils;
using N225.Domain.Repositories;
using N225.Domain.ValueObjects;
using N225.Infrastrucure;
using N225.Infrastrucure.KabuSuit;
using N225.Infrastrucure.KubuAPIs;
using N225.WinForm.BackgrondWorkers;
using N225.WinForm.Modules;
using N225.WinForm.Strategys;
using N225.WinForm.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Side = N225.Domain.CommonConst.Side;

namespace N225.WinForm.TradeViewModels
{
    public class TradeViewModel : ViewModelBase
    {
        //接続先ホスト名
        //string host = "192.168.0.100";
        private string host = "localhost";
        //接続先ポート
        private int port = 8000;

        TcpClient tClient = new TcpClient();
        WebSocket_Future client = new WebSocket_Future();
        public event EventHandler<string> MessageEventHandler;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public TradeViewModel()
        {
             
            tClient.Connected += TcpClient_OnConnected;
            tClient.Disconnected += TcpClient_OnDiscconnected;
            tClient.DataReceived += TcpClient_OnReciveData;
            //tClient.Disconnected += new TcpClient.DisconnectedEventHndler(TcpClient_OnDiscconnected);
            //tClient.DataReceived += new TcpClient.ReceiveEventHndler(TcpClient_OnReciveData);

            client.MessageEventHandler += this.Client_OnReciveData;
            client.CnnectedEventHndler += Client_CnnectedEventHndler;
            client.DisconnectedEventHndler += Client_DisconnectedEventHndler;
        }

        
        public SendOrderEntity _entity;

        private string _symbl = string.Empty;

        private int _selectedTimeInForce1 = 0;
        private int _selectedTimeInForce2 = 0;
        private BindingList<PositionListView> _positionList =
                                    new BindingList<PositionListView>();

        private BindingList<StrategyViewList> _strategyViews =
                                    new BindingList<StrategyViewList>();

        private string _currenPrice;
        private string _askPrice = string.Empty;
        private string _bidPrice = string.Empty;
        public string _message = string.Empty;

        public Dispatcher dispatcher { get; set; } = null;
        public bool ClientConnected { get; set; } = false;
        public bool TcpConnected { get; set; } = false;

        public bool InquityOrderState { get; set; }
        public bool AutoButoon { get; set; } = false;
        public string XAPIkey { get; set; }

        public int TradeMode { get; set; } = 0;
        public string Strategy { get; set; } = string.Empty;
        public int Interval { get; set; } = 0;
        public string Symbol { get; set; }
        public string SymbolName
        {
            get { return _symbl; }
            set
            {
                SetPropety(ref _symbl, value);
            }
        }

        public int SelectedTimeInForce1
        {
            get { return _selectedTimeInForce1; }
            set
            {
                SetPropety(ref _selectedTimeInForce1, value);
            }
        }
        public int SelectedTimeInForce2
        {
            get { return _selectedTimeInForce2; }
            set
            {
                SetPropety(ref _selectedTimeInForce2, value);
            }
        }
        public double Qty { get; set; }

        public double Price { get; set; }

        public double StopPrice { get; set; }

        public string CurrentPrice
        {
            get { return _currenPrice; }
            set
            {
                SetPropety(ref _currenPrice, value);
            }
        }

        public string AskPrice
        {
            get { return _askPrice; }
            set
            {
                SetPropety(ref _askPrice, value);
            }
        }
        public string BidPrice
        {
            get { return _bidPrice; }
            set
            {
                SetPropety(ref _bidPrice, value);
            }
        }
        public string Message
        {
            get { return _message; }
            set
            {
                SetPropety(ref _message, value);
            }
        }

        /// <summary>
        /// コンボボックス 有効期限条件バウンドリスト　
        /// </summary>
        public BindingList<TimeInForceEntity> TimeInForce1
                    { get; set; } = new BindingList<TimeInForceEntity>();
        
        public BindingList<TimeInForceEntity> TimeInForce2
                    { get; set; } = new BindingList<TimeInForceEntity>();

        /// <summary>
        /// 有効期限条件 成行設定１
        /// </summary>
        public void TimeInForceBaind1()
        {
            TimeInForce1.Add(new TimeInForceEntity(
                                TimeInForce.FAK, TimeInForce.FAKName )); 
            
            TimeInForce1.Add(new TimeInForceEntity(
                                TimeInForce.FOK, TimeInForce.FOKName ));
            SelectedTimeInForce1 = 2;

        }

        /// <summary>
        /// 有効期限条件 指値設定１
        /// </summary>
        public void TimeInForceBaind2()
        {
            TimeInForce2.Add(new TimeInForceEntity(
                                    TimeInForce.FAS,TimeInForce.FASName ));
            TimeInForce2.Add(new TimeInForceEntity(
                                    TimeInForce.FAK, TimeInForce.FAKName ));
            TimeInForce2.Add(new TimeInForceEntity(
                                    TimeInForce.FOK, TimeInForce.FOKName ));
            SelectedTimeInForce2 = 1;
        }

        
        /// <summary>
        /// Order GridView バインドリスト
        /// </summary>
        public BindingList<OrderListListView> OrderList
        { get; set; } = new BindingList<OrderListListView>();

        /// <summary>
        /// Position Gridview バインドリスト
        /// </summary>
        public BindingList<PositionListView> PositionList
        {
            get { return _positionList; }
            set
            {
                SetPropety(ref _positionList, value);
            }
        }

        /// <summary>
        /// Strategy GridView バインドリスト
        /// </summary>
        public BindingList<StrategyViewList> StrategyViews
        {
            get { return _strategyViews; }
            set
            {
                SetPropety(ref _strategyViews, value);
            }
        }

        /// <summary>
        /// Strategy　gridview データ削除
        /// </summary>
        public void RemoveStrategyViews()
        {
            StrategyViews.Clear();
        }

        /// <summary>
        /// アプリケーションの初期化
        /// </summary>
        public void Initialize()
        {
            bool error = true;

            TimeInForceBaind1();
            TimeInForceBaind2();
            DirectoryUtils.SafeCreateDirectory();
            SettingPassword();
            XAPIkey = KabuSuiteApiToken.Token(Shared.APIPassword, Shared.Port);
            if (string.IsNullOrEmpty(XAPIkey))
            {
                WriteMessage(" 認証エラー KabuStationに接続出来ませんでした。");
                error = false;
            }
            else
            {
                Shared.XAPIkey = XAPIkey;
                SymbolRequest symbol = new SymbolRequest(XAPIkey);
                SymbolNameResultEntity symbolEntity = symbol.Request();
                Symbol = symbolEntity.Symbol;
                SymbolName = symbolEntity.SymbolName;
                Reguster();
                //PositionAuto　Csv fileの読み込み（初期化）
                PositionAuto.CsvRead();
                OrderManager.CsvRead();
                Orders();
                Positions();
                WebClient();
                InquiryOrder();
                TcpClientStart();
            }
            Thread.Sleep(1000);
            StrategyManeger.CsvRead();
            StrategyManeger.AddList(this.StrategyViews);
            
            if (TcpConnected == false)
            {
                WriteMessage(" TradingView webHookに接続出来ませんでした。");
                error = false;   
            }
            /*
            else
            {
                WriteMessage("TradingView Webhook 接続 ");
            }*/
            if(error == false)
            {
                WriteMessage("正常に初期化されませんでした。");
                if (TcpConnected == false)
                {
                    WriteMessage("Webhookサーバーは起動していません。");
                }
                return;
            }
            WriteMessage("正常に初期化され起動しました。");
        }

        /// <summary>
        /// WebClient　起動
        /// </summary>
        public void WebClient()
        {
            WriteMessage("WebClient　起動 ");
            client.StartWebSocket();
        }

        /// <summary>
        /// TcpClient　実行スタート
        /// </summary>
        private async void TcpClientStart()
        {

            //非同期でTcpClientを実行する
            await Task.Run(() =>  tClient.StartClient(host, port));
        }

        /// <summary>
        /// 照会 BackGrand start
        /// </summary>
        private void InquiryOrder()
        {
            InquiryTimer _inquityOrder = new InquiryTimer();
            _inquityOrder.InquiryOrderEventHandler += InquiryOrderEventHandler;
            _inquityOrder.OutquiryStatEventHandler += _inquityOrder_OutquiryStatEventHandler;
            _inquityOrder.Start();

        }

        /// <summary>
        /// WebClient Connect event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_CnnectedEventHndler(object sender, EventArgs e)
        {
            ClientConnected = true;
            WriteMessage("KabuStation Push配信を起動しました。");
        }

        /// <summary>
        /// TcpClient DisConnect event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_DisconnectedEventHndler(object sender, EventArgs e)
        {
            ClientConnected = false;
            WriteMessage("KabuStation Push配信は停止しました。");
        }

        /// <summary>
        /// WebClient Recive Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Client_OnReciveData(object sender, WebSocketEntity args)
        {
            try
            {
                Dispatcher.InvokeAsync(new Action(() =>
                {
                    CurrentPrice = Convert.ToString(args.CurrentPrice);
                    AskPrice = Convert.ToString(args.AskPrice);
                    BidPrice = Convert.ToString(args.BidPrice);
                    Shared.BidPrice = args.BidPrice;
                    Shared.AskPrice = args.AskPrice;
                    ProfitCalculate(args.CurrentPrice);
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine("エラーメッセージ{0] Source {1} {2}", e.Message, e.StackTrace);
            }
        }

        /// <summary>
        /// 銘柄登録
        /// </summary>
        private void Reguster()
        {
            //symbolはn225min,Exchange:日通し
            var obj = new
            {
                Symbols = new[]
                {
                    new { Symbol, Exchange = 2}
                }
            };
            Register_Future.Register(XAPIkey, obj);
        }

        /// <summary>
        /// 注文約定照会
        /// </summary>
        private void Orders()
        {
            OrderEntity entity;
            entity = new OrderEntity()
            {
                Product = "3",
                Symbol = Symbol
            };

            string ret = Orders_Future.Orders(Shared.XAPIkey, entity);

            List<OrderListEntity> listEntity = OrderResult.GetOrderListEntity(ret);

            OrderManager.AddList( listEntity);
        }

        /// <summary>
        /// Position残高照会
        /// </summary>
        private void Positions()
        {

            string product = "3"; //先物指定
            string ret = Positions_Future.Positions(Shared.XAPIkey, product, Symbol);
            List<PositionListEntity> rsultList = PositionsResult.PositionResultCheck(ret);
            if (rsultList != null)
            {
                //Bainding Listにpositionデータ追加
                PositionManager.AddList(rsultList);
            }
        }

        /// <summary>
        /// /注文データフィールド作成
        /// </summary>
        /// <param name="selectedOrder"></param>
        /// <param name="tradeType"></param>
        /// <param name="side"></param>
        /// <param name="excutionId"></param>
        public void EntryOrder(int selectedOrder, int tradeType, string side, string excutionId = "")
        {
            int timeInForce = GetTimeInForce(selectedOrder,
                                SelectedTimeInForce1, SelectedTimeInForce2);

            InputOrder fileld = new InputOrder(
                    TradeMode, Strategy, Interval, selectedOrder, tradeType, side, Symbol,
                    timeInForce, Qty, Convert.ToDouble(Price), Convert.ToDouble(StopPrice), excutionId);

            SendOrder(fileld);
        }

        /// <summary>
        /// 注文発注送信
        /// </summary>
        /// <param name="dataFiled"></param>
        public void SendOrder(InputOrder dataFiled)
        {
            
                SendOrderEntity entity = CreateSendOrderNew.SendOrderNewInputCheck(dataFiled);

                var order = OrderFactory.Create(dataFiled.SelectedOrder);
                entity = order.CreateOrderFiled(entity);

                OrderListEntity _orderEntiry = SendOrderFuture.SendOrder(entity);
                WriteMessage(
                            "発注しました：　" +
                            _orderEntiry.TradeMode.DisplayValue + " " +
                            _orderEntiry.Strategy + " " +
                            Convert.ToString(_orderEntiry.Interval) + " " +
                            _orderEntiry.CashMargin.DisplayValue + " " +
                            _orderEntiry.Side.DisplayValue + " " +
                            Convert.ToString(_orderEntiry.Price) + " " +
                            Convert.ToString(_orderEntiry.OrderQty) + "枚 " + " " +
                            _orderEntiry.ExecutionID);
                //戻り値　viewEntityをリストに追加しデータバインドする
                OrderManager.Initial(_orderEntiry);
                PositionManager.Initial(_orderEntiry);
            
        }

        /// <summary>
        /// 注文中のオーダーキャンセル
        /// </summary>
        /// <param name="orderID"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void CancelOrder(string orderID)
        {
            SendOrderCancel.OrderCancel(orderID);
            WriteMessage("注文をキャンセルしまいた。");
        }

        /// <summary>
        /// TcpClient DisConnect Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpClient_OnDiscconnected(object sender, EventArgs e)
        {
            TcpConnected = false;
            WriteMessage("Webhook サーバーは切断しました");
        }

        /// <summary>
        /// TcpClient Connect Event
        /// </summary>
        /// <param name="e"></param>
        private void TcpClient_OnConnected(object sender,EventArgs e)
        {
            TcpConnected = true;
            WriteMessage("Webhook サーバーに接続しました"); //Tcpclientはerrorになる
        }

        /// <summary>
        /// 自動注文受信データイベント(TcpClient Recive Data Event)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TcpClient_OnReciveData(object sender, TcpClientModel e)
        {
            int tradeMode = 1;          //自動トレード
            int selectedOrder = 1;  //Best Market Order,
            int timeInForce = TimeInForce.FAS;        //,
            double stopPrice = 0;
            string excutionId = string.Empty;
            string symbol = Symbol;
            string time = e.time;
            string strategy = e.alert_name;
            if (string.IsNullOrEmpty(strategy))
            {
                WriteMessage("Strategy名が設定されていません:TradingViewに設定してください。");
            }
            int interval = Convert.ToInt32(e.interval);
            string action = e.strategy.order_action;
            double price = e.strategy.order_price;
            double qty = e.strategy.order_contracts;
            string marketposition = e.strategy.market_position;
            string prevmarketposition = e.strategy.prev_market_position;
            double marketposition_size = e.strategy.market_position_size;
            double prevmarketposition_size = e.strategy.prev_market_position_size;

            InputOrder fileld = new InputOrder()
            {
                TradeMode = tradeMode,
                Strategy = strategy,
                Interval = interval,
                SelectedOrder = selectedOrder,
                Symbol = symbol,
                TimeInForce = timeInForce,
                Price = price,
                StopPrice = stopPrice,
                ExecutionId = excutionId
            };

            string side;
            int tradeType;

            if (marketposition == "long" && prevmarketposition == "long" && action == "sell")
            {
                side = Side.Buy;
                tradeType = TradeType.ExitOrder;
                qty = e.strategy.order_contracts;
                fileld.Side = side;
                fileld.TradeType = tradeType;
                fileld.Qty = qty;

                SendOrderAuto(fileld);
            }
            else if (marketposition == "short" && prevmarketposition == "short" && action == "buy")
            {
                // short position 返済
                side = Side.Sell;
                tradeType = TradeType.ExitOrder;
                qty = e.strategy.order_contracts;
                fileld.Side = side;
                fileld.TradeType = tradeType;
                fileld.Qty = qty;

                SendOrderAuto(fileld);
            }
            else if (marketposition == "flat" && prevmarketposition == "long" && action == "sell")
            {
                // long position 返済
                // 返済ポジションがデータフレーム有るかチェックする
                side = Side.Buy;
                tradeType = TradeType.ExitOrder;
                qty = e.strategy.order_contracts;
                fileld.Side = side;
                fileld.TradeType = tradeType;
                fileld.Qty = qty;

                SendOrderAuto(fileld);
            }
            else if (marketposition == "flat" && prevmarketposition == "short" && action == "buy")
            {
                // short position 返済
                side = Side.Sell;
                tradeType = TradeType.ExitOrder;
                qty = e.strategy.order_contracts;
                fileld.Side = side;
                fileld.TradeType = tradeType;
                fileld.Qty = qty;

                SendOrderAuto(fileld);
            }
            else if (marketposition == "long" && prevmarketposition == "flat" && action == "buy")
            {
                // 新規　買い 成行買い発注
                side = Side.Buy;
                tradeType = TradeType.NewOrder;
                qty = e.strategy.order_contracts;
                fileld.Side = side;
                fileld.TradeType = tradeType;
                fileld.Qty = qty;

                SendOrderAuto(fileld);
            }
            else if (marketposition == "short" && prevmarketposition == "flat" && action == "sell")
            {
                //# 新規　売り
                side = Side.Sell;
                tradeType = TradeType.NewOrder;
                qty = e.strategy.order_contracts;
                fileld.Side = side;
                fileld.TradeType = tradeType;
                fileld.Qty = qty;

                SendOrderAuto(fileld);
            }
            else if (marketposition == "long" && prevmarketposition == "short" && action == "buy")
            {
                // 買いドテン
                // ドテン売買の場合[order_contracts]は返済　枚数+買い　枚数となる
                // 1.short position 決済(買戻し)
                side = Side.Sell;
                tradeType = TradeType.ExitOrder;
                qty = prevmarketposition_size;
                fileld.Side = side;
                fileld.TradeType = tradeType;
                fileld.Qty = qty;

                SendOrderAuto(fileld);

                side = Side.Buy;
                tradeType = TradeType.NewOrder;
                qty = marketposition_size;
                fileld.Side = side;
                fileld.TradeType = tradeType;
                fileld.Qty = qty;

                SendOrderAuto(fileld);
            }
            else if (marketposition == "short" && prevmarketposition == "long" && action == "sell")
            {
                // 売りドテン
                side = Side.Buy;
                tradeType = TradeType.ExitOrder;
                qty = prevmarketposition_size;
                fileld.Side = side;
                fileld.TradeType = tradeType;
                fileld.Qty = qty;

                SendOrderAuto(fileld);

                // 2.新規　売り
                side = Side.Sell;
                tradeType = TradeType.NewOrder;
                qty = marketposition_size;
                fileld.Side = side;
                fileld.TradeType = tradeType;
                fileld.Qty = qty;

                SendOrderAuto(fileld);
            }
        }

        /// <summary>
        /// 自動注文発注
        /// </summary>
        /// <param name="fileld"></param>
        private void SendOrderAuto(InputOrder fileld)
        {
            string ms = "売買シグナルを受信しました: " + fileld.Strategy + " " + Convert.ToString(fileld.Interval) + " " +
                                new CashMargin(fileld.TradeType + 1).DisplayValue + " " +
                                    new Domain.ValueObjects.Side(fileld.Side).DisplayValue + " " + Convert.ToString(fileld.Price);
            WriteMessage(ms);
            

            //form Strategy gridviewにトレードサインを表示する
            bool found = StrategyManeger.UpDate(StrategyViews,fileld);
            if (found == false)
            {
                WriteMessage("Strategy Listには登録されていません。");
            }
             
            if (AutoButoon == false)
            {
                return;
            }
            if (StrategyManeger.IsTrade(fileld.Strategy, fileld.Interval) == false)
            {
                WriteMessage("Strategyはチェックなし又は登録されていません。");
                return;
            }
            //返済の場合ExecutionIdを取得する
            List<string> listid;
            string executionId = String.Empty;
            if (fileld.TradeType == TradeType.ExitOrder)
            {
                listid = PositionManager.GetExecutionId(fileld.Strategy,
                                                            fileld.Interval, fileld.Side);

                if (listid.Count == 0)
                {
                    WriteMessage("返済するポジションはありません。");
                    return;
                }
                foreach (string id in listid)
                {
                    
                    fileld.ExecutionId = id;
                    
                    SendOrder(fileld);
                  
                }
            }
            else
            {
                
                SendOrder(fileld);
                 
            }
           
        }


        /// <summary>
        /// 損益計算
        /// </summary>
        /// <param name="currentPrice"></param>
        private void ProfitCalculate(double currentPrice)
        {
            if (currentPrice == 0)
            {
                return;
            }
            int rowCount = PositionList.Count;
            double qty;
            string side;
            double price;
            double profitPrice;
            for (int i = 0; i < rowCount; i++)
            {
                var entity = PositionList.ElementAt(i);
                qty = entity.LeaveQty;
                price = Convert.ToDouble(entity.Price);
                side = entity.Side;

                if (side == "買")
                {
                    profitPrice = (currentPrice - price) * qty * 100;
                }
                else
                {
                    profitPrice = (price - currentPrice) * qty * 100;
                }
                PositionList.ElementAt(i).Profit = profitPrice;
            }
        }

        /// <summary>
        /// 有効期限選択
        /// </summary>
        public int GetTimeInForce(int selectedOrder, int timeforce1, int timeForce2)
        {
            if (selectedOrder == 0)
            {
                return Convert.ToInt32(timeforce1);
            }
            if (selectedOrder == 1)
            {
                return Convert.ToInt32(timeForce2);
            }
            if (selectedOrder == 2)
            {
                return Convert.ToInt32(timeForce2);
            }
            if (selectedOrder == 3)
            {
                return Convert.ToInt32(timeforce1);
            }
            return 999;
        }

        /// <summary>
        /// 約定照会中 State Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void _inquityOrder_OutquiryStatEventHandler(object sender, bool e)
        {
            InquityOrderState = e;
        }

        /// <summary>
        /// 注文約定照会検索イベントハンドラー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void InquiryOrderEventHandler(object sender, OrdersResultModel e)
        {
            //double型のプロパティはobject型に変更した。price.qty　他
            //型変換して使う Comnver.toInt32(price)
            string _executionID;
            OrdersResultModel _ordersResul = e;
            string _orderID = e.ID;

            int count = e.Details.Count;
            double _price = Convert.ToDouble(e.Price);
            int _orderState = e.OrderState;
            int _reciveType = e.Details[count - 1].RecType;
            int _cashMargin = e.CashMargin;
            if (_cashMargin == 2)
            {
                _executionID = e.Details[count - 1].ExecutionID;
                if (_reciveType == 8)
                {
                    _price = Convert.ToDouble(e.Details[count - 1].Price);
                }
            }
            else
            {
                _executionID = OrderManager.GetExecutionId(_orderID);
                if (_reciveType == 8)
                {
                    _price = Convert.ToDouble(e.Details[count - 1].Price);
                }
            }

            //updataデータ作成
            OrederManagerEntity _orderEntity = new OrederManagerEntity()
            {
                Id = _orderID,
                ReciveType = _reciveType,
                OrderQty = Convert.ToDouble(e.OrderQty),
                CumQty = Convert.ToDouble(e.CumQty),
                Price = _price
            };

            PositionManagerEntity _positionEntity = new PositionManagerEntity()
            {
                OrderID = _orderID,
                ExecutionID = _executionID,
                OrderState = _orderState,
                ReciveType = _reciveType,
                CashMargin = _cashMargin,
                OrderQty = Convert.ToDouble(e.OrderQty),
                CumQty = Convert.ToDouble(e.CumQty),
                Price = _price
            };

            if (_reciveType == 3)
            {
                OrderManager.Xepired(_orderEntity);
                PositionManager.Xepired(_positionEntity);
                OrderMessage(_positionEntity);
            }
            if (_reciveType == 4)
            {
                OrderManager.Order(_orderEntity);
            }
            if (_reciveType == 6 || _reciveType == 7)
            {
                OrderManager.Cancel(_orderEntity);
                PositionManager.Cancel(_positionEntity);
                OrderMessage(_positionEntity);
            }
            if (_reciveType == 8 && _orderState == 3)
            {
                OrderManager.Contrct(_orderEntity);
                PositionManager.Contrct(_ordersResul, _positionEntity);
                OrderMessage(_positionEntity);
            }


            if (_reciveType == 8 && _orderState == 5)
            {
                OrderManager.ContrctAll(_orderEntity);
                PositionManager.ContrctAll(_positionEntity);
                OrderMessage(_positionEntity);
            }
        }

        /// <summary>
        /// PasswordをPropertiesにSetting
        /// </summary>
        public void SettingPassword()
        {
            //Username.Text = Properties.Settings.Default.Username;
            SecureString password = Auth.DecryptString(Properties.Settings.Default.Password);
            Shared.Password = Auth.ToInsecureString(password);
            SecureString apipassword = Auth.DecryptString(Properties.Settings.Default.APIPassword);
            Shared.APIPassword = Auth.ToInsecureString(apipassword);

        }

        /// <summary>
        /// 約定メッセージ
        /// </summary>
        /// <param name="entity"></param>
        private void OrderMessage(PositionManagerEntity entity)
        {
            string cashMargin = new CashMargin(entity.CashMargin).DisplayValue;
            string reciveType = new ReciveType(entity.ReciveType).DisplayValue;
            WriteMessage(cashMargin + "発注は" + reciveType + "しました。");
        }

        /// <summary>
        /// メッセージ
        /// </summary>
        /// <param name="message"></param>
        private void WriteMessage(string message)
        {
            //Message += message + " \r\n";
            MessageEventHandler(this, message + "\r\n");
        }
    }

}


