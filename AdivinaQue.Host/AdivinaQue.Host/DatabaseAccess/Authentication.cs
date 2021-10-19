using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

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
        public void Register(string username, string password, string name, string email )
        {
            AdivinaQueAppContext AdivinaQueAppContext = new AdivinaQueAppContext();
            string passwordHashed = ComputeSHA256Hash(password);
            AdivinaQueAppContext.Players.Add(new Players() { name = name, userName = username, email= email, password=passwordHashed });
            AdivinaQueAppContext.SaveChanges();
           
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
