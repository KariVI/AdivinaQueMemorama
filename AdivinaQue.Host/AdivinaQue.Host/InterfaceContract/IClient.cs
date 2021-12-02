using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web.UI.WebControls;
using System.Windows.Media.Imaging;

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
        void ReceiveRival(String rival);
        [OperationContract(IsOneWay = true)]
        void RecieveScores(Dictionary<string, int> globalScores);


        [OperationContract(IsOneWay = true)]
        void ReceiveCardSeed(List<int> randomImageList, List<int> randomPositionList);
        [OperationContract(IsOneWay = true)]
        void ReceiveCorrectPair(Dictionary<BitmapImage, string> cards);
        [OperationContract(IsOneWay = true)]
        void ReceiveCardTurn(BitmapImage image, string name);
        [OperationContract(IsOneWay = true)]
        void ReceiveScoreRival(int score);
        [OperationContract(IsOneWay = true)]
        void ReceiveNextTurn(bool nextTurn);
        [OperationContract(IsOneWay = true)]
        void ReceiveNumberCardsFinded(int numberCardsFinded);
        [OperationContract(IsOneWay = true)]
        void ReceiveWinner(string winner);
        [OperationContract(IsOneWay = true)]
        void ReceiveUsersPlayed(List<string> usersPlayed);

    }

    
}

