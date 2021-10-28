using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using AdivinaQue.Host.InterfaceContract;

namespace AdivinaQue.Host.DatabaseAccess
{
    public class Authentication
    {

        public Authentication()
        {
        }

        public AuthenticationStatus Login(string userName, string password)
        {
            AuthenticationStatus status = AuthenticationStatus.Failed;
            string passwordHashed = ComputeSHA256Hash(password);
            using (var context = new AdivinaQueAppContext())
            {
                var Players = (from account in context.Players
                                where account.userName == userName && account.password == passwordHashed
                                select account).Count();

                if (Players > 0)
                {

                    status = AuthenticationStatus.Success;
                }
            }
            return status;
        }
        public Player RetrievePlayer(string username)
        {
            using (var context = new AdivinaQueAppContext())
            {
                var player = context.Players.SingleOrDefault(x => x.userName == username);
                Player newPlayer = new Player { Name = player.name, Username = player.userName, Password = player.password, Email = player.email};
                return newPlayer;
            }
        
        }
        public void updatePlayer(Player player,String username)
        {
            string passwordHashed = ComputeSHA256Hash(player.Password);

            using (var context = new AdivinaQueAppContext())
            {
                var Players = ( from account in context.Players
                               where account.userName == username
                               select account);
                Players.First().password = passwordHashed;
                Players.First().name = player.Name;
                Players.First().userName = player.Username;
                Players.First().email = player.Email;
               context.SaveChanges();
            }

        }
        public void Register(string username, string password, string name, string email )
        {
            AdivinaQueAppContext AdivinaQueAppContext = new AdivinaQueAppContext();
            string passwordHashed = ComputeSHA256Hash(password);
            
            AdivinaQueAppContext.Players.Add(new Players() { name = name, userName = username, email= email, password=passwordHashed });
            AdivinaQueAppContext.SaveChanges();
        }

        public void Delete(string username)
        {
            using (var context = new AdivinaQueAppContext())
            {
                var itemToRemove = context.Players.SingleOrDefault(x => x.userName == username); 

                if (itemToRemove != null)
                {
                    context.Players.Remove(itemToRemove);
                    context.SaveChanges();
                }
            }
        }
        public string GenerateCode()
        {
            var posibleCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var code = new char[8];
            var random = new Random();

            for (int i = 0; i < code.Length; i++)
            {
                code[i] = posibleCharacters[random.Next(posibleCharacters.Length)];
            }
            string codeString = new String(code);
            return codeString;
        }
        public void sendMail(string email, string newMessage)
        {
            string userMail = "AdivinaQueTeam@hotmail.com";
            string password = "MarianaKarina1234";
            string subject = "Register Memorama game";
            string sendMail = email;
            string message = newMessage;
            
            
            MailMessage mail = new MailMessage(userMail, sendMail, subject, message);

            SmtpClient server = new SmtpClient("smtp.live.com");
            server.Port = 587;
            NetworkCredential credential = new NetworkCredential(userMail, password);
            server.Credentials = credential;
            server.EnableSsl = true;
            try
            {
                server.Send(mail);
                Console.WriteLine("\t\tCorreo enviado de manera exitosa");
                mail.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        private string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
              
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }

    public enum AuthenticationStatus
    {
        Success = 0,
        Failed
    }

}
