using AdivinaQue.Client.Proxy;
using AdivinaQue.Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaQue.Client.Control
{

  
    public class CallBack : IServiceCallback
    {
        private Chat chat;
        private Modify modify;
        private PlayersList playersList;
        public void RecieveMessage(string message)
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

        public void RecieveUsers(Dictionary<string, object> users)
        {
            chat.usersCollection.Clear();
            if(playersList != null)
            {
                Console.WriteLine("aaa");
                playersList.usersCollection.Clear();
            }
            


            foreach (var username in users.Keys)
            {
                chat.usersCollection.Add(username);
                if (playersList != null)
                {
                    playersList.usersCollection.Add(username);
                }      
            }


        }

        public void RecievePlayer(Player player)
        {
           modify.setPlayer(player);
        }

        internal void setPlayersList(PlayersList playersList)
        {
            this.playersList = playersList;
        }
    }
}
