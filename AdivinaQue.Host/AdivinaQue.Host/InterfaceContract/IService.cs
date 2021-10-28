using AdivinaQue.Host.BusinessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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
        void getConnectedUsers();
        [OperationContract(IsOneWay = true)]
        void disconnectUser(String username);
        [OperationContract]
        Boolean register(Player player);
        [OperationContract]
        bool searchUsername(String newUsername);
        [OperationContract]
        string sendMail(string to, string asunto, string body);

        [OperationContract(IsOneWay = true)]
        void GetScores(String username);
        [OperationContract(IsOneWay = true)]
        void GetTopics(String username);
        [OperationContract(IsOneWay = true)]
        void GetEmails(String username);

    }
}
