using AdivinaQue.Host.DatabaseAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdivinaQue.Host.Pruebas
{
    [TestClass]
    public class AuthenticationTest
    {
        [TestMethod]
        public void testGetPlayers()
        {
            Authentication authetication = new Authentication();
            authetication.getPlayers();
            Assert.IsNotNull(authetication.ListScores, "No se recuperaron los datos correctamente");
        }
        
        [TestMethod]
        public void testLogin() {
            Authentication authentication = new Authentication();
            AuthenticationStatus status = authentication.loginSuccesful("kari11@gmail.com", "kari11");
            Assert.Equals(status, AuthenticationStatus.Success);
        }

    }
}
