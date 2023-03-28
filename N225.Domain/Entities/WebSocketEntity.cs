namespace N225.Domain.Entities
{
    public class WebSocketEntity
    {
        public WebSocketEntity(
                    double currentPrice,
                    double askPrice,
                    double bidPrice
                    )
        {
            CurrentPrice = currentPrice;
            AskPrice = askPrice;
            BidPrice = bidPrice;

        }
        public double CurrentPrice { get; set; }
        public double AskPrice { get; set; }
        public double BidPrice { get; set; }
    }
}
