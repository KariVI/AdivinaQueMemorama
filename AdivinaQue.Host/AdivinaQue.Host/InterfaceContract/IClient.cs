using AdivinaQue.Host.DatabaseAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaQue.Host.InterfaceContract
{

    [ServiceContract]

    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void RecieveMessage(string message);
        [OperationContract(IsOneWay = true)]
        void RecieveUsers(Dictionary<String, IClient> users);
        [OperationContract(IsOneWay = true)]
        void RecievePlayer(Player player);  
    }

    [DataContract]
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

