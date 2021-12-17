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
            PlayerMgtClient server = new PlayerMgtClient(context);
            Player player = new Player();
            player.Username = "AlonsoRRV";
            player.Password = "alonso2wRRV";
            player.Name = "Alonso Rodriguez Rodriguez";
            player.Email = "alonsoRodriguez1234@gmail.com";
            Assert.IsTrue(server.Register(player));
        }

        
        [TestMethod]
        public void TestDelete()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            String username = "Marii";
            Assert.IsTrue(server.Delete(username));
        }

        
        [TestMethod]
        public void TestDeleteFailed()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            String username = "Juan";
            Assert.IsFalse(server.Delete(username));
        }

        [TestMethod]
        public void TestReceiveMessage()
        {
            Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            server.Join("MariV", "12345678910");
            server.SendMessage("Hola", "MariV", "Todos");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.RecieveMessage(It.IsAny<string>()), Times.AtLeastOnce());
            server.DisconnectUser("MariV");
        }
       
        [TestMethod]
        public void TestSearchUsername()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            string username = "MariV";
            string password = "12345678910";
            server.Join(username, password);
            Assert.IsTrue(server.SearchUsername(username));
            server.DisconnectUser("MariV");
        }
        [TestMethod]
        public void TestSearchUsernameFailed()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            string username = "Roberto";
            Assert.IsFalse(server.SearchUsername(username));
        }
 
        /// <summary>
        /// pendiente
        /// </summary>
        [TestMethod]
        public void TestSearchInfoPlayerByUsername()
        {
            Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            PlayerMgtClient server = new PlayerMgtClient(context);
            server.SearchInfoPlayerByUsername("MariV");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.RecievePlayer(It.IsAny<Player>()), Times.AtLeastOnce());

        }

        /// <summary>
        /// pendiente
        /// </summary>
        [TestMethod]
        public void TestSearchInfoPlayerByUsernameFailed()
        {
            Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            PlayerMgtClient server = new PlayerMgtClient(context);
                
            server.SearchInfoPlayerByUsername("Pepe");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.RecievePlayer(It.IsAny<Player>()), Times.Never);

        }

        [TestMethod]
        public void TestModify()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            Player player = new Player();
            player.Username = "MariV";
            player.Password = "12345678910";
            player.Name = "Mariana Yazmin Vargas Segura";
            player.Email = "MarianaVSYazmin@hotmail.com";
            Assert.IsTrue(server.Modify(player, "MariV"));
        }

        [TestMethod]
        public void TestSendInvitation()
        {
            Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            PlayerMgtClient server = new PlayerMgtClient(context);
            server.Join("MariV", "12345678910");
            server.Join("Kari", "12345678910");
            server.SendInvitation("MariV", "Kari");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.SendInvitationGame(It.IsAny<string>()), Times.AtLeastOnce());
        }

        [TestMethod]
        public void TestGenerateCode()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            string code = server.GenerateCode();
            Assert.IsNotNull(code);
        }

        [TestMethod]

        public void SendGameTest()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            GameMgtClient server = new GameMgtClient(context);
            GameCurrently gameCurrently = new GameCurrently();
            DateTime dateTime = System.DateTime.Today;
             gameCurrently.Players =  new Dictionary<string, int>();
             gameCurrently.Date = dateTime.ToString();
             gameCurrently.Winner = "Kari";
             gameCurrently.ScoreWinner = 5;
             gameCurrently.Topic = "Pruebas";
             gameCurrently.Players.Add("Kari", 5);
             gameCurrently.Players.Add("MariV", 3);
            Console.WriteLine(gameCurrently.Players.ToString());
            Assert.IsTrue(server.SendGame(gameCurrently));
        }

        [TestMethod]
        public void TestGetEmail()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            string username = "MariV";
            string emailExpected = "MarianaVSYazmin@hotmail.com";
            Assert.IsTrue(server.GetEmailByUser(username).Equals(emailExpected));
        }

        [TestMethod]
        public void TestChangePassword()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            string username = "egy";
            string newPassword = "AW12QEdgar";
            Assert.IsTrue(server.ChangePassword(username,newPassword));
        }

        [TestMethod]
        public void TestFindUsername()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            string  username = "egy";
            Assert.IsTrue(server.FindUsername(username));
        }

        [TestMethod]
        public void TestFindUsernameFailed()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            string username = "Pepe";
            Assert.IsFalse(server.FindUsername(username));
        }

        [TestMethod]
        public void TestJoinFailed()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            string username = "MariV";
            string password = "mariV";
            Assert.IsFalse(server.Join(username, password));
            server.DisconnectUser("MariV");
        }

        [TestMethod]
        public void TestGetScores()
        {
            Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            PlayerMgtClient server = new PlayerMgtClient(context);
            server.Join("MariV", "12345678910");
            server.GetScores("MariV");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.RecieveScores(It.IsAny<Dictionary<string, int>>()), Times.AtLeastOnce());
        }

        [TestMethod]
        public void TestGetConnectedUsers()
        {
            Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            PlayerMgtClient server = new PlayerMgtClient(context);
            server.Join("MariV", "12345678910");
            server.GetConnectedUsers();
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.RecieveUsers(It.IsAny<Dictionary<string, object>>()), Times.AtLeastOnce());
        }


        [TestMethod]
        public void TestSendBoard()
        {
            Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
            Mock<IGameMgtCallback> mockCallbackGame = new Mock<IGameMgtCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            InstanceContext contextGame = new InstanceContext(mockCallbackGame.Object);
            PlayerMgtClient mgtClient = new PlayerMgtClient(context);
            GameMgtClient server = new GameMgtClient(contextGame);
            mgtClient.Join("MariV", "12345678910");
            server.SendBoard("MariV", 20, "Pruebas");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.SendBoardConfigurate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()), Times.AtLeastOnce());
        }

        [TestMethod]

        public void TestSendScoreRival()
        {
            Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
            Mock<IGameMgtCallback> mockCallbackGame = new Mock<IGameMgtCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            InstanceContext contextGame = new InstanceContext(mockCallbackGame.Object);
            PlayerMgtClient mgtClient = new PlayerMgtClient(context);

            GameMgtClient server = new GameMgtClient(contextGame);
            mgtClient.Join("MariV", "12345678910");
            server.SendScoreRival("MariV", 12);
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.ReceiveScoreRival(It.IsAny<int>()), Times.AtLeastOnce());
            mgtClient.DisconnectUser("MariV");
        }

        [TestMethod]

        public void TestSendNumberCardsFinded()
        {
            Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
            Mock<IGameMgtCallback> mockCallbackGame = new Mock<IGameMgtCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            InstanceContext contextGame = new InstanceContext(mockCallbackGame.Object);
            PlayerMgtClient mgtClient = new PlayerMgtClient(context);

            GameMgtClient server = new GameMgtClient(contextGame);
            mgtClient.Join("MariV", "12345678910");
            server.SendNumberCardsFinded("MariV", 12);
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.ReceiveNumberCardsFinded(It.IsAny<int>()), Times.AtLeastOnce());
            mgtClient.DisconnectUser("MariV");
        }

        [TestMethod]

        public void TestSendWinner()
        {
            Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
            Mock<IGameMgtCallback> mockCallbackGame = new Mock<IGameMgtCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            InstanceContext contextGame = new InstanceContext(mockCallbackGame.Object);
            PlayerMgtClient mgtClient = new PlayerMgtClient(context);

            GameMgtClient server = new GameMgtClient(contextGame);
            mgtClient.Join("MariV", "12345678910");
            server.SendWinner("MariV", "egy");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.ReceiveWinner(It.IsAny<string>()), Times.AtLeastOnce());
            mgtClient.DisconnectUser("MariV");
        }


        [TestMethod]

        public void TestGetEmails()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            List<string> emailsExpected = new List<string>();
            emailsExpected.Add("MarianaVSYazmin@gmail.com");
            emailsExpected.Add("MarianaVSYazmin@hotmail.com");
            emailsExpected.Add("zs19014013@estudiantes.uv.mx");
            emailsExpected.Add("egy@hotmail.com");
            emailsExpected.Add("alonsoRodriguez1234@gmail.com");
            server.GetEmails();
            int valueExpected =3 ;
            int valueReceived = 0;

            foreach (var email in server.GetEmails())
            {
                if (emailsExpected.Contains(email))
                {
                    valueReceived++;
                }
            }

            Assert.AreEqual(valueExpected,valueReceived);

        }


        [TestMethod]

        public void TestGetUsersConnected()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            List<string> usersExpected= new List<string>();
            server.Join("MariV", "12345678910");
            usersExpected.Add("MariV");
            bool value = true;

            foreach (var user in server.GetUsersConnected())
            {
                if (!usersExpected.Contains(user))
                {
                    value = false;
                }
            }

            Assert.IsTrue(value);

        }

        [TestMethod]

        public void TestGetUsers()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            List<string> usersExpected = new List<string>();
            usersExpected.Add("Marii");
            usersExpected.Add("MariV");
            usersExpected.Add("Kari");
            usersExpected.Add("egy");
            usersExpected.Add("AlonsoRRV");
            int valueExpected = 3;
            int valueReceived = 0;

            foreach (var user in server.GetUsers())
            {
                Console.WriteLine(user);

                if (usersExpected.Contains(user))
                {
                    valueReceived++;
                }
            }

            Assert.AreEqual(valueExpected, valueReceived);

        }


    }
        
}
