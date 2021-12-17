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
using log4net;
using AdivinaQue.Host.Logs;
using System.Configuration;

namespace AdivinaQue.Host.DatabaseAccess
{
    /// <summary>
    /// Lógica para la administración de la base de datos 
    /// </summary>
    public class Authentication
    {
        public List<int> ListScores { get; set; }
        public List<string> ListPlayers { get; set; }
        private static readonly ILog Logs = Log.GetLogger();
        public Authentication()
        {
        }

        /// <summary>
        /// Buscar si un usuario se encuentra registrado para el inicio de sesión  
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns> Sucess si se encuentra  y Failed en caso contrario </returns>

        public AuthenticationStatus LoginSuccesful(string userName, string password)
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

        /// <summary>
        /// Recuperar la lista de Emails registrados en el sistema 
        /// </summary>
        /// <returns> Una lista de strings con los emails </returns>
        public List<String> GetEmails()
        {

            using (var context = new AdivinaQueAppContext())
            {

                var query = from Players in context.Players
                            select Players.email;


                return query.ToList<String>();
            }
        }
        /// <summary>
        /// Recuperar la lista de Usuarios  registrados en el sistema 
        /// </summary>
        /// <returns> Una lista de strings con los usuarios </returns>
        public List<String> GetUsers()
        {

            using (var context = new AdivinaQueAppContext())
            {

                var query = from Players in context.Players
                            select Players.userName;


                return query.ToList<String>();
            }
        }
        /// <summary>
        /// Recuperar un jugador por medio del nombre de usuario 
        /// </summary>
        ///<param name="username"></param>
       /// <returns> Un jugador  </returns>
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
        /// <summary>
        /// Actualizar la información de un jugador 
        /// </summary>
        ///<param name="player"> Nueva información del jugador</param>
        ///<param name="username"></param>
        /// <returns> Sucess si se pudo actualizar la información  y Failed en caso contrario  </returns>
        public AuthenticationStatus UpdateSucessful(Player player,String username)
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
                throw new BusinessException("Failed Update", ex);
                
            }
            return status;

        }

        /// <summary>
        /// Actualizar la contraseña de un jugador 
        /// </summary>
        ///<param name="username"></param>
       ///<param name="password"> </param>
        /// <returns> Sucess si se pudo actualizar la  contraseña  y Failed en caso contrario  </returns>

        public AuthenticationStatus UpdateSucessfulPassword( String username, String password)
        {
            string passwordHashed = ComputeSHA256Hash(password);
            AuthenticationStatus status = AuthenticationStatus.Success;
            try
            {
                using (var context = new AdivinaQueAppContext())
                {
                    var Players = (from account in context.Players
                                   where account.userName == username
                                   select account);
                    Players.First().password = passwordHashed;
                    context.SaveChanges();
                }
            }
            catch (EntityException ex)
            {
                status = AuthenticationStatus.Failed;
                                throw new BusinessException("Failed Update", ex);

            }
            return status;

        }

        /// <summary>
        /// Registrar un nuevo jugador 
        /// </summary>
        /// ///<param name="player"> Nuevo jugador</param>
        /// <returns> Sucess si se pudo registrar el jugador  y Failed en caso contrario  </returns>

        public AuthenticationStatus RegisterSucessful(Player player)
        {
            AdivinaQueAppContext AdivinaQueAppContext = new AdivinaQueAppContext();
            string passwordHashed = ComputeSHA256Hash(player.Password);
            AuthenticationStatus status = AuthenticationStatus.Success;
            try
            {
                AdivinaQueAppContext.Players.Add(new Players() { name = player.Name, userName = player.Username, email = player.Email, password = passwordHashed });              
                AdivinaQueAppContext.SaveChanges();
                AddPodio(player.Username);
            }
            catch (EntityException ex)
            {
                status = AuthenticationStatus.Failed;
                Logs.Error($"Fallo la conexión ({ ex.Message})");
                
            }
            return status;

        }

        /// <summary>
        /// Eliminar un  jugador 
        /// </summary>
        ///<param name="username"> </param>
        /// <returns> Sucess si se pudo eliminar al  jugador  y Failed en caso contrario  </returns>
        public AuthenticationStatus DeleteSucessful(string username)
        {
            AuthenticationStatus status;
            using (var context = new AdivinaQueAppContext())
            {
                var playerToRemove = context.Players.SingleOrDefault(x => x.userName == username);
               

                status = AuthenticationStatus.Failed;
                if (playerToRemove != null)
                {
                    var scoreToRemove = context.Score.SingleOrDefault(x => x.IdPlayer == playerToRemove.Id);
                    context.Players.Remove(playerToRemove);
                    context.Score.Remove(scoreToRemove);
                    context.SaveChanges();
                    status = AuthenticationStatus.Success;
                }
            }
            return status;
        }
        /// <summary>
        /// Generar un código 
        /// </summary> 
        /// <returns> Una cadena con el código generado  </returns>

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

        /// <summary>
        /// Enviar un correo para mencionarle a alguien que se registre al juego
        /// </summary>
        ///<param name="email"> </param>
        ///<param name="newMessage"> </param>
        /// <returns> Sucess si se pudo enviar el correo y Failed en caso contrario  </returns>

        public AuthenticationStatus SendMailSucessful(string email, string newMessage)
        {
           
            string userMail = ConfigurationManager.AppSettings["EmailAdmin"];
            string password = ConfigurationManager.AppSettings["PasswordAdmin"];
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
                Logs.Error($"Fallo la conexión ({ ex.Message})");

            }
            return status;
        }

        /// <summary>
        /// Hashear una cadena
        /// </summary>
        ///<param name="input"> </param>
        /// <returns>Cadena hasheada   </returns>

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

        /// <summary>
        /// Obtener los puntajes de los jugadores registrados
        /// </summary>
        /// <returns>Regresa una lista de cadena con los puntajes de los jugadores  </returns>

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

        /// <summary>
        /// Añadir un nuevo juego finalizado 
        /// </summary>
        ///<param name="gameCurrently"> </param>
        /// <returns> Sucess si se pudo añadir el juego y Failed en caso contrario  </returns>


        public AuthenticationStatus AddGameSucessful(GameCurrently gameCurrently)
        {
            AdivinaQueAppContext AdivinaQueAppContext = new AdivinaQueAppContext();
            AuthenticationStatus status = AuthenticationStatus.Success;
            try
            {
                if (!gameCurrently.Winner.Equals("both"))
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

        /// <summary>
        /// Buscar la ID de un jugador por medio de nombre del usuario 
        /// </summary>
        ///<param name="username"> </param>
        /// <returns> La ID del usuario  </returns>

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

        /// <summary>
        /// Buscar email de un jugador por medio de nombre del usuario 
        /// </summary>
        ///<param name="username"> </param>
        /// <returns> El email de usuario y si no existe regresa cadena vacia  </returns>

        public string GetEmail(string username)
        {
            string email = "";
            using (var context = new AdivinaQueAppContext())
            {
                var query = (from Players in context.Players
                             where Players.userName == username
                             select Players.email).First();
                email = query;
            }


            return email;
        }


        /// <summary>
        /// Añadir un jugador al podio o conteo de puntajes 
        /// </summary>
        ///<param name="username"> </param>
        private void AddPodio(string username) {
            AdivinaQueAppContext AdivinaQueAppContext = new AdivinaQueAppContext();
            int idUsername = GetIdUser(username);

            AdivinaQueAppContext.Score.Add(new Score() {IdPlayer= idUsername, totalGames=0 });
            AdivinaQueAppContext.SaveChanges();
        }

        /// <summary>
        /// Añadir una victoria de un juego terminado
        /// </summary>
        ///<param name="gameCurrently"> </param>

        private void AddVictory(GameCurrently gameCurrently)
        {

            if (gameCurrently.Winner.Equals("both"))
            {

                foreach (var player in  gameCurrently.Players.Select(player => player.Key))
                {
                    using (var context = new AdivinaQueAppContext())
                    {
                        var idWinner = GetIdUser(player);
                        var oldScore = (from account in context.Score
                                     where account.IdPlayer == idWinner
                                     select account.totalGames).First();

                        int newScore = (int)(oldScore + 1);
                        int idPlayer = GetIdUser(player);

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
                    int id = GetIdUser(gameCurrently.Winner);
                    var Score = (from account in context.Score
                                 where account.IdPlayer == id
                                 select account);
                    Score.First().totalGames = newScore;

                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Buscar la ID de un juego por medio de la información del juego 
        /// </summary>
        ///<param name="gameCurrently"> </param>
        /// <returns> La ID de un juego  </returns>

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

        /// <summary>
        /// Añadir participantes de un juego finalizado
        /// </summary>
        ///<param name="gameCurrently"> </param>
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
