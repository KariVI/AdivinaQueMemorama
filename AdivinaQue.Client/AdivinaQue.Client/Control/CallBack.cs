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

        public void SetPodio(Podio podio)
        {
            this.podio = podio;
        }
        public void SetHome(Home home)
        {
            this.home = home;
        }
        public void SetServerPlayer(Proxy.PlayerMgtClient server)
        {
            this.serverPlayer = server;
        }

        public void SetServer(Proxy.GameMgtClient server)
        {
           this.serverGame = server;
        }

        public void RecieveMessage(String message)
        {
            if (chat != null)
            {
                chat.MessagesCollection.Add(message);
            }
        }
        public void SetChat(Chat chat)
        {
            this.chat = chat;
        }
        public void SetModify(Modify modify)
        {
            this.modify = modify;
        }
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

        public void SetGameConfiguration(GameConfiguration gameConfiguration)
        {
            this.gameConfiguration = gameConfiguration;
        }

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

        public void RecievePlayer(Player player)
        {
           modify.SetPlayer(player);
        }
        public void SetCurrentUsername(String currentUsername)
        {
            this.currentUsername = currentUsername;
        }
        internal void SetPlayersList(PlayersList playersList)
        {
            this.playersList = playersList;
        }
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

        internal void SetGame(Game game)
        {

            this.game = game;
        }

        public void SetValidateCode(ValidationCode validationCode)
        {
            this.validationCode = validationCode;
        }

        public void SendBoardConfigurate(string username, int size, string category)
        {
            game = new Game(serverGame,size, category);
            
            game.SetUsername(username);
            home.Hide();
            game.home = home;
            game.Show();        
        }

        public void ReceiveCardSeed(int[] randomImageList, int[] randomPositionList)
        {
            game.SetRandomLists(randomImageList, randomPositionList);
        }

        public void ReceiveRival(string rival)
        {
            game.SetUsernameRival(rival);
        }

        public void ReceiveCorrectPair(Dictionary<BitmapImage, string> cards)
        {

                game.TurnOffRivalCards();
                game.UpCardRival.Clear();
                game.UpCardsRival.Clear();            
                game.SetCorrectCards(cards);
          
        }

        public void ReceiveScoreRival(int score)
        {
            game.ScoreRival = score;
            game.tbRivalScore.Text = Convert.ToString(score);
        }

        public void ReceiveNextTurn(bool nextTurn)
        {

            game.NextTurn = nextTurn;
            game.TurnOffRivalCards();
            game.UpCardRival.Clear();
            game.UpCardsRival.Clear();



        }

        public void ReceiveNumberCardsFinded(int numberCardsFinded)
        {
            game.NumberCardsFinded = numberCardsFinded;
        }

        public void ReceiveWinner(string winner)
        {
            game.ShowWinner(winner);
        }

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

        public void ReceiveCardTurn(BitmapImage image, string name)
        {
            game.UpCardRival.Clear();
            game.UpCardRival.Add(image, name);
            game.UpCardsRival.Add(image, name);
            game.TurnRivalSelection();

            
        }
    }
}
