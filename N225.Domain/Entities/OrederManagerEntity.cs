namespace N225.Domain.Entities
{
    public class OrederManagerEntity
    {
        public OrederManagerEntity()
        {
        }

        public OrederManagerEntity(string id, int reciveType, double orderQty, double cumQty, double price)
        {
            Id = id;
            ReciveType = reciveType;
            OrderQty = orderQty;
            CumQty = cumQty;
            Price = price;
        }

        public string Id { get; set; }
        public int ReciveType { get; set; }
        public double OrderQty { get; set; }
        public double CumQty { get; set; }
        public double Price { get; set; }
    }
}
