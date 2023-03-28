using N225.WinForm.Views;
using System;
using System.Windows.Forms;

namespace N225.WinForm
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TradeView());

        }
    }
}
