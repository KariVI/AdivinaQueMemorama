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

        [TestMethod]

        public void TestGetUsersConnected()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            PlayerMgtClient server = new PlayerMgtClient(context);
            List<string> usersExpected = new List<string>();
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

        /* private static ServiceHost serviceHost;
         [ClassInitialize]
         public static void InitializeClass(TestContext context)
         {
             serviceHost = new ServiceHost(typeof(CallBack));
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
             CallBack callback = new CallBack();
             InstanceContext context = new InstanceContext(callback);
             PlayerMgtClient server = new PlayerMgtClient(context);
             Assert.IsTrue(server.Join("MariV", "mariV"));
             server.DisconnectUser("MariV");
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
         public void TestReceiveMessage()
         {
             Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
             InstanceContext context = new InstanceContext(mockCallback.Object);
             PlayerMgtClient server = new PlayerMgtClient(context);
             server.Join("MariV", "mariV");

             server.SendMessage("Hola", "MariV", "Todos");
             mockCallback.Verify(mock => mock.RecieveMessage(It.IsAny<string>()), Times.AtLeastOnce());
         }

         [TestMethod]
         public void TestGetScores()
         {
             Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
             InstanceContext context = new InstanceContext(mockCallback.Object);
             PlayerMgtClient server = new PlayerMgtClient(context);
             server.Join("MariV", "mariV");

             server.GetScores("MariV");
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
             mockCallback.Verify(mock => mock.RecieveUsers(It.IsAny<Dictionary<string,object>>()), Times.AtLeastOnce());
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
             mockCallback.Verify(mock => mock.SendBoardConfigurate(It.IsAny<string>(), It.IsAny < int>(), It.IsAny < string>()), Times.AtLeastOnce());
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
             CallBack callback = new CallBack();
             InstanceContext context = new InstanceContext(callback);
             PlayerMgtClient server = new PlayerMgtClient(context);
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
             CallBack callback = new CallBack();
             InstanceContext context = new InstanceContext(callback);
             PlayerMgtClient server = new PlayerMgtClient(context);
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
             PlayerMgtClient server = new PlayerMgtClient(context);
             Assert.IsTrue(server.Delete("kari11@gmail.com"));
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
         public void TextSearchUsername()
         {
             CallBack callback = new CallBack();
             InstanceContext context = new InstanceContext(callback);
             PlayerMgtClient server = new PlayerMgtClient(context);
             server.Join("Mari", "1234");
             Assert.IsTrue(server.SearchUsername("Mari"));
             server.DisconnectUser("Mari");
         }
         [TestMethod]
         public void TextSearchUsernameFailed()
         {
             CallBack callback = new CallBack();
             InstanceContext context = new InstanceContext(callback);
             PlayerMgtClient server = new PlayerMgtClient(context);
             Assert.IsFalse(server.SearchUsername("Roberto"));
         }

         [TestMethod]
         public void TestSearchInfoPlayerByUsername()
         {
             Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
             InstanceContext context = new InstanceContext(mockCallback.Object);
             PlayerMgtClient server = new PlayerMgtClient(context);
             server.SearchInfoPlayerByUsername("Mari");
             Thread.Sleep(1000);
             mockCallback.Verify(mock => mock.RecievePlayer(It.IsAny<Player>()), Times.AtLeastOnce());

         }

         [TestMethod]
         public void TestSearchInfoPlayerByUsernameFailed()
         {
             Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
             //mockCallback.Setup(mock => mock.RecieveMessage(It.IsAny<String>()));
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
             player.Username = "Mari";
             player.Password = "1234";
             player.Name = "Mariana Yazmin Vargas";
             player.Email = "MarianaVSYazmin@gmail.com";
             Assert.IsTrue(server.Modify(player, "Mari"));
         }

         [TestMethod]
         public void TestSendInvitation()
         {
             Mock<IPlayerMgtCallback> mockCallback = new Mock<IPlayerMgtCallback>() { CallBase = true };
             InstanceContext context = new InstanceContext(mockCallback.Object);
             PlayerMgtClient server = new PlayerMgtClient(context);
             server.Join("Mari", "1234");
             server.Join("Kari", "123");
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
         }*/






    }
}
