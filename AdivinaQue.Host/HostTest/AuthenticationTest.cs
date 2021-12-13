using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdivinaQue.Host.DatabaseAccess;
using AdivinaQue.Host.InterfaceContract;

namespace HostTest
{
    [TestClass]
    public class AuthenticationTest
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

            NUnit.Framework.Assert.AreEqual(authenticationStatus, authentication.RegisterSucessful(player));
        }
    }
}