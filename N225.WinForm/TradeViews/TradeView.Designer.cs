namespace N225.WinForm.Views
{
    partial class TradeView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradeView));
            this.MarketRadioButton = new System.Windows.Forms.RadioButton();
            this.BestMarketRadioButton = new System.Windows.Forms.RadioButton();
            this.LimitRadioButton = new System.Windows.Forms.RadioButton();
            this.StopRadioButton = new System.Windows.Forms.RadioButton();
            this.LongOrderButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.QtyNumUpDopwn = new System.Windows.Forms.NumericUpDown();
            this.LimitPriceNumUpDopwn = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.SymblLabel = new System.Windows.Forms.Label();
            this.ShortOrderButoon = new System.Windows.Forms.Button();
            this.CancelOrderButton = new System.Windows.Forms.Button();
            this.ExitOrderButton = new System.Windows.Forms.Button();
            this.StopPriceNumUpDopwn = new System.Windows.Forms.NumericUpDown();
            this.PostionDataGrid = new System.Windows.Forms.DataGridView();
            this.OrderDataGrid = new System.Windows.Forms.DataGridView();
            this.StrategyGridView = new System.Windows.Forms.DataGridView();
            this.TimeInForceCombBox2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CurrentPricrLabel = new System.Windows.Forms.Label();
            this.BidPriceLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.AskPriceLabel = new System.Windows.Forms.Label();
            this.TimeInForceCombBox1 = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.初期設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoBbutton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.StateLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.QtyNumUpDopwn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LimitPriceNumUpDopwn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StopPriceNumUpDopwn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostionDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StrategyGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // MarketRadioButton
            // 
            this.MarketRadioButton.AutoSize = true;
            this.MarketRadioButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MarketRadioButton.Location = new System.Drawing.Point(134, 121);
            this.MarketRadioButton.Name = "MarketRadioButton";
            this.MarketRadioButton.Size = new System.Drawing.Size(70, 24);
            this.MarketRadioButton.TabIndex = 0;
            this.MarketRadioButton.Text = "成行";
            this.MarketRadioButton.UseVisualStyleBackColor = true;
            this.MarketRadioButton.Click += new System.EventHandler(this.MarketRadioButton_Click);
            // 
            // BestMarketRadioButton
            // 
            this.BestMarketRadioButton.AutoSize = true;
            this.BestMarketRadioButton.Checked = true;
            this.BestMarketRadioButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BestMarketRadioButton.Location = new System.Drawing.Point(39, 120);
            this.BestMarketRadioButton.Name = "BestMarketRadioButton";
            this.BestMarketRadioButton.Size = new System.Drawing.Size(70, 24);
            this.BestMarketRadioButton.TabIndex = 1;
            this.BestMarketRadioButton.TabStop = true;
            this.BestMarketRadioButton.Text = "対当";
            this.BestMarketRadioButton.UseVisualStyleBackColor = true;
            this.BestMarketRadioButton.Click += new System.EventHandler(this.BestMarketRadioButton_Click);
            // 
            // LimitRadioButton
            // 
            this.LimitRadioButton.AutoSize = true;
            this.LimitRadioButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LimitRadioButton.Location = new System.Drawing.Point(39, 160);
            this.LimitRadioButton.Name = "LimitRadioButton";
            this.LimitRadioButton.Size = new System.Drawing.Size(70, 24);
            this.LimitRadioButton.TabIndex = 2;
            this.LimitRadioButton.Text = "指値";
            this.LimitRadioButton.UseVisualStyleBackColor = true;
            this.LimitRadioButton.Click += new System.EventHandler(this.LimitRadioButton_Click);
            // 
            // StopRadioButton
            // 
            this.StopRadioButton.AutoSize = true;
            this.StopRadioButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.StopRadioButton.Location = new System.Drawing.Point(39, 205);
            this.StopRadioButton.Name = "StopRadioButton";
            this.StopRadioButton.Size = new System.Drawing.Size(90, 24);
            this.StopRadioButton.TabIndex = 3;
            this.StopRadioButton.Text = "逆指値";
            this.StopRadioButton.UseVisualStyleBackColor = true;
            this.StopRadioButton.Click += new System.EventHandler(this.StopRadioButton_Click);
            // 
            // LongOrderButton
            // 
            this.LongOrderButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LongOrderButton.Location = new System.Drawing.Point(48, 249);
            this.LongOrderButton.Name = "LongOrderButton";
            this.LongOrderButton.Size = new System.Drawing.Size(102, 36);
            this.LongOrderButton.TabIndex = 4;
            this.LongOrderButton.Text = "買注文";
            this.LongOrderButton.UseVisualStyleBackColor = true;
            this.LongOrderButton.Click += new System.EventHandler(this.LongOrderButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(36, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "銘　柄";
            // 
            // QtyNumUpDopwn
            // 
            this.QtyNumUpDopwn.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.QtyNumUpDopwn.Location = new System.Drawing.Point(164, 80);
            this.QtyNumUpDopwn.Name = "QtyNumUpDopwn";
            this.QtyNumUpDopwn.Size = new System.Drawing.Size(127, 27);
            this.QtyNumUpDopwn.TabIndex = 8;
            this.QtyNumUpDopwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.QtyNumUpDopwn.ValueChanged += new System.EventHandler(this.QtyNumUpDopwn_ValueChanged);
            // 
            // LimitPriceNumUpDopwn
            // 
            this.LimitPriceNumUpDopwn.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LimitPriceNumUpDopwn.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.LimitPriceNumUpDopwn.Location = new System.Drawing.Point(164, 158);
            this.LimitPriceNumUpDopwn.Name = "LimitPriceNumUpDopwn";
            this.LimitPriceNumUpDopwn.Size = new System.Drawing.Size(127, 27);
            this.LimitPriceNumUpDopwn.TabIndex = 9;
            this.LimitPriceNumUpDopwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.LimitPriceNumUpDopwn.ValueChanged += new System.EventHandler(this.LimitPriceNumUpDopwn_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(36, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "数　量";
            // 
            // SymblLabel
            // 
            this.SymblLabel.AutoSize = true;
            this.SymblLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SymblLabel.Location = new System.Drawing.Point(130, 47);
            this.SymblLabel.Name = "SymblLabel";
            this.SymblLabel.Size = new System.Drawing.Size(105, 20);
            this.SymblLabel.TabIndex = 12;
            this.SymblLabel.Text = "SymblLabel";
            // 
            // ShortOrderButoon
            // 
            this.ShortOrderButoon.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ShortOrderButoon.Location = new System.Drawing.Point(184, 249);
            this.ShortOrderButoon.Name = "ShortOrderButoon";
            this.ShortOrderButoon.Size = new System.Drawing.Size(102, 35);
            this.ShortOrderButoon.TabIndex = 13;
            this.ShortOrderButoon.Text = "売注文";
            this.ShortOrderButoon.UseVisualStyleBackColor = true;
            this.ShortOrderButoon.Click += new System.EventHandler(this.ShortOrderButoon_Click);
            // 
            // CancelOrderButton
            // 
            this.CancelOrderButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CancelOrderButton.Location = new System.Drawing.Point(458, 249);
            this.CancelOrderButton.Name = "CancelOrderButton";
            this.CancelOrderButton.Size = new System.Drawing.Size(102, 36);
            this.CancelOrderButton.TabIndex = 14;
            this.CancelOrderButton.Text = "キャンセル";
            this.CancelOrderButton.UseVisualStyleBackColor = true;
            this.CancelOrderButton.Click += new System.EventHandler(this.CancelOrderButton_Click);
            // 
            // ExitOrderButton
            // 
            this.ExitOrderButton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ExitOrderButton.Location = new System.Drawing.Point(319, 249);
            this.ExitOrderButton.Name = "ExitOrderButton";
            this.ExitOrderButton.Size = new System.Drawing.Size(102, 35);
            this.ExitOrderButton.TabIndex = 15;
            this.ExitOrderButton.Text = "返　済";
            this.ExitOrderButton.UseVisualStyleBackColor = true;
            this.ExitOrderButton.Click += new System.EventHandler(this.ExitOrderButton_Click);
            // 
            // StopPriceNumUpDopwn
            // 
            this.StopPriceNumUpDopwn.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.StopPriceNumUpDopwn.Location = new System.Drawing.Point(164, 203);
            this.StopPriceNumUpDopwn.Name = "StopPriceNumUpDopwn";
            this.StopPriceNumUpDopwn.Size = new System.Drawing.Size(127, 27);
            this.StopPriceNumUpDopwn.TabIndex = 16;
            this.StopPriceNumUpDopwn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.StopPriceNumUpDopwn.ValueChanged += new System.EventHandler(this.StopPriceNumUpDopwn_ValueChanged);
            // 
            // PostionDataGrid
            // 
            this.PostionDataGrid.AllowUserToAddRows = false;
            this.PostionDataGrid.AllowUserToDeleteRows = false;
            this.PostionDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PostionDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PostionDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.PostionDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PostionDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.PostionDataGrid.Location = new System.Drawing.Point(12, 458);
            this.PostionDataGrid.Name = "PostionDataGrid";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PostionDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.PostionDataGrid.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PostionDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.PostionDataGrid.RowTemplate.Height = 24;
            this.PostionDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PostionDataGrid.Size = new System.Drawing.Size(962, 164);
            this.PostionDataGrid.TabIndex = 17;
            this.PostionDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PostionDataGrid_CellClick);
            this.PostionDataGrid.BindingContextChanged += new System.EventHandler(this.PostionDataGrid_BindingContextChanged);
            // 
            // OrderDataGrid
            // 
            this.OrderDataGrid.AllowUserToAddRows = false;
            this.OrderDataGrid.AllowUserToDeleteRows = false;
            this.OrderDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrderDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OrderDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.OrderDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.OrderDataGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.OrderDataGrid.Location = new System.Drawing.Point(12, 302);
            this.OrderDataGrid.Name = "OrderDataGrid";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OrderDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.OrderDataGrid.RowHeadersWidth = 51;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OrderDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.OrderDataGrid.RowTemplate.Height = 24;
            this.OrderDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OrderDataGrid.Size = new System.Drawing.Size(962, 150);
            this.OrderDataGrid.TabIndex = 19;
            this.OrderDataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OrderDataGrid_CellClick);
            this.OrderDataGrid.BindingContextChanged += new System.EventHandler(this.OrderDataGrid_BindingContextChanged);
            // 
            // StrategyGridView
            // 
            this.StrategyGridView.AllowUserToAddRows = false;
            this.StrategyGridView.AllowUserToDeleteRows = false;
            this.StrategyGridView.AllowUserToResizeColumns = false;
            this.StrategyGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StrategyGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.StrategyGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.StrategyGridView.ColumnHeadersHeight = 29;
            this.StrategyGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.StrategyGridView.DefaultCellStyle = dataGridViewCellStyle10;
            this.StrategyGridView.Location = new System.Drawing.Point(461, 81);
            this.StrategyGridView.Name = "StrategyGridView";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.StrategyGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.StrategyGridView.RowHeadersWidth = 51;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.StrategyGridView.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.StrategyGridView.RowTemplate.Height = 24;
            this.StrategyGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.StrategyGridView.Size = new System.Drawing.Size(516, 150);
            this.StrategyGridView.TabIndex = 20;
            this.StrategyGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StrategyGridView_CellContentClick);
            this.StrategyGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.StrategyGridView_CellValueChanged);
            this.StrategyGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.StrategyGridView_RowsAdded);
            // 
            // TimeInForceCombBox2
            // 
            this.TimeInForceCombBox2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TimeInForceCombBox2.FormattingEnabled = true;
            this.TimeInForceCombBox2.Location = new System.Drawing.Point(325, 159);
            this.TimeInForceCombBox2.Name = "TimeInForceCombBox2";
            this.TimeInForceCombBox2.Size = new System.Drawing.Size(95, 28);
            this.TimeInForceCombBox2.TabIndex = 31;
            this.TimeInForceCombBox2.SelectedIndexChanged += new System.EventHandler(this.TimeInForceCombBox2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(662, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 20);
            this.label5.TabIndex = 28;
            this.label5.Text = "BID:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(494, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 29;
            this.label6.Text = "現在値";
            // 
            // CurrentPricrLabel
            // 
            this.CurrentPricrLabel.AutoSize = true;
            this.CurrentPricrLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CurrentPricrLabel.Location = new System.Drawing.Point(569, 45);
            this.CurrentPricrLabel.Name = "CurrentPricrLabel";
            this.CurrentPricrLabel.Size = new System.Drawing.Size(158, 20);
            this.CurrentPricrLabel.TabIndex = 33;
            this.CurrentPricrLabel.Text = "CurrentPricrLabel";
            // 
            // BidPriceLabel
            // 
            this.BidPriceLabel.AutoSize = true;
            this.BidPriceLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BidPriceLabel.Location = new System.Drawing.Point(712, 45);
            this.BidPriceLabel.Name = "BidPriceLabel";
            this.BidPriceLabel.Size = new System.Drawing.Size(124, 20);
            this.BidPriceLabel.TabIndex = 34;
            this.BidPriceLabel.Text = "BidPriceLabel";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(799, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 20);
            this.label7.TabIndex = 35;
            this.label7.Text = "ASK";
            // 
            // AskPriceLabel
            // 
            this.AskPriceLabel.AutoSize = true;
            this.AskPriceLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AskPriceLabel.Location = new System.Drawing.Point(851, 45);
            this.AskPriceLabel.Name = "AskPriceLabel";
            this.AskPriceLabel.Size = new System.Drawing.Size(128, 20);
            this.AskPriceLabel.TabIndex = 36;
            this.AskPriceLabel.Text = "AskPriceLabel";
            // 
            // TimeInForceCombBox1
            // 
            this.TimeInForceCombBox1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TimeInForceCombBox1.FormattingEnabled = true;
            this.TimeInForceCombBox1.Location = new System.Drawing.Point(325, 118);
            this.TimeInForceCombBox1.Name = "TimeInForceCombBox1";
            this.TimeInForceCombBox1.Size = new System.Drawing.Size(95, 28);
            this.TimeInForceCombBox1.TabIndex = 37;
            this.TimeInForceCombBox1.SelectedIndexChanged += new System.EventHandler(this.TimeInForceCombBox1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.初期設定ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(986, 33);
            this.menuStrip1.TabIndex = 40;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 初期設定ToolStripMenuItem
            // 
            this.初期設定ToolStripMenuItem.Font = new System.Drawing.Font("Yu Gothic UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.初期設定ToolStripMenuItem.Name = "初期設定ToolStripMenuItem";
            this.初期設定ToolStripMenuItem.Size = new System.Drawing.Size(98, 29);
            this.初期設定ToolStripMenuItem.Text = "初期設定";
            this.初期設定ToolStripMenuItem.Click += new System.EventHandler(this.初期設定ToolStripMenuItem_Click);
            // 
            // AutoBbutton
            // 
            this.AutoBbutton.BackColor = System.Drawing.Color.Gainsboro;
            this.AutoBbutton.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.AutoBbutton.Location = new System.Drawing.Point(772, 248);
            this.AutoBbutton.Name = "AutoBbutton";
            this.AutoBbutton.Size = new System.Drawing.Size(115, 36);
            this.AutoBbutton.TabIndex = 41;
            this.AutoBbutton.Text = "AUTO OFF";
            this.AutoBbutton.UseVisualStyleBackColor = false;
            this.AutoBbutton.Click += new System.EventHandler(this.AutoBbutton_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 628);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(961, 83);
            this.richTextBox1.TabIndex = 42;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(641, 249);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 36);
            this.button1.TabIndex = 43;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::N225.WinForm.Properties.Resources.red;
            this.pictureBox1.Location = new System.Drawing.Point(337, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(15, 14);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 44;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::N225.WinForm.Properties.Resources.maru;
            this.pictureBox2.Location = new System.Drawing.Point(375, 47);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(15, 14);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 45;
            this.pictureBox2.TabStop = false;
            // 
            // StateLabel
            // 
            this.StateLabel.AutoSize = true;
            this.StateLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.StateLabel.Location = new System.Drawing.Point(411, 45);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(69, 20);
            this.StateLabel.TabIndex = 46;
            this.StateLabel.Text = "照会中";
            // 
            // TradeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 723);
            this.Controls.Add(this.StateLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.AutoBbutton);
            this.Controls.Add(this.TimeInForceCombBox1);
            this.Controls.Add(this.AskPriceLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.BidPriceLabel);
            this.Controls.Add(this.CurrentPricrLabel);
            this.Controls.Add(this.TimeInForceCombBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.StrategyGridView);
            this.Controls.Add(this.OrderDataGrid);
            this.Controls.Add(this.PostionDataGrid);
            this.Controls.Add(this.StopPriceNumUpDopwn);
            this.Controls.Add(this.ExitOrderButton);
            this.Controls.Add(this.CancelOrderButton);
            this.Controls.Add(this.ShortOrderButoon);
            this.Controls.Add(this.SymblLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LimitPriceNumUpDopwn);
            this.Controls.Add(this.QtyNumUpDopwn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LongOrderButton);
            this.Controls.Add(this.StopRadioButton);
            this.Controls.Add(this.LimitRadioButton);
            this.Controls.Add(this.MarketRadioButton);
            this.Controls.Add(this.BestMarketRadioButton);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TradeView";
            this.Text = "N225Trader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TradeView_FormClosing);
            this.Load += new System.EventHandler(this.TradeView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.QtyNumUpDopwn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LimitPriceNumUpDopwn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StopPriceNumUpDopwn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostionDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StrategyGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton StopRadioButton;
        private System.Windows.Forms.RadioButton LimitRadioButton;
        private System.Windows.Forms.RadioButton BestMarketRadioButton;
        private System.Windows.Forms.RadioButton MarketRadioButton;
        private System.Windows.Forms.Button LongOrderButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown QtyNumUpDopwn;
        private System.Windows.Forms.NumericUpDown LimitPriceNumUpDopwn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label SymblLabel;
        private System.Windows.Forms.Button ShortOrderButoon;
        private System.Windows.Forms.Button CancelOrderButton;
        private System.Windows.Forms.Button ExitOrderButton;
        private System.Windows.Forms.NumericUpDown StopPriceNumUpDopwn;
        private System.Windows.Forms.DataGridView PostionDataGrid;
        private System.Windows.Forms.DataGridView OrderDataGrid;
        private System.Windows.Forms.DataGridView StrategyGridView;
        private System.Windows.Forms.ComboBox TimeInForceCombBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label CurrentPricrLabel;
        private System.Windows.Forms.Label BidPriceLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label AskPriceLabel;
        private System.Windows.Forms.ComboBox TimeInForceCombBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 初期設定ToolStripMenuItem;
        private System.Windows.Forms.Button AutoBbutton;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label StateLabel;
    }
}