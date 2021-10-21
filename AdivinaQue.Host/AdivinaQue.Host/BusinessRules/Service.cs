using AdivinaQue.Host.DatabaseAccess;
using AdivinaQue.Host.InterfaceContract;
using AdivinaQue.Host.Logs;
using log4net;
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
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]

    public class Service: IService
    {
        private static readonly ILog Logs = Log.GetLogger();
        public Dictionary<string, IClient> users = new Dictionary<String, IClient>();

        public void disconnectUser(String username)
        {
            users.Remove(username);
            getConnectedUsers();
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
            AuthenticationStatus status = authentication.loginSuccesful(username, password);
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

        public bool searchUsername(string newUsername)
        {
            bool value = false;
            if (users[newUsername] != null)
            {
                value = true;
            }
            return value;
        }

        public string sendMail(string to, string asunto, string body)
        {
            string message = "Error al enviar este correo. Por favor verifique los datos o intente más tarde.";
            string from = "angelicaiglesiase@hotmail.com";
            string displayName = "Administrador de Adivina ¿Qué? Memorama";
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);
                mail.To.Add(to);

                mail.Subject = asunto;
                mail.Body = body;
                mail.IsBodyHtml = true;


                SmtpClient client = new SmtpClient("smtp.office365.com", 587); //Aquí debes sustituir tu servidor SMTP y el puerto
                client.Credentials = new NetworkCredential(from, "karina");
                client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false


                client.Send(mail);
                message = "Exito";

            }
            catch (SmtpException ex)
            {
                message= "Error";
                Logs.Error($"Fallo la conexión ({ ex.Message})");

            }

            return message;
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
