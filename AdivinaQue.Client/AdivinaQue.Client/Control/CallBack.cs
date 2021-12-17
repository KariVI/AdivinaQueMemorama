using AdivinaQue.Client.Proxy;
using AdivinaQue.Client.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace AdivinaQue.Client.Control
{


    public class CallBack : IGameMgtCallback, IPlayerMgtCallback
    {
        private Chat chat;
        private Modify modify;
        private PlayersList playersList;
        private string currentUsername;
        private Podio podio;
        private ValidationCode validationCode;
        private GameConfiguration gameConfiguration;
        private Game game;
        private Home home;
        private  Proxy.GameMgtClient serverGame;
        private Proxy.PlayerMgtClient serverPlayer;
        private  List<String> usersPlayed = new List<String>();

        /// <summary>
        /// Inicializa la variable podio.
        /// </summary>
        /// <param name="podio"></param>
        public void SetPodio(Podio podio)
        {
            this.podio = podio;
        }

        /// <summary>
        /// Inicializa la variable home.
        /// </summary>
        /// <param name="home"></param>
        public void SetHome(Home home)
        {
            this.home = home;
        }

        /// <summary>
        /// Inicializa la variable serverPlayer.
        /// </summary>
        /// <param name="server"></param>
        public void SetServerPlayer(Proxy.PlayerMgtClient server)
        {
            this.serverPlayer = server;
        }

        /// <summary>
        /// Inicializa la variable serverGame.
        /// </summary>
        /// <param name="server"></param>
        public void SetServer(Proxy.GameMgtClient server)
        {
           this.serverGame = server;
        }

        /// <summary>
        /// Envia el mensaje del servidor a la instancia de chat.
        /// </summary>
        /// <param name="message"></param>
        public void RecieveMessage(String message)
        {
            if (chat != null)
            {
                chat.MessagesCollection.Add(message);
            }
        }

        /// <summary>
        /// Inicializa la variable chat.
        /// </summary>
        /// <param name="chat"></param>
        public void SetChat(Chat chat)
        {
            this.chat = chat;
        }

        /// <summary>
        /// Inicializa la variable modify.
        /// </summary>
        /// <param name="modify"></param>
        public void SetModify(Modify modify)
        {
            this.modify = modify;
        }

        /// <summary>
        /// Envia la invitación a una partida.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>true si el jugador acepto la initación, false en caso contrario</returns>
        public bool SendInvitationGame(String username)
        {
            var option = Alert.ShowDialogWithResponse(username + " " + Application.Current.Resources["lbInvitation"].ToString(), Application.Current.Resources["btNo"].ToString(), Application.Current.Resources["btYes"].ToString());
            bool value = false;
            if (option == AlertResult.Yes)
            {
                value = true;
            }
            return value;
        }

        /// <summary>
        /// Inicializa la variable gameConfiguration.
        /// </summary>
        /// <param name="gameConfiguration"></param>
        public void SetGameConfiguration(GameConfiguration gameConfiguration)
        {
            this.gameConfiguration = gameConfiguration;
        }

        /// <summary>
        /// Recibe los usuarios conectados.
        /// </summary>
        /// <param name="users"></param>
        public void RecieveUsers(Dictionary<string, object> users)
        {
            
            if(playersList != null)
            {
                playersList.UsersCollection.Clear();
                foreach (var username  in users.Keys)
                {
                    if(username != currentUsername)
                    {
                        playersList.UsersCollection.Add(username);
                    }
                }
                 
            }
            if (chat!= null)
            {
                chat.UsersCollection.Clear();
                foreach (var username in users.Keys)
                {
                    chat.UsersCollection.Add(username);
                }
                chat.UsersCollection.Remove(currentUsername);
            }


        }

        /// <summary>
        /// Recibe el jugador a modificar.
        /// </summary>
        /// <param name="player"></param>
        public void RecievePlayer(Player player)
        {
           modify.SetPlayer(player);
        }

        /// <summary>
        /// Inicializa la variable currentUsername.
        /// </summary>
        /// <param name="currentUsername"></param>
        public void SetCurrentUsername(String currentUsername)
        {
            this.currentUsername = currentUsername;
        }

        /// <summary>
        /// Inicializa la variable playerList
        /// </summary>
        /// <param name="playersList"></param>
        internal void SetPlayersList(PlayersList playersList)
        {
            this.playersList = playersList;
        }

        /// <summary>
        /// Recibe los puntajes de todos los jugadores.
        /// </summary>
        /// <param name="globalScores"></param>
        public void RecieveScores(Dictionary<string, int> globalScores)
        {
            if (podio != null)
            {
                podio.PlayersCollection.Clear();
                podio.ScoresCollection.Clear();
                foreach (var player in globalScores)
                {
                    podio.PlayersCollection.Add(player.Key);
                    podio.ScoresCollection.Add(player.Value);

                }
            }


        }

        /// <summary>
        /// Inicializa la variable game.
        /// </summary>
        /// <param name="game"></param>
        internal void SetGame(Game game)
        {

            this.game = game;
        }

        /// <summary>
        /// Inicializa el código de validación.
        /// </summary>
        /// <param name="validationCode"></param>
        public void SetValidateCode(ValidationCode validationCode)
        {
            this.validationCode = validationCode;
        }

        /// <summary>
        /// Envia la configuración del tablero.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="size"></param>
        /// <param name="category"></param>
        public void SendBoardConfigurate(string username, int size, string category)
        {
            game = new Game(serverGame,size, category);
            
            game.SetUsername(username);
            home.Hide();
            game.home = home;
            game.Show();        
        }

        /// <summary>
        /// Recibe la listas aleatorias para la configuración de las cartas.
        /// </summary>
        /// <param name="randomImageList">Lista de números aleatorios para las imagenes</param>
        /// <param name="randomPositionList">Lista de números aleatorios para las posiciones</param>
        public void ReceiveCardSeed(int[] randomImageList, int[] randomPositionList)
        {
            game.SetRandomLists(randomImageList, randomPositionList);
        }

        /// <summary>
        /// Recibe el nombre de usuario del rival.
        /// </summary>
        /// <param name="rival"></param>
        public void ReceiveRival(string rival)
        {
            game.SetUsernameRival(rival);
        }

        /// <summary>
        /// Recive un par de cartas correctas.
        /// </summary>
        /// <param name="cards">Diccionario con las cartas correctas </param>
        public void ReceiveCorrectPair(Dictionary<BitmapImage, string> cards)
        {
           
                game.TurnOffRivalCards();
            
                game.UpCardRival.Clear();
                game.UpCardsRival.Clear();            
                game.SetCorrectCards(cards);
          
        }

        /// <summary>
        /// Recibe el puntaje del rival.
        /// </summary>
        /// <param name="score">Int, puntaje del rival</param>
        public void ReceiveScoreRival(int score)
        {
            game.ScoreRival = score;
            game.tbRivalScore.Text = Convert.ToString(score);
        }

        /// <summary>
        /// Recibe el turno siguiente.
        /// </summary>
        /// <param name="nextTurn">true si es turno del jugador, false en caso contrario</param>
        public void ReceiveNextTurn(bool nextTurn)
        {

            game.NextTurn = nextTurn;
            game.TurnOffRivalCards();
            game.UpCardRival.Clear();
            game.UpCardsRival.Clear();

        }

        /// <summary>
        /// Recibe el número de cartas encontradas.
        /// </summary>
        /// <param name="numberCardsFinded"></param>
        public void ReceiveNumberCardsFinded(int numberCardsFinded)
        {
            game.NumberCardsFinded = numberCardsFinded;
        }

        /// <summary>
        /// Recibe el username del ganador de la partida.
        /// </summary>
        /// <param name="winner"></param>
        public void ReceiveWinner(string winner)
        {
            game.ShowWinner(winner);
        }

        /// <summary>
        /// Recibe una lista con los jugadores que se encuentran en una partida actualmente.
        /// </summary>
        /// <param name="usersPlayed">Lista con los nombres de usuario de los jugadores.</param>
        public void ReceiveUsersPlayed(string[] usersPlayed)
        {
            if (playersList != null)
            {
                playersList.UsersPlayedCollection.Clear();
                foreach (var player in usersPlayed)
                {
                    if (playersList.UsersCollection.Contains(player))
                    {
                        playersList.UsersCollection.Remove(player);
                    }   
                    playersList.UsersPlayedCollection.Add(player);
                }
            }
        }

        /// <summary>
        /// Recibe la carta volteada en un turno.
        /// </summary>
        /// <param name="image">Imagen del botón</param>
        /// <param name="name">identificador del bóton</param>
        public void ReceiveCardTurn(BitmapImage image, string name)
        {
            game.UpCardRival.Clear();
            game.UpCardRival.Add(image, name);
            game.UpCardsRival.Add(image, name);
            game.TurnRivalSelection(); 
        }
    }
}
