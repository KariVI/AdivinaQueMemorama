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

    /// <summary>
    /// Contrato en relación al la administración del  juego 
    /// </summary> 
    public interface IGameMgt
    {
      
        [OperationContract(IsOneWay = true)]

        /// <summary>
        /// Mandar las especificaciones del tablero  al rival
        /// </summary> 
        /// <param name="toUsername"> Quien va recibir el tablero</param>
        /// <param name="size"></param>
        /// <param name="category"></param>
        void SendBoard(string toUsername, int size, string category);
        [OperationContract(IsOneWay = true)]
         /// <summary>
        /// Mandar el rival al otro jugador
       /// </summary> 
        /// <param name="rival"> </param>
        /// <param name="fromUsername"> </param>
        void SendRival(string rival, string fromUsername);
        [OperationContract(IsOneWay = true)]
        /// <summary>
        /// Mandar las listas correspondientes al tablero 
        /// </summary> 
        /// <param name="toUsername"> </param>
        /// <param name="randomImageList"> Imagenes del tablero </param>
        /// <param name="randomPositionList"> Posiciones del tablero </param>
        void SendBoardLists(String toUsername, List<int> randomImageList, List<int> randomPositionList);
    
        [OperationContract(IsOneWay = true)]
        /// <summary>
        /// Mandar las cartas correctas al rival 
        /// </summary> 
        /// <param name="toUsername"> </param>
        /// <param name="cards"> Par de cartas </param>
        void SendCorrectCards(string toUsername, Dictionary<BitmapImage, string> cards);
        [OperationContract(IsOneWay = true)]
        /// <summary>
        /// Mandar las cartas que esta volteando actualmente el jugador al rival
        /// </summary> 
        /// <param name="toUsername"> </param>
        ///  <param name="image">  Imagen de la carta</param>
        ///  <param name="name">  Identificador de la carta</param>
        void SendCardTurn(string toUsername, BitmapImage image, string name);

        [OperationContract(IsOneWay = true)]
        /// <summary>
        /// Mandar el puntaje del jugador al rival 
        /// </summary> 
        /// <param name="toUsername"> </param>
        /// <param name="score"> Par de cartas </param>
        void SendScoreRival(string toUsername, int score);

        [OperationContract(IsOneWay = true)]

        void SendNextTurnRival(string toUsername, bool nextTurn);
        [OperationContract(IsOneWay = true)]
        /// <summary>
        /// Mandar el total de cartas encontradas en el tablero  al rival
        /// </summary> 
        /// <param name="toUsername"> </param>
        /// <param name="numberCardsFinded"> Total de cartas encontradas</param>
        void SendNumberCardsFinded(string toUsername, int numberCardsFinded);
        [OperationContract(IsOneWay = true)]
        /// <summary>
        /// Mandar el ganador al rival
        /// </summary> 
        /// <param name="toUsername"> </param>
        ///  <param name="winner"> </param>
        void SendWinner(string toUsername, string winner);
        [OperationContract(IsOneWay = true)]
        /// <summary>
        /// Cuando finaliza un juego es necesario quitar a los jugadores del listado de personas jugando 
        /// </summary> 
        /// <param name="username"> </param>
        ///  <param name="rival"> </param>
        void DisconnectPlayers(string username, string rival);

        [OperationContract]
        /// <summary>       
        /// Mandar el juego finalizado para registrar  
        ///   </summary> 
        /// <param name="gameCurrently"> Juego finalizado </param>
        bool SendGame(GameCurrently gameCurrently);
       
    }
}
