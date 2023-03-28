using N225.Domain;
using N225.Domain.CommonConst;
using N225.Domain.Entities;
using N225.Domain.Exceptions;
using N225.Domain.StaticVlues;
using N225.WinForm.Modules;
using N225.WinForm.TradeViewModels;
using N225.WinForm.StrategyViews;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Threading;

namespace N225.WinForm.Views
{
    public partial class TradeView : Form
    {

        TradeViewModel viewModel;
        DataGridViewCheckBoxColumn column;
        bool _checkBoxEventEnable = true;
        bool blink = false;

        Dispatcher _thisDispatcher;

        private int SelectedOrder = 0;
        private string executionID;
        private string orderID;
        private decimal qtyHold = 1;
        private decimal limitPriceHold = 10000;
        private decimal stopPriceHold = 10000;

        private bool BindingChanged = true;
        private bool OrderBindingChanged = true;

        private bool SetLimitPriceEnable = true;
        private bool SetStopPriceEnable = true;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TradeView()
        {
            try
            {
                viewModel = new TradeViewModel();
                viewModel.MessageEventHandler += ViewModel_MessageEventHandler;

                InitializeComponent();

                this.AutoBbutton.BackColor = Color.Gainsboro;

                _thisDispatcher = Dispatcher.CurrentDispatcher;

                viewModel.Dispatcher = Dispatcher.CurrentDispatcher;
                PositionManager.Dispatcher = Dispatcher.CurrentDispatcher;
                OrderManager.Dispatcher = Dispatcher.CurrentDispatcher;
                StrategyManeger.Dispatcher = Dispatcher.CurrentDispatcher;
                OrderManager.OrderList = viewModel.OrderList;
                PositionManager.PositionList = viewModel.PositionList;

                this.MarketRadioButton.Checked = true;
                this.QtyNumUpDopwn.Minimum = 1;
                this.LimitPriceNumUpDopwn.Increment = 5;
                this.LimitPriceNumUpDopwn.Maximum = 50000;
                //this.LimitPriceNumUpDopwn.Minimum = 10000;
                this.LimitPriceNumUpDopwn.Visible = false;
                this.StopPriceNumUpDopwn.Increment = 5;
                this.StopPriceNumUpDopwn.Maximum = 50000;
                //this.StopPriceNumUpDopwn.Minimum = 10000;
                this.StopPriceNumUpDopwn.Visible = false;

                //データバインディング
                TimeInForceCombBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                TimeInForceCombBox2.DropDownStyle = ComboBoxStyle.DropDownList;

                TimeInForceCombBox1.FormattingEnabled = true;

                //TimeInForceCombBox1.DataSource = viewModel.TimeInForce1;
                TimeInForceCombBox1.DataBindings.Add(
                    "DataSource", viewModel, nameof(viewModel.TimeInForce1));
                TimeInForceCombBox1.DisplayMember = nameof(TimeInForceEntity.Name);
                TimeInForceCombBox1.ValueMember = nameof(TimeInForceEntity.Condition);
                //TimeInForceCombBox1.DataBindings.Add(
                //    "SelectedValue", viewModel, nameof(viewModel.SelectedTimeInForce1));

                //TimeInForceCombBox2.DataSource = viewModel.TimeInForce2;
                TimeInForceCombBox2.DataBindings.Add(
                    "DataSource", viewModel, nameof(viewModel.TimeInForce2));
                TimeInForceCombBox2.DisplayMember = nameof(TimeInForceEntity.Name);
                TimeInForceCombBox2.ValueMember = nameof(TimeInForceEntity.Condition);
                //TimeInForceCombBox2.DataBindings.Add(
                //    "SelectedValue", viewModel, nameof(viewModel.SelectedTimeInForce2));

                this.CurrentPricrLabel.DataBindings.Add(
                    "Text", viewModel, nameof(viewModel.CurrentPrice));
                this.BidPriceLabel.DataBindings.Add(
                    "Text", viewModel, nameof(viewModel.BidPrice));
                this.AskPriceLabel.DataBindings.Add(
                    "Text", viewModel, nameof(viewModel.AskPrice));

                this.SymblLabel.DataBindings.Add(
                    "Text", viewModel, nameof(viewModel.SymbolName));

                PictureBox pictureBox1 = new PictureBox();
                PictureBox pictureBox2 = new PictureBox();
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox1.Image = Properties.Resources.maru;
                pictureBox2.Image = Properties.Resources.maru;

                Timer timer1 = new Timer();
                timer1.Interval = 1000;
                timer1.Tick += Timer1_Tick;
                timer1.Start();

                //DataGridデータバインディング

                OrderDataGrid.RowHeadersVisible = false;
                OrderDataGrid.AllowUserToAddRows = false;
                OrderDataGrid.AutoGenerateColumns = true;
                OrderDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                OrderDataGrid.ReadOnly = true;
                OrderDataGrid.ColumnHeadersDefaultCellStyle.Alignment =
                                    DataGridViewContentAlignment.MiddleCenter;
                OrderDataGrid.DataSource = viewModel.OrderList;

                PostionDataGrid.RowHeadersVisible = false;
                PostionDataGrid.AllowUserToAddRows = false;
                PostionDataGrid.AutoGenerateColumns = true;
                PostionDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                PostionDataGrid.ReadOnly = true;
                PostionDataGrid.ColumnHeadersDefaultCellStyle.Alignment =
                                    DataGridViewContentAlignment.MiddleCenter;
                PostionDataGrid.DataSource = viewModel.PositionList;
                SetHeaderText();

                StrategyGridView.RowHeadersVisible = false;
                column = new DataGridViewCheckBoxColumn();
                column.Name = "Check";
                column.HeaderText = "AUTO";
                //column.TrueValue = true;
                //column.FalseValue = false;
                StrategyGridView.Columns.Insert(0, column);

                StrategyGridView.DataSource = viewModel.StrategyViews;

                StrategyGridView.Columns["Name"].ReadOnly = true;
                StrategyGridView.Columns["Name"].Width = 100;
                StrategyGridView.Columns["Interval"].ReadOnly = true;
                StrategyGridView.Columns["Interval"].Width = 50;
                StrategyGridView.Columns["DateTime"].ReadOnly = true;
                StrategyGridView.Columns["DateTime"].Width = 100;
                StrategyGridView.Columns["TradeType"].ReadOnly = true;
                StrategyGridView.Columns["Side"].ReadOnly = true;
                StrategyGridView.Columns["Price"].ReadOnly = true;
                StrategyGridView.Columns["Price"].Width = 80;
                StrategyGridView.Columns["Description"].ReadOnly = true;


                viewModel.SettingPassword();

                if (string.IsNullOrEmpty(Shared.Password) || string.IsNullOrEmpty(Shared.APIPassword))
                {
                    StrategyForm subform = new StrategyForm("new");
                    subform.ShowDialog();
                    subform.Dispose();
                }
                
                ////アプリケーション初期化
                viewModel.Initialize();
                //viewModel.TimeInForceBaind1();
                //viewModel.TimeInForceBaind2();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBoxIcon icon = MessageBoxIcon.Error;
                string caption = "エラー";
                var exceptonBase = e as ExceptionBase;
                if (exceptonBase != null)
                {
                    if (exceptonBase.Kind == ExceptionBase.ExceptionKind.Info)
                    {
                        icon = MessageBoxIcon.Information;
                        caption = "情報";
                    }
                    else if (exceptonBase.Kind == ExceptionBase.ExceptionKind.Warning)
                    {
                        icon = MessageBoxIcon.Warning;
                        caption = "警告";
                    }

                }
                //string errorMessage = e.InnerException.InnerException.Message;
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, icon);
            }
        }

        /// <summary>
        /// TcpClient,webClient,照会 State　表示する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            bool state = viewModel.InquityOrderState;
            if (state)
            {
                StateLabel.Visible = true;
            }
            else
            {
                StateLabel.Visible = false;
            }
            if (blink == false)
            {
                pictureBox1.Image = Properties.Resources.maru;
                pictureBox2.Image = Properties.Resources.maru;
                blink = true;
            }
            else
            {
                if (viewModel.ClientConnected == false)
                {
                    pictureBox1.Image = Properties.Resources.red;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.grren;
                }

                if (viewModel.TcpConnected == false)
                {
                    pictureBox2.Image = Properties.Resources.red;
                }
                else
                {
                    pictureBox2.Image = Properties.Resources.grren;
                }
                blink = false;
            }
        }

        /// <summary>
        /// //UIスレッド、アクセスデレゲート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        delegate void UIrichTextWrite(object sender, string e);
        
        /// <summary>
        /// メッセージBoxに表示する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_MessageEventHandler(object sender, string e)
        {
            //非同期でUIスレッドを実行する
            _thisDispatcher.InvokeAsync(new Action(() =>
                    {
                        string message = DateTime.Now.ToString("MM/dd HH:mm:ss: ");
                        message += e.ToString();

                        //先頭行に新しい文字列を追加する
                        richTextBox1.SelectionStart = 0;
                        richTextBox1.SelectedText = message;       //Text文と空白行1行挿入
                    }));

            //同期でUIスレッドを実行する
            //_thisDispatcher.Invoke(delegate ()
            //{
            //    string message = DateTime.Now.ToString("MM/dd HH:mm:ss: ");
            //    message += e.ToString();

            //    //先頭行に新しい文字列を追加する
            //    richTextBox1.SelectionStart = 0;
            //    richTextBox1.SelectedText = message;       //Text文と空白行1行挿入

            //});

            ////スレッド切り替えてUIスレッドで実行する
            //if (richTextBox1.InvokeRequired)
            //{
            //    UIrichTextWrite d = new UIrichTextWrite(ViewModel_MessageEventHandler);
            //    this.Invoke(d, new object[] {this, (object)e.ToString() });
            //}
            //else
            //{
            //    string message = DateTime.Now.ToString("MM/dd HH:mm:ss: ");
            //    message += e.ToString();

            //    //先頭行に新しい文字列を追加する
            //    richTextBox1.SelectionStart = 0;
            //    richTextBox1.SelectedText = message;       //Text文と空白行1行挿入
            //}

        }

        /// <summary>
        /// DataGridヘッダー日本語表記設定
        /// </summary>
        private void SetHeaderText()
        {
            PostionDataGrid.Columns["TradeMode"].HeaderText = "取引";       //中央
            PostionDataGrid.Columns["DateTime"].HeaderText = "約定日";
            PostionDataGrid.Columns["Strategy"].HeaderText = "アラート";
            PostionDataGrid.Columns["Iterval"].HeaderText = "足種別";
            PostionDataGrid.Columns["Side"].HeaderText = "売買";
            PostionDataGrid.Columns["LeaveQty"].HeaderText = "保有";
            PostionDataGrid.Columns["HoldQty"].HeaderText = "注文";
            PostionDataGrid.Columns["Price"].HeaderText = "約定値";      //右詰め
            PostionDataGrid.Columns["Profit"].HeaderText = "評価損益";     //右詰め
            PostionDataGrid.Columns["ExecutionID"].HeaderText = "ExecutionID";
            PostionDataGrid.Columns["Price"].DefaultCellStyle.Alignment =
                                            DataGridViewContentAlignment.MiddleRight;
            PostionDataGrid.Columns["Profit"].DefaultCellStyle.Alignment =
                                    DataGridViewContentAlignment.MiddleRight;
            OrderDataGrid.Columns["TradeMode"].HeaderText = "取引";       //中央
            OrderDataGrid.Columns["RecvTime"].HeaderText = "受注日時";
            OrderDataGrid.Columns["Strategy"].HeaderText = "アラート";
            OrderDataGrid.Columns["Iterval"].HeaderText = "足種別";
            //OrderDataGrid.Columns["CashMargin"].Width = 70;
            OrderDataGrid.Columns["CashMargin"].HeaderText = "取引区分";
            OrderDataGrid.Columns["Side"].HeaderText = "売買";
            OrderDataGrid.Columns["State"].HeaderText = "状態";
            OrderDataGrid.Columns["OrderQty"].HeaderText = "注文";      //右詰め
            OrderDataGrid.Columns["CumQty"].HeaderText = "約定";     //右詰め
            OrderDataGrid.Columns["Price"].HeaderText = "約定価格";
            OrderDataGrid.Columns["OrderID"].HeaderText = "注文ID";
            OrderDataGrid.Columns["ExecutionID"].HeaderText = "約定番号";

            OrderDataGrid.Columns["Price"].DefaultCellStyle.Alignment =
                                    DataGridViewContentAlignment.MiddleRight;
        }

        private void TradeView_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 新規買い注文発注ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LongOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                viewModel.EntryOrder(SelectedOrder, TradeType.NewOrder, Side.Buy);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                string errorMessage;
                if (ex.InnerException == null)
                {
                    errorMessage = ex.Message;
                }
                else
                {
                    errorMessage = ex.InnerException.Message;
                }
                MessageBox.Show(errorMessage);
            }
        }

        /// <summary>
        /// 新規売り注文発注ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShortOrderButoon_Click(object sender, EventArgs e)
        {
            try
            {
                viewModel.EntryOrder(SelectedOrder, TradeType.NewOrder, Side.Sell);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                string errorMessage;
                if (ex.InnerException == null)
                {
                    errorMessage = ex.Message;
                }
                else
                {
                    errorMessage = ex.InnerException.Message;
                }
                MessageBox.Show(errorMessage);
            }
        }

        /// <summary>
        /// 返済発注ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                string side = PositionsCache.GetSide(executionID);
                Console.WriteLine("ExecutionID:{0} Side:{1}", executionID, side);

                viewModel.EntryOrder(SelectedOrder, TradeType.ExitOrder, side, executionID);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                string errorMessage;
                if (ex.InnerException == null)
                {
                    errorMessage = ex.Message;
                }
                else
                {
                    errorMessage = ex.InnerException.Message;
                }
                MessageBox.Show(errorMessage);
            }
        }

        /// <summary>
        /// 取消発注ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                viewModel.CancelOrder(orderID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

                string errorMessage;
                if (ex.InnerException == null)
                {
                    errorMessage = ex.Message;
                }
                else
                {
                    errorMessage = ex.InnerException.Message;
                }
                MessageBox.Show(errorMessage);

            }
        }

        /// <summary>
        /// 取引区分 Market RadioButton 設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarketRadioButton_Click(object sender, EventArgs e)
        {
            SelectedOrder = 0;
            LimitPriceNumUpDopwn.Visible = false;
            StopPriceNumUpDopwn.Visible = false;
            TimeInForceCombBox1.Visible = true;
            TimeInForceCombBox2.Visible = false;
            TimeInForceCombBox1.SelectedIndex = 0;
            viewModel.SelectedTimeInForce1 = 2;
        }

        /// <summary>
        /// 取引区分 BestMarket RadioButton 設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BestMarketRadioButton_Click(object sender, EventArgs e)
        {
            SelectedOrder = 1;
            LimitPriceNumUpDopwn.Visible = false;
            StopPriceNumUpDopwn.Visible = false;
            TimeInForceCombBox1.Visible = false;
            TimeInForceCombBox2.Visible = true;
            TimeInForceCombBox2.SelectedIndex = 0;
            viewModel.SelectedTimeInForce2 = 1;
            SetLimitPriceEnable = true;
        }
 
        /// <summary>
        /// 取引区分 Limit RadioButton 設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LimitRadioButton_Click(object sender, EventArgs e)
        {
            SelectedOrder = 2;
            LimitPriceNumUpDopwn.Visible = true;
            StopPriceNumUpDopwn.Visible = false;
            TimeInForceCombBox1.Visible = false;
            TimeInForceCombBox2.Visible = true;
            TimeInForceCombBox2.SelectedIndex = 0;
            viewModel.SelectedTimeInForce2 = 1;
            limitPriceHold = 0;
            LimitPriceNumUpDopwn.Value = 0;
            viewModel.Price = 0;
            SetLimitPriceEnable = true;
        }

        /// <summary>
        /// 取引区分 Stop RadioButton 設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopRadioButton_Click(object sender, EventArgs e)
        {
            SelectedOrder = 3;
            LimitPriceNumUpDopwn.Visible = false;
            StopPriceNumUpDopwn.Visible = true;
            TimeInForceCombBox1.Visible = true;
            TimeInForceCombBox2.Visible = false;
            TimeInForceCombBox1.SelectedIndex = 0;
            viewModel.SelectedTimeInForce1 = 2;
            stopPriceHold = 0;
            StopPriceNumUpDopwn.Value = 0;
            viewModel.StopPrice = 0;
            SetStopPriceEnable = true;
        }

        /// <summary>
        /// PostionDataがバインドした時1回だけヘッダーを日本語に変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PostionDataGrid_BindingContextChanged(object sender, EventArgs e)
        {
            try
            {
                int count = PostionDataGrid.Columns.Count;
                if (BindingChanged == true && count > 0)
                {

                    if (PostionDataGrid.CurrentRow != null)
                    {
                        executionID = PostionDataGrid.CurrentRow.Cells[9].Value.ToString();
                    }

                    BindingChanged = false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("PostionDataGrid_BindingContextChangedeエラー");
            }
        }

        private void OrderDataGrid_BindingContextChanged(object sender, EventArgs e)
        {
            int count = OrderDataGrid.Columns.Count;
            try
            {
                if (OrderBindingChanged == true && count > 0)
                {
                    OrderDataGrid.Columns["CashMargin"].Width = 50;
                    OrderDataGrid.Columns["Side"].Width = 50;

                    OrderBindingChanged = false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("OrderDataGrid_BindingContextChangedでエラー");
            }
        }

        /// <summary>
        /// OrderDataGridからOrderIDを取得する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                // ヘッダーなら何もしない
            }
            else
            {
                // 選択行を取得する
                int iRow = OrderDataGrid.CurrentCell.RowIndex;

                //選択列を取得する
                int count = OrderDataGrid.CurrentRow.Cells.Count;
                if (count > 0)
                {
                    orderID = OrderDataGrid.CurrentRow.Cells[10].Value.ToString();
                }

                Console.WriteLine("ExecutionID:{0}", orderID);
            }
        }

        /// <summary>
        /// PostionDataGrid選択行からExecutionIDを取得する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PostionDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                // ヘッダーなら何もしない
            }
            else
            {
                // 選択行を取得する
                int iRow = PostionDataGrid.CurrentCell.RowIndex;

                //選択列を取得する
                int count = PostionDataGrid.CurrentRow.Cells.Count;
                if (count > 0)
                {
                    executionID = PostionDataGrid.CurrentRow.Cells[9].Value.ToString();
                }

                Console.WriteLine("ExecutionID:{0}", executionID);
            }
        }

        /// <summary>
        /// NumUpDopwn指値価格をTradeViewModelプロパティPriceに設定する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LimitPriceNumUpDopwn_ValueChanged(object sender, EventArgs e)
        {
            if (SetLimitPriceEnable == true && string.IsNullOrEmpty(viewModel.CurrentPrice) == false)
            {
                LimitPriceNumUpDopwn.Value = Convert.ToDecimal(viewModel.CurrentPrice);

                SetLimitPriceEnable = false;
            }
            if (SetLimitPriceEnable == false)
            {
                if (LimitPriceNumUpDopwn.Value >= 10000)
                {
                    limitPriceHold = LimitPriceNumUpDopwn.Value;
                }
                else
                {
                    LimitPriceNumUpDopwn.Value = limitPriceHold;
                }
            }
            viewModel.Price = Convert.ToDouble(LimitPriceNumUpDopwn.Value);

        }

        /// <summary>
        /// NumUpDopwn逆指値価格をTradeViewModelプロパティStopPriceに設定する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopPriceNumUpDopwn_ValueChanged(object sender, EventArgs e)
        {
            if (SetStopPriceEnable == true && string.IsNullOrEmpty(viewModel.CurrentPrice) == false)
            {
                StopPriceNumUpDopwn.Value = Convert.ToDecimal(viewModel.CurrentPrice);

                SetStopPriceEnable = false;
            }
            if (SetStopPriceEnable == false)
            {
                if (StopPriceNumUpDopwn.Value >= 10000)
                {
                    stopPriceHold = StopPriceNumUpDopwn.Value;
                }
                else
                {
                    StopPriceNumUpDopwn.Value = stopPriceHold;
                }
            }
            viewModel.StopPrice = Convert.ToDouble(StopPriceNumUpDopwn.Value);
        }

        /// <summary>
        /// NumUpDopwn 注文数をTradeViewModelプロパティQtyに設定する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QtyNumUpDopwn_ValueChanged(object sender, EventArgs e)
        {
            if (QtyNumUpDopwn.Value >= 1)
            {
                qtyHold = QtyNumUpDopwn.Value;
            }
            else
            {
                QtyNumUpDopwn.Value = qtyHold;
            }
            viewModel.Qty = Convert.ToDouble(QtyNumUpDopwn.Value);
        }

        /// <summary>
        /// 初期設定画面表示する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 初期設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StrategyForm subform = new StrategyForm();
            subform.ShowDialog();
            subform.Dispose();

            //チェックBoxイベントを停止する
            viewModel.RemoveStrategyViews();
            StrategyManeger.AddList(viewModel.StrategyViews);
        }

        /// <summary>
        /// StrategyGridView　CheckBox　Commit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StrategyGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (StrategyGridView.Columns[e.ColumnIndex].Name == "Check" && _checkBoxEventEnable)
            {
                StrategyGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// StrategyGrid CheckBox check Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StrategyGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (StrategyGridView.Columns[e.ColumnIndex].Name == "Check" && _checkBoxEventEnable)
            {
                bool check = (bool)StrategyGridView.Rows[e.RowIndex].Cells[0].Value;
                string _name = StrategyGridView.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                int interval = Convert.ToInt32(StrategyGridView.Rows[e.RowIndex].Cells["Interval"].Value);
                StrategyListCash.UpdateCheck(check, _name, interval);
                //Console.WriteLine("TradeView UpdateCheck Check:{0} Nam {1}", check,_name);
            }
        }

        /// <summary>
        /// StrategyGrid CheckBox check set
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StrategyGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {

                for (int i = 0; i < StrategyGridView.Rows.Count; i++)
                {
                    string name = StrategyGridView.Rows[i].Cells["Name"].Value.ToString();
                    int interval = Convert.ToInt32(StrategyGridView.Rows[i].Cells["Interval"].Value);
                    bool check = StrategyListCash.GetCheck(name, interval);
                    StrategyGridView.Rows[i].Cells[0].Value = check;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 自動取引設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoBbutton_Click(object sender, EventArgs e)
        {
            if (viewModel.AutoButoon == false)
            {
                viewModel.AutoButoon = true;
                this.AutoBbutton.Text = "AUTO ON";
                this.AutoBbutton.BackColor = Color.Green;
            }
            else
            {
                viewModel.AutoButoon = false;
                this.AutoBbutton.Text = "AUTO OFF";
                this.AutoBbutton.BackColor = Color.Gainsboro;
            }
        }

        /// <summary>
        /// Form Close イベント CSVファイルを保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TradeView_FormClosing(object sender, FormClosingEventArgs e)
        {
            StrategyManeger.SaveToCsv();
        }

        /// <summary>
        /// viewModel プロパティ SelectedTimeInForce1設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeInForceCombBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewModel.SelectedTimeInForce1 = Convert.ToInt32(TimeInForceCombBox1.SelectedValue);
            //Console.WriteLine("index{0} name {1} selectValue {2}" ,TimeInForceCombBox1.SelectedIndex ,
            //    TimeInForceCombBox1.Text, TimeInForceCombBox1.SelectedValue);
        }

        /// <summary>
        /// viewModel プロパティ SelectedTimeInForce２設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeInForceCombBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewModel.SelectedTimeInForce2 = Convert.ToInt32(TimeInForceCombBox2.SelectedValue);
            Console.WriteLine("index{0} name {1} selectValue {2}", TimeInForceCombBox2.SelectedIndex,
                TimeInForceCombBox2.Text, TimeInForceCombBox2.SelectedValue);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
