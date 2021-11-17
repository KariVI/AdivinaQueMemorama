using AdivinaQue.Host.Exception;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.UI.WebControls;
using System.Windows.Media.Imaging;

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
        string SendMail(string to, string asunto, string body) ;
        [OperationContract]
        List<String> GetEmails();
        [OperationContract(IsOneWay = true)]
        void GetTopics(string username);
        [OperationContract(IsOneWay = true)]
        void GetScores(string username);

        [OperationContract(IsOneWay = true)]
        void SendBoard(string toUsername, int size, string category);
        [OperationContract(IsOneWay = true)]
        void SendRival(string rival, string fromUsername);
        [OperationContract(IsOneWay = true)]
        void SendBoardLists(String toUsername, List<int> randomImageList, List<int> randomPositionList);
        [OperationContract]
        List<String> GetUsers();
        [OperationContract(IsOneWay = true)]
        void SendCorrectCards(string toUsername, Dictionary<BitmapImage, string> cards);

        [OperationContract(IsOneWay = true)]
        void SendScoreRival(string toUsername, int score);
        [OperationContract(IsOneWay = true)]
        void SendNextTurnRival(string toUsername, bool nextTurn);
        [OperationContract(IsOneWay = true)]
        void SendNumberCardsFinded(string toUsername, int numberCardsFinded);

        [OperationContract]
        bool SendGame(GameCurrently gameCurrently);
    }
}
