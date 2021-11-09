using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace AdivinaQue.Host.InterfaceContract
{

    [ServiceContract]

    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void RecieveMessage(String message);
        [OperationContract(IsOneWay = true)]
        void RecieveUsers(Dictionary<String, IClient> users);
        [OperationContract(IsOneWay = true)]
        void RecievePlayer(Player player);
        [OperationContract]
        bool SendInvitationGame(String username);
        [OperationContract(IsOneWay = true)]
        void SendBoardConfigurate(String username, int size, string category);
        [OperationContract(IsOneWay = true)]
        void RecieveScores(Dictionary<string, int> globalScores);
        
        [OperationContract(IsOneWay = true)]
        void RecieveTopics(List<String> topics);
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

