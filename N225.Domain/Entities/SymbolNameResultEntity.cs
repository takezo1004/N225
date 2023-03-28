namespace N225.Domain.Entities
{
    public class SymbolNameResultEntity
    {
        public SymbolNameResultEntity(
                            string symbol,
                            string symbolName)
        {
            Symbol = symbol;
            SymbolName = symbolName;
        }
        public string Symbol { get; set; }
        public string SymbolName { get; set; }
    }
}
