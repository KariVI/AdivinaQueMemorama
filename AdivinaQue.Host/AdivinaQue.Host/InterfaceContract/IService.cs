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
        [OperationContract(IsOneWay = true)]
        void Register(String username, String password, String name, String email);
        [OperationContract]
        bool SearchUsername(String newUsername);
        [OperationContract(IsOneWay = true)]
        void SearchInfoPlayerByUsername(String username);
        [OperationContract]
        string SendMailValidation(String email);
        [OperationContract(IsOneWay = true)]
        void Modify(Player player, String username);
        [OperationContract(IsOneWay = true)]
        void Delete(string username);
        [OperationContract(IsOneWay = true)]
        void SendMailInvitation(string email);
        [OperationContract]
        bool SendInvitation(String toUsername,String fromUsername);
    }
}
