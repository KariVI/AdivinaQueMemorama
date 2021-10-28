using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AdivinaQue.Client;

namespace ClientTest
{
    [TestClass]
    public class ServiceClientTest
    {
        CallBack callback = new CallBack();
        InstanceContext context = new InstanceContext(callback);
        Proxy.ServiceClient server = new Proxy.ServiceClient(context);

        [TestMethod]
        public void testRegisterUsername()
        {

            Player player = new Player();
            player.Username = "pepePicas";
            player.Password = "PicoDeHielo1234";
            player.Name = "Pedro Josue Sanchez ";
            player.Email = "pepe@outlook.com";
           Assert.IsTrue( server.register(player));


        }
    }
}
