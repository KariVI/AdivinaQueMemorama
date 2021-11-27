using AdivinaQue.Host.DatabaseAccess;
using AdivinaQue.Host.InterfaceContract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NUnit.Framework;

namespace HostUnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void testRegister()
        {
            Authentication authentication = new Authentication();
            Player player = new Player();
            player.Name = "Alonso Hernandez Hernandez";
            player.Username = "aloHer";
            player.Email = "alonso@outlook.com";
            player.Password = "Alonso12Hernandez";
            AuthenticationStatus authenticationStatus = AuthenticationStatus.Success;

             NUnit.Framework.Assert.IsTrue(authenticationStatus==authentication.Register(player));
        }
    }
}
