using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AdivinaQue.Host.BusinessRules;
using System.Data.Entity.Core;
using log4net;
using AdivinaQue.Host.Logs;

namespace AdivinaQue.Host.DatabaseAccess
{
    public class Authentication
    {
       private List<int> listScores;
       private List<string> listPlayers;
       private static readonly ILog Logs = Log.GetLogger();



        public List<int> ListScores {  get{ return listScores; }  set { listScores = value; } }
       public List<string> ListPlayers { get { return listPlayers; } set { listPlayers = value; } }
        public Authentication()
        {
        }

        public AuthenticationStatus loginSuccesful(string userName, string password)
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
        public AuthenticationStatus Register(Player player)
        {
            AdivinaQueAppContext AdivinaQueAppContext = new AdivinaQueAppContext();
            string passwordHashed = ComputeSHA256Hash(player.Password);
            AuthenticationStatus status = AuthenticationStatus.Success;
            try
            {
                AdivinaQueAppContext.Players.Add(new Players() { name = player.Name, userName = player.Username, email = player.Email, password = passwordHashed });
                AdivinaQueAppContext.SaveChanges();
            }catch(EntityException ex)
            {
                status = AuthenticationStatus.Failed;
                Logs.Error($"Fallo la conexión ({ ex.Message})");

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

        public List<GlobalScore> getPlayers()
        {

            using (var context = new AdivinaQueAppContext()) {

                var query = from Players in context.Players
                            join
                            Score in context.Score on Players.Id equals Score.IdPlayer
                            select new GlobalScore { score = Score.totalGames, username= Players.userName  };

         
                return query.ToList();
            }
        }
    }

    public enum AuthenticationStatus
    {
        Success = 0,
        Failed
    }

}
