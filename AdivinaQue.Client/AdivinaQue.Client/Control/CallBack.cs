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
        public void RecieveMessage(string message)
        {
            chat.messagesCollection.Add(message);
        }
        public void setChat(Chat chat)
        {
            this.chat = chat;
        }

        public void setPodio(Podio podio)
        {
            this.podio = podio;
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
    }
}
