using N225.Domain;
using N225.Domain.Entities;
using N225.WinForm.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N225.WinForm.TradeViewModels
{
    //public class KabuClient : ListViewBase
    public class KabuClient : ViewModelBase
    {
        private string _currentPrice;
        private string _askPrice;
        private string _bidPrice;
        public string CurrentPrice
        {
            get { return _currentPrice; }
            set
            {
                SetPropety(ref _currentPrice, value);
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
            get { return _askPrice; }
            set
            {
                SetPropety(ref _askPrice, value);
            }
        }



        public void ReciveData(object sender, WebSocketEntity e)
        {
            try
            {
                Console.WriteLine("CurrentPrice:{0} AskPrice:{1} BidPrice:{2}",
                                            e.CurrentPrice, e.AskPrice, e.BidPrice);

                CurrentPrice = Convert.ToString(e.CurrentPrice);
                AskPrice = Convert.ToString(e.AskPrice);
                BidPrice = Convert.ToString(e.BidPrice);
                Shared.BidPrice = e.BidPrice;
                Shared.AskPrice = e.AskPrice;
                //ProfitCalculate(args.CurrentPrice);

            }
            catch (Exception )
            {
                //Console.WriteLine("エラーメッセージ{0] Source {1} {2}", e.Message, e.Source, "メッセージ終了");
            }
        }
    }
}
