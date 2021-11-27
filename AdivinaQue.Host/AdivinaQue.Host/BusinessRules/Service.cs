using AdivinaQue.Host.DatabaseAccess;
using AdivinaQue.Host.Exception;
using AdivinaQue.Host.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Net;
using System.Net.Mail;
using System.ServiceModel;
using System.Web.UI.WebControls;
using System.Windows.Media.Imaging;
using System.Configuration;

namespace AdivinaQue.Host.BusinessRules
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]

    public class Service : IService
    {
        public Dictionary<string, IClient> users = new Dictionary<String, IClient>();
        public List<string> currentlyUserPlayed = new List<string>();
     
        
        public void DisconnectUser(String username)
        {
            users.Remove(username);
            Console.WriteLine("Usuario {0} se desconecto", username);
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
            AuthenticationStatus status = authentication.UpdatePlayer(player, username);
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
            if (users.ContainsKey(newUsername))
            {
                value = true;
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
            string from = ConfigurationManager.AppSettings["EmailAdmin"];
            string smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            string passwordAdmin = ConfigurationManager.AppSettings["PasswordAdmin"];
            string displayName = "Administrador de Adivina ¿Qué? Memorama";
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);
                mail.To.Add(to);

                mail.Subject = asunto;
                mail.Body = body;
                mail.IsBodyHtml = true;


                SmtpClient client = new SmtpClient(smtpServer, port);
                client.Credentials = new NetworkCredential(from, passwordAdmin);
                client.EnableSsl = true;


                client.Send(mail);
                message = "Exito";

            }
            catch (SmtpException ex)
            {
                message = "Error";
                throw new BusinessException("Error send Mail", ex);

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

  


        public List<String> GetEmails()
        {
            Authentication authentication = new Authentication();
            List<String> emails = authentication.GetEmails();
            return emails;
        }

        public List<String> GetUsers()
        {
            Authentication authentication = new Authentication();
            List<String> users = authentication.GetUsers();
            return users;
        }

        public void SendRival(string rival, string fromUsername)
        {
            users[fromUsername].ReceiveRival(rival);
        }

        public void SendBoardLists(string toUsername, List<int> randomImageList, List<int> randomPositionList)
        {
            users[toUsername].ReceiveCardSeed(randomImageList, randomPositionList);
        }

        public void SendCorrectCards(string toUsername, Dictionary<BitmapImage, string> cards)
        {
            users[toUsername].ReceiveCorrectPair(cards);
        }

        public void SendScoreRival(string toUsername, int score)
        {
            users[toUsername].ReceiveScoreRival(score);
        }

        public void SendNextTurnRival(string toUsername, bool nextTurn)
        {
            users[toUsername].ReceiveNextTurn(nextTurn);
        }

        public void SendNumberCardsFinded(string toUsername, int numberCardsFinded)
        {
            users[toUsername].ReceiveNumberCardsFinded(numberCardsFinded);
        }

        public bool SendGame(GameCurrently gameCurrently)
        {
            bool value = false;
            Authentication authentication = new Authentication();

                if (authentication.AddGame(gameCurrently) == AuthenticationStatus.Success)
                {
                    value = true;
                }
            
           


            return value;
            
        }

        public void SendWinner(string toUsername, string winner)
        {
            users[toUsername].ReceiveWinner(winner);
        }

        public string GetEmailByUser(string username)
        {
            Authentication authentication = new Authentication();

            return authentication.GetEmail(username);
        }

        public bool ChangePassword(string username, string newPassword)
        {
            Authentication authentication = new Authentication();
            bool value = false;

            if (authentication.UpdatePassword(username, newPassword) == AuthenticationStatus.Success)
            {
                value = true;
            }
            return value;
        }

        public bool FindUsername(string username)
        {
            bool value = false;
            Authentication authentication = new Authentication();
            List<string> usersRegister = authentication.GetUsers();
            if (usersRegister.Contains(username)) {
                value = true;
            } 

            return value;
        }

       

        public void SendCardTurn(string toUsername, BitmapImage image, string name)
        {
            users[toUsername].ReceiveCardTurn(image, name);
        }
    }
}
