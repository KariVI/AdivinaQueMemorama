using AdivinaQue.Host.BusinessRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ServiceModel;

namespace HostTests
{
    [TestClass]
    public class UnitTest1
    {
        private static ServiceHost serviceHost;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            serviceHost = new ServiceHost(typeof(AdivinaQue.Host.BusinessRules.Service));
            serviceHost.Open();
        }
        [TestMethod]
        public void testRegister()
        {
            Player player = new Player();
            player.Username = "pepePicas";
            player.Name = "Pedro Josue Sanchez";
            player.Password = "picoDeHielo123";
            player.Email = "pepe@outlook.com";
            s

        }
    }
}
