using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AdivinaQue.Host.InterfaceContract
{
    [ServiceContract(CallbackContract = typeof(IClient))]

    public interface IGameMgt
    {
      
        [OperationContract(IsOneWay = true)]
        void SendBoard(string toUsername, int size, string category);
        [OperationContract(IsOneWay = true)]
        void SendRival(string rival, string fromUsername);
        [OperationContract(IsOneWay = true)]
        void SendBoardLists(String toUsername, List<int> randomImageList, List<int> randomPositionList);
    
        [OperationContract(IsOneWay = true)]
        void SendCorrectCards(string toUsername, Dictionary<BitmapImage, string> cards);
        [OperationContract(IsOneWay = true)]
        void SendCardTurn(string toUsername, BitmapImage image, string name);

        [OperationContract(IsOneWay = true)]
        void SendScoreRival(string toUsername, int score);
        [OperationContract(IsOneWay = true)]
        void SendNextTurnRival(string toUsername, bool nextTurn);
        [OperationContract(IsOneWay = true)]
        void SendNumberCardsFinded(string toUsername, int numberCardsFinded);
        [OperationContract(IsOneWay = true)]
        void SendWinner(string toUsername, string winner);

        [OperationContract]
        bool SendGame(GameCurrently gameCurrently);
    }
}
