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
            Assert.IsTrue(server.Delete("Marii"));
        }
        [TestMethod]
        public void TestDeleteFailed()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            Assert.IsFalse(server.Delete("Juan"));
        }

        [TestMethod]
        public void TestReceiveMessage()
        {
            Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            server.Join("MariV", "mariV");

            server.SendMessage("Hola", "MariV", "Todos");
            Thread.Sleep(2000);
            mockCallback.Verify(mock => mock.RecieveMessage(It.IsAny<string>()), Times.AtLeastOnce());
            server.DisconnectUser("MariV");
        }
        [TestMethod]
        public void TextSearchUsername()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            server.Join("MariV", "Mariv");
            Assert.IsTrue(server.SearchUsername("Mariv"));
            server.DisconnectUser("MariV");
        }
        [TestMethod]
        public void TextSearchUsernameFailed()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            Assert.IsFalse(server.SearchUsername("Roberto"));
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
            player.Password = "mariV";
            player.Name = "Mariana Yazmin Vargas Segura";
            player.Email = "MarianaVSYazmin@gmail.com";
            Assert.IsTrue(server.Modify(player, "MariV"));
        }

        [TestMethod]
        public void TestSendInvitation()
        {
            Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            PlayerMgtClient server = new PlayerMgtClient(context);
            server.Join("MariV", "mariV");
            server.Join("Kari", "karival3");
            server.SendInvitation("Mari", "Kari");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.SendInvitationGame(It.IsAny<string>()), Times.AtLeastOnce());

        }

        [TestMethod]
        public void TextGenerateCode()
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
            System.DateTime dateTime = System.DateTime.Today;
             gameCurrently.Players =  new System.Collections.Generic.Dictionary<string, int>();
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

            Assert.IsTrue(server.GetEmailByUser("MariV").Equals("mariV@gmail.com"));
        }

        [TestMethod]
        public void TestChangePassword()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);

            Assert.IsTrue(server.ChangePassword("egy", "AW12QEdgar"));
        }

        [TestMethod]
        public void TestFindUsername()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);

            Assert.IsTrue(server.FindUsername("egy"));
        }

        [TestMethod]
        public void TestFindUsernameFailed()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);

            Assert.IsFalse(server.FindUsername("pepe"));
        }

        [TestMethod]
        public void TestJoinFailed()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            Assert.IsFalse(server.Join("MariV", "mari"));
            server.DisconnectUser("MariV");

        }




        [TestMethod]
        public void TestGetScores()
        {
            Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            PlayerMgtClient server = new PlayerMgtClient(context);
            server.Join("MariV", "mariV");

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
            server.Join("MariV", "mariV");

            server.GetConnectedUsers();
            Thread.Sleep(1000);

            mockCallback.Verify(mock => mock.RecieveUsers(It.IsAny<Dictionary<string, object>>()), Times.AtLeastOnce());
        }


        [TestMethod]
        public void TestSendBoard()
        {
            Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };

            InstanceContext context = new InstanceContext(mockCallback.Object);
            PlayerMgtClient mgtClient = new PlayerMgtClient(context);

            GameMgtClient server = new GameMgtClient(context);
            mgtClient.Join("MariV", "mariV");

            server.SendBoard("MariV", 20, "All");
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.SendBoardConfigurate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()), Times.AtLeastOnce());
        }

        [TestMethod]

        public void TestSendScoreRival()
        {
            Mock<IGameMgtCallback> mockCallback = new Mock<IGameMgtCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            PlayerMgtClient mgtClient = new PlayerMgtClient(context);

            GameMgtClient server = new GameMgtClient(context);
            mgtClient.Join("MariV", "mariV");
            server.SendScoreRival("MariV", 12);
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.ReceiveScoreRival(It.IsAny<int>()), Times.AtLeastOnce());
            mgtClient.DisconnectUser("MariV");
        }

        [TestMethod]

        public void TestSendNumberCardsFinded()
        {
            Mock<IGameMgtCallback> mockCallback = new Mock<IGameMgtCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            PlayerMgtClient mgtClient = new PlayerMgtClient(context);

            GameMgtClient server = new GameMgtClient(context);
            mgtClient.Join("MariV", "mariV");
            server.SendNumberCardsFinded("MariV", 12);
            Thread.Sleep(1000);
            mockCallback.Verify(mock => mock.ReceiveNumberCardsFinded(It.IsAny<int>()), Times.AtLeastOnce());
            mgtClient.DisconnectUser("MariV");
        }

        [TestMethod]

        public void TestSendWinner()
        {
            Mock<IGameMgtCallback> mockCallback = new Mock<IGameMgtCallback>() { CallBase = true };
            InstanceContext context = new InstanceContext(mockCallback.Object);
            PlayerMgtClient mgtClient = new PlayerMgtClient(context);

            GameMgtClient server = new GameMgtClient(context);
            mgtClient.Join("MariV", "mariV");
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
            emailsExpected.Add("Kari11");
            emailsExpected.Add("mariV@gmail.com");
            emailsExpected.Add("angelicaiglesiase@hotmail.com");
            emailsExpected.Add("angelicaiglesiase@hotmail.com");
            server.GetEmails();
            int valueExpected =4 ;
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
            server.Join("MariV", "mariV");
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
            usersExpected.Add("kari11 @gmail.com");
            usersExpected.Add("MariV");
            usersExpected.Add("angy");
            usersExpected.Add("egy");
            int valueExpected = 4;
            int valueReceived = 0;

            foreach (var user in server.GetUsers())
            {
                Console.WriteLine(user);

                if (!usersExpected.Contains(user))
                {
                    valueReceived++;
                }
            }

            Assert.AreEqual(valueExpected, valueReceived);

        }


    }
}
