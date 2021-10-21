using AdivinaQue.Host.BusinessRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ServiceModel;

namespace AdivinaQue.Host.Pruebas
{
    [TestClass]
    public class ServiceTest
    {
        ServiceHost host = new ServiceHost(typeof(Service));


    }
}
