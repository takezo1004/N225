using log4net.Repository.Hierarchy;
using log4net;
using log4net.Appender;
using N225.WinForm.Views;
using System;
using System.Windows.Forms;
using N225.Domain.Modules.Utils;
using System.IO;

namespace N225.WinForm
{
    internal static class Program
    {
        
        private static log4net.ILog _logger =
            log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _logger.Debug("Application Start");                       
            
            Application.Run(new TradeView());

        }
    }
}
