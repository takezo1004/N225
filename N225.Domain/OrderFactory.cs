using N225.Domain.Repositories;
using System;

namespace N225.Domain
{
    enum MemberKind
    {
        Market = 0,
        Best = 1,
        Limit = 2,
        Stop = 3,

    }

    /// <summary>
    /// Order Factory 
    /// </summary>
    public static class OrderFactory
    {

        public static IOrder Create(int order)
        {
            if (order == Convert.ToInt32(MemberKind.Best))
            {
                return new BestMarketOder();
            }
            if (order == Convert.ToInt32(MemberKind.Market))
            {
                return new MarketOrder();
            }
            if (order == Convert.ToInt32(MemberKind.Limit))
            {
                return new LimitOrder();
            }
            return new StopOrder();
        }
    }
}
