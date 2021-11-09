using AdivinaQue.Host.DatabaseAccess;
using AdivinaQue.Host.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaQue.Host.BusinessRules
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]

    public class Service : IService
    {
        public Dictionary<string, IClient> users = new Dictionary<String, IClient>();

        public void GetTopics(string username)
        {
            Authentication authentication = new Authentication();
            List<String> topics = authentication.getTopics();


            users[username].RecieveTopics(topics);
        }
      

        public List<String> GetEmails(){
          Authentication authentication = new Authentication();
            List<String> emails = authentication.getEmails();
            return emails;
          }

        public List<String> GetUsers()
        {
            Authentication authentication = new Authentication();
            List<String> users = authentication.getUsers();
            return users;
        }
        public void DisconnectUser(String username)
        {
            users.Remove(username);
            GetConnectedUsers();
        }
        public bool Delete(string username)
        {
            Authentication authentication = new Authentication();
            AuthenticationStatus status = authentication.Delete(username);
            bool value = false;
            if (status == AuthenticationStatus.Success)
            {
                value = true;
            }
            return value;
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

        public Boolean Register(Player player)
        {
            Authentication authentication = new Authentication();
            AuthenticationStatus status = authentication.Register(player);
            Boolean value = false;

            if (status == AuthenticationStatus.Success)
            {
                value = true;
            }
            return value;
        }

        public bool Modify(Player player, String username)
        {
            Authentication authentication = new Authentication();
            AuthenticationStatus status = authentication.updatePlayer(player, username);
            bool value = true;
            if(status == AuthenticationStatus.Failed)
            {
                value = false;
            }
            return value;
        }

        public string GenerateCode()
        {
            Authentication authentication = new Authentication();
            var code = authentication.GenerateCode();
            return code;
        }

        public void SearchInfoPlayerByUsername(String username)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IClient>();
            Authentication authentication = new Authentication();
            Player player = authentication.RetrievePlayer(username);
            if(player != null)
            {
                connection.RecievePlayer(player);

            }
        }

        public bool SearchUsername(string newUsername)
        {
            bool value = false;
            try
            {
                if (users[newUsername] != null)
                {
                    value = true;
                }  
            }catch(KeyNotFoundException ex) { 
       
            }
            return value;
        }

        public void SendMessage(String message, String username, String userReceptor)
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

        public void SendBoard(String toUsername, int size, string category)
        {
             users[toUsername].SendBoardConfigurate(toUsername,size,category);
        }
        public string SendMail(string to, string asunto, string body)
        {
            string message = "Error al enviar este correo. Por favor verifique los datos o intente más tarde.";
            string from = "adivinaQueTeam@hotmail.com";
            string displayName = "Administrador de Adivina ¿Qué? Memorama";
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);
                mail.To.Add(to);

                mail.Subject = asunto;
                mail.Body = body;
                mail.IsBodyHtml = true;


                SmtpClient client = new SmtpClient("smtp.office365.com", 587);
                client.Credentials = new NetworkCredential(from, "MarianaKarina1234");
                client.EnableSsl = true;


                client.Send(mail);
                message = "Exito";

            }
            catch (SmtpException ex)
            {
                message = "Error";

            }
            return message;
        }
        public void GetScores(String username)
        {
            Authentication authentication = new Authentication();
            List<GlobalScore> scores = authentication.GetPlayers();
            Dictionary<String, int> globalScores = new Dictionary<string, int>();

            foreach (var player in scores)
            {
                globalScores.Add(player.username, player.score);
            }

            users[username].RecieveScores(globalScores);


        }

    
    }
}
