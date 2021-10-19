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
        public void RecieveMessage(string message)
        {
            chat.messagesCollection.Add(message);
        }
        public void setChat(Chat chat)
        {
            this.chat = chat;
        }

        public void RecieveUsers(Dictionary<string, object> users)
        {
            chat.usersCollection.Clear();


            foreach (var username in users.Keys)
            {
                chat.usersCollection.Add(username);
            }


        }
    }
}
