using AdivinaQue.Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaQue.Client.Control
{
    public class CallBack : Proxy.IServiceCallback
    {
        private Chat chat;
        private Podio podio;
        private GameConfiguration gameConfiguration;
        private ValidationCode validationCode;
        public void RecieveMessage(string message)
        {
            chat.messagesCollection.Add(message);
        }
        public void SetChat(Chat chat)
        {
            this.chat = chat;
        }

        public void SetPodio(Podio podio)
        {
            this.podio = podio;
        }

        public void SetValidateCode(ValidationCode validationCode)
        {
            this.validationCode = validationCode;
        }
        public void SetGameConfiguration(GameConfiguration gameConfiguration)
        {
            this.gameConfiguration = gameConfiguration;
        }

        public void RecieveUsers(Dictionary<string, object> users)
        {
            chat.usersCollection.Clear();


            foreach (var username in users.Keys)
            {
                chat.usersCollection.Add(username);
            }


        }

        public void RecieveScores(Dictionary<string, int> globalScores)
        {
            foreach (var player in globalScores)
            {
                podio.playersCollection.Add(player.Key);
                podio.scoresCollection.Add(player.Value);

            }           
        }

        public void RecieveTopics(string[] topics)
        {
            foreach (var topic in topics)
            {
               gameConfiguration.topicsCollection.Add(topic);

            }
        }

        public void RecieveEmails(string[] emails)
        {
            foreach (var email in emails)
            {

                validationCode.emails.Add(email);
            }
        }
    }
}
