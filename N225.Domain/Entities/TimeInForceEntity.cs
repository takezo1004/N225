namespace N225.Domain.Entities
{
    public sealed class TimeInForceEntity
    {
        public TimeInForceEntity(int condition,string name)
        {
            Condition = condition;
            Name = name;
        }
        public int Condition { get; }
        public string Name { get; }
    }
}
