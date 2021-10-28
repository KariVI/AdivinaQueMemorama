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
        [ClassCleanup]
        public static void CleanupClass()
        {
            serviceHost.Close();
        }

        [TestMethod]
        public void TestServiceJoin()
        {
          





        }
    }
}
