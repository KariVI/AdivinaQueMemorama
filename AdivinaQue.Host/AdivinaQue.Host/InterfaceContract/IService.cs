using System;
using System.ServiceModel;

namespace AdivinaQue.Host.InterfaceContract
{
    [ServiceContract(CallbackContract = typeof(IClient))]

    public interface IService
    {
        [OperationContract]
        Boolean Join(string username, string password);
        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, String username, string userReceptor);
        [OperationContract(IsOneWay = true)]
        void GetConnectedUsers();
        [OperationContract(IsOneWay = true)]
        void DisconnectUser(String username);
        [OperationContract]
        Boolean Register(Player player);
        [OperationContract]
        bool SearchUsername(String newUsername);
        [OperationContract(IsOneWay = true)]
        void SearchInfoPlayerByUsername(String username);
        [OperationContract]
        string GenerateCode();
        [OperationContract]
        bool Modify(Player player, String username);
        [OperationContract]
        bool Delete(string username);
        [OperationContract]
        bool SendInvitation(String toUsername,String fromUsername);
        [OperationContract]
        string SendMail(string to, string asunto, string body);
        [OperationContract(IsOneWay = true)]
        void GetScores(String username);
        [OperationContract(IsOneWay = true)]
        void GetEmails(string username);
        [OperationContract(IsOneWay = true)]
        void GetTopics(string username);
       
    }
}
