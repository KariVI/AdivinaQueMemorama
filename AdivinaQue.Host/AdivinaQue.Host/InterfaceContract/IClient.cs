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
        /// <summary>
        /// Envia el mensaje del servidor a la instancia de chat.
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true)]
        void RecieveMessage(String message);

        /// <summary>
        /// Recibe los usuarios conectados.
        /// </summary>
        /// <param name="users"></param>
        [OperationContract(IsOneWay = true)]
        void RecieveUsers(Dictionary<String, IClient> users);

        /// <summary>
        /// Recibe el jugador a modificar.
        /// </summary>
        /// <param name="player"></param>
        [OperationContract(IsOneWay = true)]
        void RecievePlayer(Player player);

        /// <summary>
        /// Envia la invitación a una partida.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>true si el jugador acepto la initación, false en caso contrario</returns>
        [OperationContract]
        bool SendInvitationGame(String username);

        /// <summary>
        /// Envia la configuración del tablero.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="size"></param>
        /// <param name="category"></param>
        [OperationContract(IsOneWay = true)]
        void SendBoardConfigurate(String username, int size, string category);

        /// <summary>
        /// Recibe el nombre de usuario del rival.
        /// </summary>
        /// <param name="rival"></param>
        [OperationContract(IsOneWay = true)]
        void ReceiveRival(String rival);

        /// <summary>
        /// Recibe los puntajes de todos los jugadores.
        /// </summary>
        /// <param name="globalScores"></param>
        [OperationContract(IsOneWay = true)]
        void RecieveScores(Dictionary<string, int> globalScores);


        /// /// <summary>
        /// Recibe la listas aleatorias para la configuración de las cartas.
        /// </summary>
        /// <param name="randomImageList">Lista de números aleatorios para las imagenes</param>
        /// <param name="randomPositionList">Lista de números aleatorios para las posiciones</param>
        [OperationContract(IsOneWay = true)]
        void ReceiveCardSeed(List<int> randomImageList, List<int> randomPositionList);

        /// <summary>
        /// Recive un par de cartas correctas.
        /// </summary>
        /// <param name="cards">Diccionario con las cartas correctas </param>
        [OperationContract(IsOneWay = true)]
        void ReceiveCorrectPair(Dictionary<BitmapImage, string> cards);

        /// <summary>
        /// Recibe la carta volteada en un turno.
        /// </summary>
        /// <param name="image">Imagen del botón</param>
        /// <param name="name">identificador del bóton</param>
        [OperationContract(IsOneWay = true)]
        void ReceiveCardTurn(BitmapImage image, string name);

        /// <summary>
        /// Recibe el puntaje del rival.
        /// </summary>
        /// <param name="score">Int, puntaje del rival</param>
        [OperationContract(IsOneWay = true)]
        void ReceiveScoreRival(int score);

        /// <summary>
        /// Recibe el turno siguiente.
        /// </summary>
        /// <param name="nextTurn">true si es turno del jugador, false en caso contrario</param>
        [OperationContract(IsOneWay = true)]
        void ReceiveNextTurn(bool nextTurn);

        /// <summary>
        /// Recibe el número de cartas encontradas.
        /// </summary>
        /// <param name="numberCardsFinded"></param>
        [OperationContract(IsOneWay = true)]
        void ReceiveNumberCardsFinded(int numberCardsFinded);

        /// <summary>
        /// Recibe el username del ganador de la partida.
        /// </summary>
        /// <param name="winner"></param>
        [OperationContract(IsOneWay = true)]
        void ReceiveWinner(string winner);

        /// <summary>
        /// Recibe una lista con los jugadores que se encuentran en una partida actualmente.
        /// </summary>
        /// <param name="usersPlayed">Lista con los nombres de usuario de los jugadores.</param>
        [OperationContract(IsOneWay = true)]
        void ReceiveUsersPlayed(List<string> usersPlayed);

    }

    
}

