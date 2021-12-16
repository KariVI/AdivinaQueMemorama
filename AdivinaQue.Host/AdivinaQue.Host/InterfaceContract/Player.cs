using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaQue.Host.InterfaceContract
{
    [DataContract]
    /// <summary>
    ///  Guardar la información sobre un jugador 
    /// </summary> 
    public class Player
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Email { get; set; }
    }
}
