using AdivinaQue.Client.Control;
using AdivinaQue.Client.Proxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ServiceModel;

namespace ClientTests
{
    [TestClass]
    public class UnitTest1
    {
        private static ServiceHost serviceHost;

       // [ClassInitialize]
      /*  public static void InitializeClass(TestContext context)
        {
            serviceHost = new ServiceHost(typeof(AdivinaQue.Client.Control.CallBack));
            serviceHost.Open();
        }*/
        [TestMethod]
        public void testRegister()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            AdivinaQue.Client.Proxy.ServiceClient server = new AdivinaQue.Client.Proxy.ServiceClient(context);
            Player player = new Player();
            player.Username = "pepePicas";
            player.Name = "Pedro Josue Sanchez";
            player.Password = "picoDeHielo123";
            player.Email = "pepe@outlook.com";

            Assert.IsTrue(server.register(player));
            
        }

       
    }
}
