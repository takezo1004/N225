using System.Runtime.Serialization;

namespace N225.Domain
{

    [DataContract]
    public class CancelOrderParam
    {
        [DataMember(Name = "OrderId")]
        public string OrderId { get; set; }

        [DataMember(Name = "Password")]
        public string OrderPassword { get; set; }

    }

}
