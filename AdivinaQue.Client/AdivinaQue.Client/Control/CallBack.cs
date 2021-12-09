using AdivinaQue.Client.Proxy;
using AdivinaQue.Client.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AdivinaQue.Client.Control
{


    public class CallBack : IServiceCallback
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
        Proxy.ServiceClient server;
        private List<string> usersPlayedList;

        public void SetPodio(Podio podio)
        {
            this.podio = podio;
        }
        public void setHome(Home home)
        {
            this.home = home;
        }

        public void setServer(Proxy.ServiceClient server)
        {
           this.server = server;
        }

        public void RecieveMessage(String message)
        {
            chat.messagesCollection.Add(message);
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
            var option = Alert.ShowDialog(username + " " + Application.Current.Resources["lbInvitation"].ToString(), Application.Current.Resources["btNo"].ToString(), Application.Current.Resources["btYes"].ToString());
            bool value = false;
            if (option == AlertResult.Yes)
            {
                value = true;
            }
            return value;
        }
        public void RecieveTopics(string[] topics)
        {
            foreach (var topic in topics)
            {
               // gameConfiguration.topicsCollection.Add(topic);

            }
        }
        public void SetGameConfiguration(GameConfiguration gameConfiguration)
        {
            this.gameConfiguration = gameConfiguration;
        }

     
  

        public void RecieveUsers(Dictionary<string, object> users)
        {
            
            if(playersList != null)
            {
                playersList.usersCollection.Clear();
                foreach (var username  in users.Keys)
                {
            
                    if (username != currentUsername && !usersPlayedList.Contains(username))
                    {
                        playersList.usersCollection.Add(username);
                    }
                }
                 
            }
            if (chat!= null)
            {
                chat.usersCollection.Clear();
                foreach (var username in users.Keys)
                {
                    chat.usersCollection.Add(username);
                }
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
                podio.playersCollection.Clear();
                podio.scoresCollection.Clear();
                foreach (var player in globalScores)
                {
                    podio.playersCollection.Add(player.Key);
                    podio.scoresCollection.Add(player.Value);

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
            game = new Game(server,size, category);
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
          
                game.upCardRival.Clear();
                game.upCardsRival.Clear();
            
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
            game.turnOffRivalCards();
            game.upCardRival.Clear();
            game.upCardsRival.Clear();



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
            usersPlayedList = new List<string>();
            Array.ForEach(usersPlayed, value => usersPlayedList.Add(usersPlayed.ToString()));
            
            if (playersList != null)
            {
                playersList.usersPlayed.Clear();
                foreach (var user in usersPlayed)
                {
                    playersList.usersPlayed.Add(user);
                }
            }
        }

        public void ReceiveCardTurn(BitmapImage image, string name)
        {
            game.upCardRival.Clear();
            game.upCardRival.Add(image, name);
            game.upCardsRival.Add(image, name);
            game.turnRivalSelection();

            
        }
    }
}
