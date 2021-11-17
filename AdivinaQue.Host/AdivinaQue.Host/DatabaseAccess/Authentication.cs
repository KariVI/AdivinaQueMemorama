using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using AdivinaQue.Host.InterfaceContract;
using System.Data.Entity.Core;
using System.Collections.Generic;
using AdivinaQue.Host.Exception;

namespace AdivinaQue.Host.DatabaseAccess
{
    public class Authentication
    {
        private List<int> listScores;
        private List<string> listPlayers;
        public Authentication()
        {
        }
        public List<int> ListScores { get { return listScores; } set { listScores = value; } }
        public List<string> ListPlayers { get { return listPlayers; } set { listPlayers = value; } }
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
        public List<String> GetTopics()
        {

            using (var context = new AdivinaQueAppContext())
            {

                var query = from Cards in context.Card
                            select Cards.topic;


                return query.Distinct().ToList<String>();
            }
        }
        public List<String> GetEmails()
        {

            using (var context = new AdivinaQueAppContext())
            {

                var query = from Players in context.Players
                            select Players.email;


                return query.ToList<String>();
            }
        }

        public List<String> GetUsers()
        {

            using (var context = new AdivinaQueAppContext())
            {

                var query = from Players in context.Players
                            select Players.userName;


                return query.ToList<String>();
            }
        }
        public Player RetrievePlayer(string username)
        {
            using (var context = new AdivinaQueAppContext())
            {
                var player = context.Players.SingleOrDefault(x => x.userName == username);
                Player newPlayer = null;
                if (player != null)
                {
                   newPlayer = new Player { Name = player.name, Username = player.userName, Password = player.password, Email = player.email };
                }
               
                return newPlayer;
            }
        
        }
        public AuthenticationStatus UpdatePlayer(Player player,String username)
        {
            string passwordHashed = ComputeSHA256Hash(player.Password);
            AuthenticationStatus status = AuthenticationStatus.Success;
            try
            {
                using (var context = new AdivinaQueAppContext())
                {
                    var Players = (from account in context.Players
                                   where account.userName == username
                                   select account);
                    Players.First().password = passwordHashed;
                    Players.First().name = player.Name;
                    Players.First().userName = player.Username;
                    Players.First().email = player.Email;
                    context.SaveChanges();
                }
            }
            catch (EntityException ex)
            {
                status = AuthenticationStatus.Failed;
            }
            return status;

        }
        public AuthenticationStatus Register(Player player)
        {
            AdivinaQueAppContext AdivinaQueAppContext = new AdivinaQueAppContext();
            string passwordHashed = ComputeSHA256Hash(player.Password);
            AuthenticationStatus status = AuthenticationStatus.Success;
            try
            {
                AdivinaQueAppContext.Players.Add(new Players() { name = player.Name, userName = player.Username, email = player.Email, password = passwordHashed });
                AddPodio(player.Username);
                AdivinaQueAppContext.SaveChanges();
            }
            catch (EntityException ex)
            {
                status = AuthenticationStatus.Failed;
                throw new BusinessException("Failed Register", ex);
            }
            return status;

        }


        public AuthenticationStatus Delete(string username)
        {
            AuthenticationStatus status;
            using (var context = new AdivinaQueAppContext())
            {
                var itemToRemove = context.Players.SingleOrDefault(x => x.userName == username);
                status = AuthenticationStatus.Failed;
                if (itemToRemove != null)
                {
                    context.Players.Remove(itemToRemove);
                    context.SaveChanges();
                    status = AuthenticationStatus.Success;
                }
            }
            return status;
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
        public AuthenticationStatus SendMail(string email, string newMessage)
        {
            string userMail = "AdivinaQueTeam@hotmail.com";
            string password = "MarianaKarina1234";
            string subject = "Register Memorama game";
            string sendMail = email;
            string message = newMessage;

            AuthenticationStatus status = AuthenticationStatus.Failed;
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
                status = AuthenticationStatus.Success;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return status;
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
        public List<GlobalScore> GetPlayers()
        {

            using (var context = new AdivinaQueAppContext())
            {

                var query = from Players in context.Players
                            join
                            Score in context.Score on Players.Id equals Score.IdPlayer
                            orderby Score.totalGames descending
                            select new GlobalScore { score = (int)Score.totalGames, username = Players.userName };


                return query.ToList();
            }
        }


        public AuthenticationStatus AddGame(GameCurrently gameCurrently)
        {
            AdivinaQueAppContext AdivinaQueAppContext = new AdivinaQueAppContext();
            AuthenticationStatus status = AuthenticationStatus.Success;
            try
            {
                if (gameCurrently.Winner != "Both")
                {
                    AdivinaQueAppContext.Game.Add(new Game() { date = gameCurrently.Date, topic = gameCurrently.Topic, winner = GetIdUser(gameCurrently.Winner) });
                    AdivinaQueAppContext.SaveChanges();
                }
                else
                {
                    AdivinaQueAppContext.Game.Add(new Game() { date = gameCurrently.Date, topic = gameCurrently.Topic, winner = GetIdUser(gameCurrently.Players.First().Key) });
                    AdivinaQueAppContext.SaveChanges();
                }
                AddParticipateGame(gameCurrently);
                AddVictory(gameCurrently);
            }
            catch (EntityException ex)
            {
                status = AuthenticationStatus.Failed;
                throw new BusinessException("Failed Register", ex);
            }
            return status;

        }


        private int GetIdUser(string username)
        {
            int id = 0;
            using (var context = new AdivinaQueAppContext())
            {
                var query = (from Players in context.Players where Players.userName==username
                            select  Players.Id).First(); 
                id =query;
            }


            return id;
        }

        private void AddPodio(string username) {
            AdivinaQueAppContext AdivinaQueAppContext = new AdivinaQueAppContext();

            AdivinaQueAppContext.Score.Add(new Score() {IdPlayer= GetIdUser(username), totalGames=0 });
            AdivinaQueAppContext.SaveChanges();
        }

        private void AddVictory(GameCurrently gameCurrently)
        {
            AdivinaQueAppContext AdivinaQueAppContext = new AdivinaQueAppContext();

            if (gameCurrently.Winner.Equals("Both"))
            {

                foreach (var player in gameCurrently.Players)
                {
                    using (var context = new AdivinaQueAppContext())
                    {
                        var idWinner = GetIdUser(player.Key);
                        var oldScore = (from account in context.Score
                                     where account.IdPlayer == idWinner
                                     select account.totalGames).First();

                        int newScore = (int)(oldScore + 1);
                        int idPlayer = GetIdUser(player.Key);

                        var Score = (from account in context.Score
                                     where account.IdPlayer == idPlayer
                                     select account);
                        Score.First().totalGames = newScore;

                        context.SaveChanges();
                    }
                }
            }
            else
            {
                using (var context = new AdivinaQueAppContext())
                {
                    var idWinner = GetIdUser(gameCurrently.Winner);
                    var oldScore = (from account in context.Score
                                    where account.IdPlayer == idWinner
                                    select account.totalGames).First();

                    int newScore = (int)(oldScore + 1);

                    var Score = (from account in context.Score
                                 where account.IdPlayer == GetIdUser(gameCurrently.Winner)
                                 select account);
                    Score.First().totalGames = newScore;

                    context.SaveChanges();
                }
            }
        }

        public int GetIdGame(GameCurrently gameCurrently)
        {
            int id = 0;
            using (var context = new AdivinaQueAppContext())
            {

                try
                {
                    var query = (from Game in context.Game
                                 where Game.topic.Equals(gameCurrently.Topic)
                                 where Game.date.Equals(gameCurrently.Date)

                                 select Game.Id).First();
                    id = query;
                }
                catch (InvalidOperationException ex)
                {
                    throw new BusinessException("Invalid operation", ex);
                }
            }


            return id;
        }
        private void AddParticipateGame( GameCurrently gameCurrently)
        {
            AdivinaQueAppContext AdivinaQueAppContext = new AdivinaQueAppContext();
            int idGame = GetIdGame(gameCurrently);
            try
            {
                foreach(var player in gameCurrently.Players)
                {
                    int idPlayer = GetIdUser(player.Key);

                    AdivinaQueAppContext.Participate.Add(new Participate() { IdPlayer = idPlayer, score = player.Value, IdGame = idGame });
                    AdivinaQueAppContext.SaveChanges();


                }

            }
            catch (EntityException ex)
            {
                throw new BusinessException("Failed Register", ex);
            }

        }
    }

    public enum AuthenticationStatus
    {
        Success = 0,
        Failed
    }

}
