using System;

namespace N225.Domain.ValueObject
{
    public sealed class Exchange
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Exchange()
        {
            GetDerivExchange();
        }
        public int DerivExchange { get; set; }

        //TimeSpan StartTime = new DateTime(01, 01, 01, 8, 45, 00).TimeOfDay;
        //TimeSpan EndTime = new DateTime(01, 01, 01, 15, 15, 00).TimeOfDay;
        //TimeSpan NightStart = new DateTime(01, 01, 01, 16, 30, 00).TimeOfDay;
        //TimeSpan NightEnd = new DateTime(01,01,01,6,00,00).TimeOfDay;

        /// <summary>
        /// 市場コード
        /// </summary>
        private void GetDerivExchange()
        {
            var timeNow = DateTime.Now.TimeOfDay;

            if (timeNow > Shared.NightEnd && timeNow <= Shared.EndTime)
            {
                DerivExchange = 23;
            }
            if (timeNow > Shared.EndTime)
            {
                DerivExchange = 24;
            }
            if (timeNow < Shared.NightEnd)
            {
                DerivExchange = 24;
            }
        }
        //Select Case val
        //Case "日通し": GetDerivExchange = 2		
        //Case "日中": GetDerivExchange = 23		
        //Case "夜間": GetDerivExchange = 24		
        //Case Else: GetDerivExchange = 999		

    }
}
