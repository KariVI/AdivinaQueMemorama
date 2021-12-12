using AdivinaQue.Host.DatabaseAccess;
using AdivinaQue.Host.InterfaceContract;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HostUnitTest
{
    [TestClass]
    public class UnitTest1
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

            Assert.AreEqual(authenticationStatus, authentication.RegisterSucessful(player));
        }
    }
}
