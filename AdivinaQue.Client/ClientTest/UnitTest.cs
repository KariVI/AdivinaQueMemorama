using AdivinaQue.Client.Control;
using AdivinaQue.Client.Proxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ClientTest
{
    [TestClass]
    public class UnitTest
    {
        
        private static ServiceHost serviceHost;
        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            serviceHost = new ServiceHost(typeof(CallbackTest));
            serviceHost.Open();
        }
        [ClassCleanup]
        public static void CleanupClass()
        {
            serviceHost.Close();
        }

        [TestMethod]
        public void TestJoin()
        {
            CallbackTest callback = new CallbackTest();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            Assert.IsTrue(server.Join("MariV", "mariV"));
            server.DisconnectUser("MariV");
        }

        [TestMethod]
        public void TestJoinFailed()
        {
            CallBackTest callback = new CallBackTest();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            Assert.IsFalse(server.Join("MariV", "mari"));
            server.DisconnectUser("MariV");

        }



        [TestMethod]
        public void TestReceiveMessage()
        {
            Mock<IServiceCallback> mockCallback = new Mock<IServiceCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            ServiceClient server = new ServiceClient(context);
            server.Join("MariV", "mariV");

            server.SendMessage("Hola", "MariV", "Todos");
            mockCallback.Verify(mock => mock.RecieveMessage(It.IsAny<string>()), Times.AtLeastOnce());
        }

        [TestMethod]
        public void TestGetScores()
        {
            Mock<IServiceCallback> mockCallback = new Mock<IServiceCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            ServiceClient server = new ServiceClient(context);
            server.Join("MariV", "mariV");

            server.GetScores("MariV");
            mockCallback.Verify(mock => mock.RecieveScores(It.IsAny<Dictionary<string, int>>()), Times.AtLeastOnce());
        }

        [TestMethod]
        public void TestGetTopics()
        {
            Mock<IServiceCallback> mockCallback = new Mock<IServiceCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            ServiceClient server = new ServiceClient(context);
            server.Join("MariV", "mariV");

            server.GetTopics("MariV");
            mockCallback.Verify(mock => mock.RecieveTopics(It.IsAny<string[]>()), Times.AtLeastOnce());
        }

        [TestMethod]
        public void TestGetConnectedUsers()
        {
            Mock<IServiceCallback> mockCallback = new Mock<IServiceCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            ServiceClient server = new ServiceClient(context);
            server.Join("MariV", "mariV");

            server.GetConnectedUsers();
            mockCallback.Verify(mock => mock.RecieveUsers(It.IsAny<Dictionary<string,object>>()), Times.AtLeastOnce());
        }


        [TestMethod]
        public void TestSendBoard()
        {
            Mock<IServiceCallback> mockCallback = new Mock<IServiceCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            ServiceClient server = new ServiceClient(context);
            server.Join("MariV", "mariV");

            server.SendBoard("MariV", 20, "All");
            mockCallback.Verify(mock => mock.SendBoardConfigurate(It.IsAny<string>(), It.IsAny < int>(), It.IsAny < string>()), Times.AtLeastOnce());
        }

        [TestMethod]

        public void TestGetEmails()
        {
            CallBackTest callback = new CallBackTest();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            List<string> emailsExpected = new List<string>();
            emailsExpected.Add("Kari11");
            emailsExpected.Add("mariV@gmail.com");
            emailsExpected.Add("angelicaiglesiase@hotmail.com");
            emailsExpected.Add("angelicaiglesiase@hotmail.com");
            server.GetEmails();
            bool value = true;
 
            foreach(var email in server.GetEmails())
            {
                if (! emailsExpected.Contains(email))
                {
                    value = false;
                }
            }

            Assert.IsTrue(value);

        }


        [TestMethod]

        public void TestGetUsers()
        {
            CallBackTest callback = new CallBackTest();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            List<string> usersExpected = new List<string>();
            usersExpected.Add("kari11 @gmail.com");
            usersExpected.Add("MariV");
            usersExpected.Add("angy");
            usersExpected.Add("egy");
            bool value = true;

            foreach (var user in server.GetUsers())
            {
                Console.WriteLine(user);
               
                if (!usersExpected.Contains(user))
                {
                    value = false;
                }
            }

            Assert.IsTrue(value);

        }

        /*[TestMethod]
        public void TestScores()
        {
            CallbackTest callback = new CallbackTest();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            Dictionary<string, int> globalScores = new Dictionary<string, int>();

            globalScores.Add("MariV", 50);
            globalScores.Add("kari11@gmail.com", 23);
            globalScores.Add("Angy", 7);
            server.Join("MariV", "mariV");
            server.GetScores("MariV");
            foreach (var player in callback.globalScores)
            {
                Console.WriteLine(player.Value);
                
            }
           // Assert.AreEqual(globalScores, callback.globalScores);




        }*/



    }
}
