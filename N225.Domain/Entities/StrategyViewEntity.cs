using System;

namespace N225.Domain.Entities
{
    public sealed class StrategyViewEntity
    {
        public bool Check { get; set; } = false;
        public string Name { get; set; } = String.Empty;
        public int Interval { get; set; } = 0;
        public string DateTime { get; set; } = String.Empty;
        public string TradeType { get; set; } = String.Empty;
        public string Side { get; set; } = String.Empty;
        public string Price { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
    }
}
