using AdivinaQue.Client.Control;
using AdivinaQue.Client.Proxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ServiceModel;

namespace ClientTest
{
    [TestClass]
    public class UnitTest1
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
        public void TestJoin()
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            ServiceClient server = new ServiceClient(context);
            Assert.IsTrue(server.Join("Mari", "123"));
        }

   
    }
}
