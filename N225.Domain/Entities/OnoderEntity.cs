using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N225.Domain.Entities
{
    public class OnoderEntity
    {
        public string TradeMode { get; set; }
        public string RecvTime { get; set; }
        public string Strategy { get; set; }
        public int Iterval { get; set; }
        public string CashMargin { get; set; }
        public string Side { get; set; }
        public string State { get; set; } 
        public double OrderQty { get; set; }
        public double CumQty { get; set; }
        public double Price { get; set; }
        public string OrderID { get; set; }
        public string ExecutionID { get; set; }
    }
}
