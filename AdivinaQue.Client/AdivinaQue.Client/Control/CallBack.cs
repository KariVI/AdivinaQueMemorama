using AdivinaQue.Client.Proxy;
using AdivinaQue.Client.Views;
using System;
using System.Collections.Generic;
using System.Windows;


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

        public void setPodio(Podio podio)
        {
            this.podio = podio;
        }
        public void RecieveMessage(String message)
        {
            chat.messagesCollection.Add(message);
        }
        public void setChat(Chat chat)
        {
            this.chat = chat;
        }
        public void setModify(Modify modify)
        {
            this.modify = modify;
        }
        public bool SendInvitationGame(String username)
        {
            var option = MessageBox.Show(username+ " invited you, acept?", "Message", MessageBoxButton.YesNo);
            bool value = false;
            if (option == MessageBoxResult.Yes)
            {
                value = true;
            }
            return value;
        }
        public void RecieveTopics(string[] topics)
        {
            foreach (var topic in topics)
            {
                gameConfiguration.topicsCollection.Add(topic);

            }
        }
        public void SetGameConfiguration(GameConfiguration gameConfiguration)
        {
            this.gameConfiguration = gameConfiguration;
        }

        public void RecieveEmails(string[] emails)
        {
            foreach (var email in emails)
            {
                //validationCode.emails.Add(email);
            }
        }
  

        public void RecieveUsers(Dictionary<string, object> users)
        {
            chat.usersCollection.Clear();
            if(playersList != null)
            {
                playersList.usersCollection.Clear();
                foreach (var username  in users.Keys)
                {
                    if(username != currentUsername)
                    {
                        playersList.usersCollection.Add(username);
                    }
                }
                 
            }

            foreach (var username in users.Keys)
            {
                chat.usersCollection.Add(username);      
            }


        }

        public void RecievePlayer(Player player)
        {
           modify.setPlayer(player);
        }
        public void SetCurrentUsername(String currentUsername)
        {
            this.currentUsername = currentUsername;
        }
        internal void setPlayersList(PlayersList playersList)
        {
            this.playersList = playersList;
        }
        public void RecieveScores(Dictionary<string, int> globalScores)
        {
            foreach (var player in globalScores)
            {
                podio.playersCollection.Add(player.Key);
                podio.scoresCollection.Add(player.Value);

            }


        }
        public void SetValidateCode(ValidationCode validationCode)
        {
            this.validationCode = validationCode;
        }
    }
}
