using N225.Domain.Modules;
using N225.Domain.StaticVlues;
using N225.WinForm.Modules;
using N225.WinForm.Strategys;
using System;
using System.Security;
using System.Windows.Forms;
using System.Windows.Threading;

namespace N225.WinForm.StrategyViews
{
    public partial class StrategyForm : Form
    {
        StrategyModel _view;
        int _rowIndex;
        DataGridViewCheckBoxColumn _column;
        bool _checkBoxEventEnable = true;

        public StrategyForm(string initial = "")
        {
            _view = new StrategyModel();
            _view.Dispatcher = Dispatcher.CurrentDispatcher;

            InitializeComponent();

            this.KeyPreview = true;

            NameTextBox.DataBindings.Add(
                "Text", _view, nameof(_view.Name));
            IntervalTextBox.DataBindings.Add(
                "Text", _view, nameof(_view.Interval));
            DescriptionTtextBox.DataBindings.Add(
                "Text", _view, nameof(_view.Description));

            //ListDataGridView.VirtualMode = true;

            ListDataGridView.RowHeadersVisible = false;
            ListDataGridView.AllowUserToAddRows = false;
            ListDataGridView.AutoGenerateColumns = true;
            ListDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ListDataGridView.ColumnHeadersDefaultCellStyle.Alignment =
                                DataGridViewContentAlignment.MiddleCenter;
            ListDataGridView.DataSource = _view.Views;

            _column = new DataGridViewCheckBoxColumn();
            _column.Name = "Check";
            _column.HeaderText = "AUTO";
            //_column.TrueValue = true;
            //_column.FalseValue = false;

            ListDataGridView.Columns.Insert(0, _column);
            ListDataGridView.Columns["Name"].ReadOnly = true;
            ListDataGridView.Columns["Interval"].ReadOnly = true;
            ListDataGridView.Columns["Interval"].Width = 50;
            ListDataGridView.Columns["DateTime"].ReadOnly = true;
            ListDataGridView.Columns["TradeType"].ReadOnly = true;
            ListDataGridView.Columns["Side"].ReadOnly = true;
            ListDataGridView.Columns["Price"].ReadOnly = true;
            ListDataGridView.Columns["Description"].ReadOnly = true;


            if (initial == "new")
            {
                tabControl1.SelectedIndex = 1;
            }

            _view.ListView();
        }
        private void StrategyForm_Load(object sender, EventArgs e)
        {
            Username.Text = Properties.Settings.Default.Username;
            SecureString password = Auth.DecryptString(Properties.Settings.Default.Password);
            Password.Text = Auth.ToInsecureString(password);
            SecureString apipassword = Auth.DecryptString(Properties.Settings.Default.APIPassword);
            APIPassword.Text = Auth.ToInsecureString(apipassword);
        }


        private void AddNewButton_Click(object sender, EventArgs e)
        {
            try
            {
                _view.AddView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DeletedButton_Click(object sender, EventArgs e)
        {
            _view.Remove(_rowIndex);
        }

        private void ListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                //string _name = ListDataGridView.Columns[e.ColumnIndex].CellType.Name;

                _rowIndex = e.RowIndex;
                if (e.ColumnIndex == 0 && ListDataGridView.Columns[e.ColumnIndex].CellType.Name
                                                == "DataGridViewCheckBoxCell")
                {

                }
                else
                {
                    string name = ListDataGridView.Rows[_rowIndex].Cells[1].Value.ToString();
                    int interval = Convert.ToInt32(ListDataGridView.Rows[_rowIndex].Cells[2].Value);

                    _view.ReloadText(_rowIndex, name, interval);
                }
            }

        }

        /// <summary>
        /// CheckBoxの値取得呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ListDataGridView.Columns[e.ColumnIndex].Name == "Check" && _checkBoxEventEnable)
            {

                ListDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        /// <summary>
        /// CheckBoxの値取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListDataGridView_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (ListDataGridView.Columns[e.ColumnIndex].Name == "Check" && _checkBoxEventEnable)
            {
                bool check = (bool)ListDataGridView.Rows[e.RowIndex].Cells[0].Value;
                string _name = ListDataGridView.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                int interval = Convert.ToInt32(ListDataGridView.Rows[e.RowIndex].Cells["Interval"].Value);

                _view.UpdateCheck(check, _name, interval);
                Console.WriteLine("Strategy UpdateCheck Check:{0} Nam {1}", check, _name);

            }
        }
        private void StrategyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool forward = e.Modifiers != Keys.Shift;
                //this.ProcessTabKey(forward);
                this.SelectNextControl(this.ActiveControl, forward, true, true, true);
                e.Handled = true;
            }
        }

        private void ListDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ////チェックを付ける

            for (int i = 0; i < ListDataGridView.Rows.Count; i++)
            {
                string name = ListDataGridView.Rows[i].Cells["Name"].Value.ToString();
                int interval = Convert.ToInt32(ListDataGridView.Rows[i].Cells["Interval"].Value);
                bool check = StrategyListCash.GetCheck(name, interval);
                Console.WriteLine("GetCheck Check:{0} Nam {1}", check, name);
                ListDataGridView.Rows[i].Cells[0].Value = check;
            }

        }

        private void StrategyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StrategyManeger.SaveToCsv();
            Properties.Settings.Default.Username = Username.Text;
            Properties.Settings.Default.Password = Auth.EncryptString(Auth.ToSecureString(Password.Text));
            Properties.Settings.Default.APIPassword = Auth.EncryptString(Auth.ToSecureString(APIPassword.Text));
            Properties.Settings.Default.Save();

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int index = tabControl1.SelectedIndex;
            if (index == 0)
            {

            }
            if (index == 1)
            {
                Username.Text = Properties.Settings.Default.Username;
                SecureString password = Auth.DecryptString(Properties.Settings.Default.Password);
                Password.Text = Auth.ToInsecureString(password);
                SecureString apipassword = Auth.DecryptString(Properties.Settings.Default.APIPassword);
                APIPassword.Text = Auth.ToInsecureString(apipassword);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
