using System.Runtime.Serialization;

namespace N225.Domain.Elements
{
    [DataContract]
    public class TokenParam
    {

        [DataMember(Name = "APIPassword")]
        public string APIPassword { get; set; }

    }
}
