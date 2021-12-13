using AdivinaQue.Host.Exception;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.UI.WebControls;
using System.Windows.Media.Imaging;

namespace AdivinaQue.Host.InterfaceContract
{
    [ServiceContract(CallbackContract = typeof(IClient))]

    public interface IPlayerMgt
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
        [OperationContract]
        bool FindUsername(String username);
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
        string SendMail(string to, string asunto, string body) ;
        [OperationContract]
        List<String> GetEmails();
        [OperationContract]
        string GetEmailByUser(string username);

        [OperationContract]
        bool ChangePassword(string username, string newPassword);
        [OperationContract]
        List<string> GetUsers();
        [OperationContract]
        List<string> GetUsersConnected();

        [OperationContract(IsOneWay = true)]
        void GetScores(string username);
        [OperationContract(IsOneWay = true)]
        void GetCurrentlyUserPlayed();

    }
}
