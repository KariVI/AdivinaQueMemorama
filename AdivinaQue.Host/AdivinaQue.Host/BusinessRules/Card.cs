using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaQue.Host.BusinessRules
{
    [DataContract]
    public class Card

    {
        [DataMember]
        public int IdCard { get; set; }
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Topic { get; set; }
        [DataMember]
        public int IdPair { get; set; } 
    }
}
