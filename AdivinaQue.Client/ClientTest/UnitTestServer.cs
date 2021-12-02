using AdivinaQue.Client.Control;
using AdivinaQue.Client.Proxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;

namespace ClientTest
{
    [TestClass]
    public class UnitTestServer
    {
        
        private static ServiceHost serviceHost;
        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            serviceHost = new ServiceHost(typeof(CallBackPlayerMgr));
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
            CallBackPlayerMgr callback = new CallBackPlayerMgr();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            Assert.IsTrue(server.Join("MariV", "mariV"));
            server.DisconnectUser("MariV");
        }

        [TestMethod]
        public void TestJoinFailed()
        {
            CallBackPlayerMgr callback = new CallBackPlayerMgr();
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
            CallBackPlayerMgr callback = new CallBackPlayerMgr();
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
            CallBackPlayerMgr callback = new CallBackPlayerMgr();
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


        [TestMethod]
        public void TestRegister()
        {
            CallBackPlayerMgr callback = new CallBackPlayerMgr();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            Player player = new Player();
            player.Username = "Marii";
            player.Password = "1234";
            player.Name = "Mariana Yazmin Vargas";
            player.Email = "MarianaVSYazmin@gmail.com";
            Assert.IsTrue(server.Register(player));
        }

        [TestMethod]
        public void TestDelete()
        {
            CallBackPlayerMgr callback = new CallBackPlayerMgr();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            Assert.IsTrue(server.Delete("Marii"));
        }
        [TestMethod]
        public void TestDeleteFailed()
        {
            CallBackPlayerMgr callback = new CallBackPlayerMgr();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            Assert.IsFalse(server.Delete("Juan"));
        }

       
        [TestMethod]
        public void TextSearchUsername()
        {
            CallBackPlayerMgr callback = new CallBackPlayerMgr();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            server.Join("Mari", "1234");
            Assert.IsTrue(server.SearchUsername("Mari"));
            server.DisconnectUser("Mari");
        }
        [TestMethod]
        public void TextSearchUsernameFailed()
        {
            CallBackPlayerMgr callback = new CallBackPlayerMgr();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            Assert.IsFalse(server.SearchUsername("Roberto"));
        }

        [TestMethod]
        public void TestSearchInfoPlayerByUsername()
        {
            Mock<IServiceCallback> mockCallback = new Mock<IServiceCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            ServiceClient server = new ServiceClient(context);
            server.SearchInfoPlayerByUsername("Mari");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.RecievePlayer(It.IsAny<Player>()), Times.AtLeastOnce());

        }

        [TestMethod]
        public void TestSearchInfoPlayerByUsernameFailed()
        {
            Mock<IServiceCallback> mockCallback = new Mock<IServiceCallback>() { CallBase = true };
            //mockCallback.Setup(mock => mock.RecieveMessage(It.IsAny<String>()));
            InstanceContext context = new InstanceContext(mockCallback.Object);
            ServiceClient server = new ServiceClient(context);


            server.SearchInfoPlayerByUsername("Pepe");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.RecievePlayer(It.IsAny<Player>()), Times.Never);

        }

        [TestMethod]
        public void TestModify()
        {
            CallBackPlayerMgr callback = new CallBackPlayerMgr();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            Player player = new Player();
            player.Username = "Mari";
            player.Password = "1234";
            player.Name = "Mariana Yazmin Vargas";
            player.Email = "MarianaVSYazmin@gmail.com";
            Assert.IsTrue(server.Modify(player, "Mari"));
        }

        [TestMethod]
        public void TestSendInvitation()
        {
            Mock<IServiceCallback> mockCallback = new Mock<IServiceCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            ServiceClient server = new ServiceClient(context);
            server.Join("Mari", "1234");
            server.Join("Kari", "123");
            server.SendInvitation("Mari", "Kari");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.SendInvitationGame(It.IsAny<string>()), Times.AtLeastOnce());

        }

        [TestMethod]
        public void TextGenerateCode()
        {
            CallBackPlayerMgr callback = new CallBackPlayerMgr();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            string code = server.GenerateCode();
            Assert.IsNotNull(code);
        }






    }
}
