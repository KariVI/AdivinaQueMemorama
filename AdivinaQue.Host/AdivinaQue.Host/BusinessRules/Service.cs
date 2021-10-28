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



        public void DisconnectUser(String username)
        {
            users.Remove(username);
            GetConnectedUsers();
        }
        public void Delete(string username)
        {
            Authentication authentication = new Authentication();
            authentication.Delete(username);
        }

        public void GetConnectedUsers()
        {
            foreach (var other in users.Values)
            {
                other.RecieveUsers(users);
            }
        }

        public bool Join(string username, string password)
        {
            Authentication authentication = new Authentication();
            AuthenticationStatus status = authentication.Login(username, password);
            Boolean value = false;
            if (status == AuthenticationStatus.Success)
            {
                var connection = OperationContext.Current.GetCallbackChannel<IClient>();
                users[username] = connection;
                Console.WriteLine("Usuario {0} se conecto", username);
                value = true;
            }
            return value;
        }

        public void Register(string username, string password, string name, string email)
        {
            Authentication authentication = new Authentication();
            authentication.Register(username, password, name, email);
        }
        public void Modify(Player player, String username)
        {
            Authentication authentication = new Authentication();
            authentication.updatePlayer(player,  username);
        }
       
        public string SendMailValidation(string email)
        {
            Authentication authentication = new Authentication();
            var code = authentication.GenerateCode();
            string message = "Ingrese el codigo en la aplicacion: " + code;
            authentication.sendMail(email,message);
            return code;
        }

        public void SendMailInvitation(string email)
        {
            Authentication authentication = new Authentication();
            string message = "Lo han invitado a jugar Adivina Que! Instale el juego AQUI";
            authentication.sendMail(email, message);
        }

        public void SearchInfoPlayerByUsername(String username)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IClient>();
            Authentication authentication = new Authentication();
            Player player = authentication.RetrievePlayer(username);
            connection.RecievePlayer(player);
        }

        public bool SearchUsername(string newUsername)
        {
            bool value = false;
            if (users[newUsername] != null)
            {
                value = true;
            }
            return value;
        }

        public void SendMessage(string message, String username, string userReceptor)
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
        public bool SendInvitation(String toUsername, String fromUsername)
        {
           var result = users[toUsername].SendInvitationGame(fromUsername);
           return result;
        }
    }
}
