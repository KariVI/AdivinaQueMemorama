using AdivinaQue.Client.Control;
using AdivinaQue.Client.Proxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.ServiceModel;
using System.Threading;

namespace ClientTest
{
    [TestClass]
    public class UnitTest
    {
        private static ServiceHost serviceHost;
        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            serviceHost = new ServiceHost(typeof(AdivinaQue.Client.Control.CallBack));
            serviceHost.Open();
        }
        [ClassCleanup]
        public static void CleanupClass()
        {
            serviceHost.Close();
        }

        [TestMethod]
        public void TestRegister()
        {
            CallBack callback = new CallBack();
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
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            Assert.IsTrue(server.Delete("Marii"));
        }
        [TestMethod]
        public void TestDeleteFailed()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            Assert.IsFalse(server.Delete("Juan"));
        }

        [TestMethod]
        public void TestReceiveMessage()
        {
            Mock<IServiceCallback> mockCallback = new Mock<IServiceCallback>() { CallBase = true };
            //mockCallback.Setup(mock => mock.RecieveMessage(It.IsAny<String>()));
            InstanceContext context = new InstanceContext(mockCallback.Object);
            ServiceClient server = new ServiceClient(context);
            server.Join("Mari", "1234");

            server.SendMessage("Hola", "Mari", "Todos");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.RecieveMessage(It.IsAny<string>()), Times.AtLeastOnce());
            server.DisconnectUser("Mari");
            // mockSomeClass.VerifyAll();
        }
        [TestMethod]
        public void TextSearchUsername()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            server.Join("Mari", "1234");
            Assert.IsTrue(server.SearchUsername("Mari"));
            server.DisconnectUser("Mari");
        }
        [TestMethod]
        public void TextSearchUsernameFailed()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            Assert.IsFalse(server.SearchUsername("Roberto"));
        }

        [TestMethod]
        public void TestSearchInfoPlayerByUsername()
        {
            Mock<IServiceCallback> mockCallback = new Mock<IServiceCallback>() { CallBase = true };
            //mockCallback.Setup(mock => mock.RecieveMessage(It.IsAny<String>()));
            InstanceContext context = new InstanceContext(mockCallback.Object);
            ServiceClient server = new ServiceClient(context);
            server.SearchInfoPlayerByUsername("Mari");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.RecievePlayer(It.IsAny<Player>()), Times.AtLeastOnce());

            // mockSomeClass.VerifyAll();
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

            // mockSomeClass.VerifyAll();
        }

        [TestMethod]
        public void TestModify()
        {
            CallBack callback = new CallBack();
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
            //mockCallback.Setup(mock => mock.RecieveMessage(It.IsAny<String>()));
            InstanceContext context = new InstanceContext(mockCallback.Object);
            ServiceClient server = new ServiceClient(context);
            server.Join("Mari", "1234");
            server.Join("Kari", "123");
            server.SendInvitation("Mari", "Kari");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.SendInvitationGame(It.IsAny<string>()), Times.AtLeastOnce());

            // mockSomeClass.VerifyAll();
        }

        [TestMethod]
        public void TextGenerateCode()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            string code = server.GenerateCode();
            Assert.IsNotNull(code);
        }

        [TestMethod]

        public void SendGameTest()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            GameCurrently gameCurrently = new GameCurrently();
            System.DateTime dateTime = System.DateTime.Today;
            /* gameCurrently.Players =  new System.Collections.Generic.Dictionary<string, int>();
             gameCurrently.date = dateTime.ToString();
             gameCurrently.winner = "angy";
             gameCurrently.scoreWinner = 5;
             gameCurrently.topic = "Pruebas";
             gameCurrently.Players.Add("angy", 5);
             gameCurrently.Players.Add("MariV", 3);*/
            Console.WriteLine(gameCurrently.Players.ToString());
            Assert.IsTrue(server.SendGame(gameCurrently));
        }

        [TestMethod]
        public void TestGetEmail()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);

            Assert.IsTrue(server.GetEmailByUser("MariV").Equals("mariV@gmail.com"));
        }

        [TestMethod]
        public void TestChangePassword()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);

            Assert.IsTrue(server.ChangePassword("egy", "AW12"));
        }

        [TestMethod]
        public void TestFindUsername()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);

            Assert.IsTrue(server.FindUsername("egy"));
        }

    }
}
