using AdivinaQue.Host.DatabaseAccess;
using AdivinaQue.Host.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaQue.Host.BusinessRules
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]

    public class Service: IService
    {
        public Dictionary<string, IClient> users = new Dictionary<String, IClient>();



        public void disconnectUser(String username)
        {
            users.Remove(username);
            getConnectedUsers();
        }
        public void delete(string username)
        {
            Authentication authentication = new Authentication();
            authentication.Delete(username);
        }

        public void getConnectedUsers()
        {
            foreach (var other in users.Values)
            {
                other.RecieveUsers(users);
            }
        }

        public bool join(string username, string password)
        {
            Authentication authentication = new Authentication();
            AuthenticationStatus status = authentication.Login(username, password);
            Boolean value = true;
            if (status == AuthenticationStatus.Success)
            {
                var connection = OperationContext.Current.GetCallbackChannel<IClient>();
                users[username] = connection;
                Console.WriteLine("Usuario {0} se conecto", username);

            }
            else
            {
                value = false;
                Console.WriteLine("No se conecto");

            }
            return value;
        }

        public void register(string username, string password, string name, string email)
        {
            Authentication authentication = new Authentication();
            authentication.Register(username, password, name, email);
        }
        public void modify(Player player, String username)
        {
            Authentication authentication = new Authentication();
            authentication.updatePlayer(player,  username);
        }
       
        public string sendMail(string email)
        {
            Authentication authentication = new Authentication();
            var code = authentication.sendMail(email);
            return code;
        }
        public void searchInfoPlayerByUsername(String username)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IClient>();
            Authentication authentication = new Authentication();
            Player player = authentication.RetrievePlayer(username);
            connection.RecievePlayer(player);
        }

        public bool searchUsername(string newUsername)
        {
            bool value = false;
            if (users[newUsername] != null)
            {
                value = true;
            }
            return value;
        }

        public void sendMessage(string message, String username, string userReceptor)
        {
            if (userReceptor.Equals("Todos"))
            {
                foreach (var other in users.Values)
                {
                    Console.WriteLine("{0} : {1}", username, message);
                    String text = username + ": " + message;
                    other.RecieveMessage(text);
                }
            }
            else
            {
                Console.WriteLine("[Privado] {0} : {1}", username, message);
                String text = "[Mensaje privado]" + username + ": " + message;
                users[userReceptor].RecieveMessage(text);

            }
        }

    }
}
