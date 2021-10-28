using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HostTest
{
    public class Tests
    {

        private static ServiceHost serviceHost;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            serviceHost = new ServiceHost(typeof(CalculatorService.CalculatorService));
            serviceHost.Open();
        }
    }
}