using AdivinaQue.Host.DatabaseAccess;
using AdivinaQue.Host.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.ServiceModel;
using System.Windows.Media.Imaging;

namespace AdivinaQue.Host.BusinessRules
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]

    public class Service : IPlayerMgt, IGameMgt
    {
        private Dictionary<string, IClient> users = new Dictionary<String, IClient>();

        private List<string>  currentlyUserPlayed = new List<string>();

        /// <summary>
        /// Desconectar a un jugador del servidor
        /// </summary>
        ///<param name="username"> </param>
        public void DisconnectUser(String username)
        {
            users.Remove(username);
            currentlyUserPlayed.Remove(username);
            Console.WriteLine("Usuario {0} se desconecto", username);
            GetConnectedUsers();
        }
        /// <summary>
        /// Eliminar a un jugador del juego
        /// </summary>
        ///<param name="username"> </param>
        ///<returns>True si fue posible eliminarlo y false si fue el caso contrario</returns>
        public bool Delete(string username)
        {
            Authentication authentication = new Authentication();
            AuthenticationStatus status = authentication.DeleteSucessful(username);
            bool value = false;
            if (status == AuthenticationStatus.Success)
            {
                value = true;
            }
            return value;
        }
        /// <summary>
        /// Obtener a los usuarios conectados 
        /// </summary>
        
        public void GetConnectedUsers()
        {
            foreach (var other in users.Values)
            {
                other.RecieveUsers(users);
            }
        }
        /// <summary>
        /// Obtener a los usuarios que estan jugando actualmente 
        /// </summary>
        public void GetCurrentlyUserPlayed()
        {
            foreach (var other in users.Values)
            {
                other.ReceiveUsersPlayed(currentlyUserPlayed);
            }
        }

        /// <summary>
        /// Ingresar al servidor 
        /// </summary>
        ///<param name="username"> </param>
        ///<param name="password"> </param>
        ///<returns>True si fue posible ingresar y false si fue el caso contrario</returns>
        public bool Join(string username, string password)
        {
            Authentication authentication = new Authentication();
            AuthenticationStatus status = authentication.LoginSuccesful(username, password);
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

        /// <summary>
        /// Registrar un jugador
        /// </summary>
        ///<param name="player"> </param>
        ///<returns>True si fue posible registrar al jugador y false si fue el caso contrario</returns>
        public Boolean Register(Player player)
        {
            Authentication authentication = new Authentication();
            AuthenticationStatus status = authentication.RegisterSucessful(player);
            Boolean value = false;

            if (status == AuthenticationStatus.Success)
            {
                value = true;
            }
            return value;
        }

        /// <summary>
        /// Actualizar la información de un jugador 
        /// </summary>
        ///<param name="player"> Nueva información del jugador</param>
        ///<param name="username"></param>
        /// <returns> True si se pudo actualizar la información  y False en caso contrario  </returns>
        public bool Modify(Player player, String username)
        {
            Authentication authentication = new Authentication();
            AuthenticationStatus status = authentication.UpdateSucessful(player, username);
            bool value = true;
            if(status == AuthenticationStatus.Failed)
            {
                value = false;
            }
            return value;
        }

        /// <summary>
        /// Generar un código 
        /// </summary> 
        /// <returns> Una cadena con el código generado  </returns>
        public string GenerateCode()
        {
            Authentication authentication = new Authentication();
            var code = authentication.GenerateCode();
            return code;
        }

        /// <summary>
        /// Buscar la información de un jugador por medio de su nombre de usuario 
        /// </summary> 
        ///<param name="username"></param>

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

        /// <summary>
        /// Buscar si existe el jugador en el servidor 
        /// </summary> 
        ///<param name="newUsername"></param>
        /// <returns> Una cadena con el código generado  </returns>

        public bool SearchUsername(string newUsername)
        {
            bool value = false;
            if (users.ContainsKey(newUsername))
            {
                value = true;
            }
            return value;
        }

        /// <summary>
        /// Enviar un mensaje a un jugador conectado
        /// </summary> 
        ///<param name="message"></param>
        /// <param name="username"></param>
        /// <param name="userReceptor"></param>

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

        /// <summary>
        /// Enviar una invitación a una nueva partida 
        /// </summary> 
        ///<param name="message"></param>
        /// <param name="username"></param>
        /// <param name="userReceptor"></param>
        public bool SendInvitation(String toUsername, String fromUsername)
        {
            var result = users[toUsername].SendInvitationGame(fromUsername);
            return result;
        }


        /// <summary>
        /// Mandar las especificaciones del tablero  al rival
        /// </summary> 
        /// <param name="toUsername"> Quien va recibir el tablero</param>
        /// <param name="size"></param>
        /// <param name="category"></param>
        public void SendBoard(String toUsername, int size, string category)
        {
            users[toUsername].SendBoardConfigurate(toUsername,size,category);
        }

        /// <summary>
        /// Mandar un correo de confirmaciób
        /// </summary> 
        /// <param name="to"> Quien va recibir el tablero</param>
        /// <param name="asunto"></param>
        /// <param name="body"></param>
        /// <returns> Mandar una cadena de exito o error al enviar el correo</returns>

        public string SendMail(string to, string asunto, string body)
        {
            string message = "";
            string from = ConfigurationManager.AppSettings["EmailAdmin"];
            string smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            string passwordAdmin = ConfigurationManager.AppSettings["PasswordAdmin"];
            string displayName = "Administrador de Adivina ¿Qué? Memorama";
           
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

            
            
            return message;
        }

        /// <summary>
        /// Mandar la lista de puntajes
         /// </summary> 
        /// <param name="username"> Quien va recibir los puntajes</param>

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
        /// <summary>
        /// Recuperar la lista de Emails registrados en el sistema 
        /// </summary>
        /// <returns> Una lista de strings con los emails </returns>

        public List<String> GetEmails()
        {
            Authentication authentication = new Authentication();
            List<String> emails = authentication.GetEmails();
            return emails;
        }

        /// <summary>
        /// Recuperar la lista de Usuarios  registrados en el sistema 
        /// </summary>
        /// <returns> Una lista de strings con los usuarios </returns>
        public List<String> GetUsers()
        {
            Authentication authentication = new Authentication();
            List<String> usersRegister = authentication.GetUsers();
            return usersRegister;
        }



        /// <summary>
        /// Mandar el rival al otro jugador
       /// </summary> 
        /// <param name="rival"> </param>
        /// <param name="fromUsername"> </param>
        public void SendRival(string rival, string fromUsername)
        {
            users[fromUsername].ReceiveRival(rival);
            
        }

        /// <summary>
        /// Agregar usuarios a la lista de conectados
        /// </summary> 
        /// <param name="rival"> </param>
        /// <param name="fromUsername"> </param>
        public void ConnectCurrentlyUsers(string rival, string fromUsername)
        {
            currentlyUserPlayed.Add(rival);
            currentlyUserPlayed.Add(fromUsername);
        }

        /// <summary>
        /// Devuelve true si el jugador se encuentra jugando actualmente
        /// </summary>
        /// <param name="username"></param>
        /// <returns>True si el jugador esta jugando, false en caso contrario</returns>
        public bool isPlaying(string username)
        {
            bool value = false;
            if (currentlyUserPlayed.Contains(username))
            {
               value = true;
            }
            return value;
        }

        /// <summary>
        /// Mandar las listas correspondientes al tablero 
        /// </summary> 
        /// <param name="toUsername"> </param>
        /// <param name="randomImageList"> Imagenes del tablero </param>
        /// <param name="randomPositionList"> Posiciones del tablero </param>


        public void SendBoardLists(string toUsername, List<int> randomImageList, List<int> randomPositionList)
        {
            users[toUsername].ReceiveCardSeed(randomImageList, randomPositionList);
        }
        /// <summary>
        /// Mandar las cartas correctas al rival 
        /// </summary> 
        /// <param name="toUsername"> </param>
        /// <param name="cards"> Par de cartas </param>

        public void SendCorrectCards(string toUsername, Dictionary<BitmapImage, string> cards)
        {
            users[toUsername].ReceiveCorrectPair(cards);
        }

        /// <summary>
        /// Mandar el puntaje del jugador al rival 
        /// </summary> 
        /// <param name="toUsername"> </param>
        /// <param name="score"> Par de cartas </param>

        public void SendScoreRival(string toUsername, int score)
        {
            users[toUsername].ReceiveScoreRival(score);
        }
        /// <summary>
        /// Mandar el siguiente turno al rival 
        /// <param name="toUsername"> </param>
        /// <param name="nextTurn"> Par de cartas </param>
        /// </summary> 
        public void SendNextTurnRival(string toUsername, bool nextTurn)
        {
            users[toUsername].ReceiveNextTurn(nextTurn);
        }
        /// <summary>
        /// Mandar el total de cartas encontradas en el tablero  al rival
        /// </summary> 
        /// <param name="toUsername"> </param>
        /// <param name="numberCardsFinded"> Total de cartas encontradas</param>

        public void SendNumberCardsFinded(string toUsername, int numberCardsFinded)
        {
            users[toUsername].ReceiveNumberCardsFinded(numberCardsFinded);
        }
        /// <summary>       
        /// Mandar el juego finalizado para registrar  
        ///   </summary> 
        /// <param name="gameCurrently"> Juego finalizado </param>


        public bool SendGame(GameCurrently gameCurrently)
        {
            bool value = false;
            Authentication authentication = new Authentication();

                if (authentication.AddGameSucessful(gameCurrently) == AuthenticationStatus.Success)
                {
                    value = true;
                }
            
           


            return value;
            
        }
        /// <summary>
        /// Mandar el ganador al rival
        /// </summary> 
        /// <param name="toUsername"> </param>
        ///  <param name="winner"> </param>


        public void SendWinner(string toUsername, string winner)
        {
            users[toUsername].ReceiveWinner(winner);
        }
        /// <summary>
        /// Cuando finaliza un juego es necesario quitar a los jugadores del listado de personas jugando 
        /// </summary> 
        /// <param name="username"> </param>
        ///  <param name="rival"> </param>

        public void DisconnectPlayers(string username, string rival)
        {
            currentlyUserPlayed.Remove(username);
            currentlyUserPlayed.Remove(rival);
            GetCurrentlyUserPlayed();
            GetConnectedUsers();
        }


        /// <summary>
        /// Buscar email de un jugador por medio de nombre del usuario 
        /// </summary>
        ///<param name="username"> </param>
        /// <returns> El email de usuario  </returns>
        public string GetEmailByUser(string username)
        {
            Authentication authentication = new Authentication();

            return authentication.GetEmail(username);
        }

        /// <summary>
        /// Actualizar la contraseña de un jugador 
        /// </summary>
        ///<param name="username"></param>
        ///<param name="newPassword"> </param>
        /// <returns> True si se pudo actualizar la  contraseña  y False en caso contrario  </returns>
        public bool ChangePassword(string username, string newPassword)
        {
            Authentication authentication = new Authentication();
            bool value = false;

            if (authentication.UpdateSucessfulPassword(username, newPassword) == AuthenticationStatus.Success)
            {
                value = true;
            }
            return value;
        }

        /// <summary>
        /// Buscar a un jugador
        /// </summary>
        ///<param name="username"></param>
        /// <returns> True si se pudo localizar al jugador y False en caso contrario  </returns>
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

        /// <summary>
        /// Mandar las cartas que esta volteando actualmente el jugador al rival
        ///</summary> 
        /// <param name="toUsername"> </param>
        ///  <param name="image">  Imagen de la carta</param>
        ///  <param name="name">  Identificador de la carta</param>


        public void SendCardTurn(string toUsername, BitmapImage image, string name)

        {
            users[toUsername].ReceiveCardTurn(image, name);
        }

        /// <summary>
        /// Obtener a los usuarios conectados 
        /// </summary>
        /// <returns> Lista de cadenas con los nombres de los usuarios conectados</returns>
        public List<string> GetUsersConnected()
        {
            List<string> usersConnected= new List<string>();
            foreach(var user in users.Keys)
            {
                usersConnected.Add(user);
            }

            return usersConnected;
        }
    }
    
}
