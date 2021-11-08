using AdivinaQue.Client.Proxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    [TestClass]
    public class CallbackTest : IServiceCallback
    {
        public Dictionary<string, int> globalScores;


        public CallbackTest()
        {
            globalScores = new Dictionary<string, int>(); 
        }
        public void RecieveMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void RecievePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public void RecieveScores(Dictionary<string, int> globalScores)
        {
            foreach (var player in globalScores)
            {
             
                globalScores.Add(player.Key,player.Value);
            }
        }

        public void RecieveTopics(string[] topics)
        {
            throw new NotImplementedException();
        }

        public void RecieveUsers(Dictionary<string, object> users)
        {
            throw new NotImplementedException();
        }

        public void SendBoardConfigurate(string username, int size, string category)
        {
            throw new NotImplementedException();
        }

        public bool SendInvitationGame(string username)
        {
            throw new NotImplementedException();
        }
    }
}
