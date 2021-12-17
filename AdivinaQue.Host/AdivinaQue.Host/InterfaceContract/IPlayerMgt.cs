using AdivinaQue.Host.Exception;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.UI.WebControls;
using System.Windows.Media.Imaging;

namespace AdivinaQue.Host.InterfaceContract
{
    [ServiceContract(CallbackContract = typeof(IClient))]


    /// <summary>
    /// Contrato en relación al la administración de los usuarios
    /// </summary> 
    public interface IPlayerMgt
    {
        [OperationContract]
        /// <summary>
        /// Ingresar al servidor 
        /// </summary>
        ///<param name="username"> </param>
        ///<param name="password"> </param>
        ///<returns>True si fue posible ingresar y false si fue el caso contrario</returns>
        Boolean Join(string username, string password);
        [OperationContract(IsOneWay = true)]

        /// <summary>
        /// Enviar un mensaje a un jugador conectado
        /// </summary> 
        ///<param name="message"></param>
        /// <param name="username"></param>
        /// <param name="userReceptor"></param>

        void SendMessage(string message, String username, string userReceptor);
        [OperationContract(IsOneWay = true)]
        /// <summary>
        /// Obtener a los usuarios conectados 
        /// </summary>
        void GetConnectedUsers();
        [OperationContract(IsOneWay = true)]

        /// <summary>
        /// Desconectar a un jugador del servidor
        /// </summary>
        ///<param name="username"> </param>
        void DisconnectUser(String username);
        [OperationContract]
        /// <summary>
        /// Registrar un jugador
        /// </summary>
        ///<param name="player"> </param>
        ///<returns>True si fue posible registrar al jugador y false si fue el caso contrario</returns>
        Boolean Register(Player player);
        [OperationContract]
        /// <summary>
        /// Buscar si existe el jugador en el servidor 
        /// </summary> 
        ///<param name="newUsername"></param>
        /// <returns> Una cadena con el código generado  </returns>
        bool SearchUsername(String newUsername);
        [OperationContract]
        /// <summary>
        /// Buscar a un jugador
        /// </summary>
        ///<param name="username"></param>
        /// <returns> True si se pudo localizar al jugador y False en caso contrario  </returns>
        bool FindUsername(String username);
        [OperationContract(IsOneWay = true)]
        /// <summary>
        /// Buscar la información de un jugador por medio de su nombre de usuario 
        /// </summary> 
        ///<param name="username"></param>
        void SearchInfoPlayerByUsername(String username);
        [OperationContract]
        /// <summary>
        /// Generar un código 
        /// </summary> 
        /// <returns> Una cadena con el código generado  </returns>
        string GenerateCode();
        [OperationContract]
        /// <summary>
        /// Actualizar la información de un jugador 
        /// </summary>
        ///<param name="player"> Nueva información del jugador</param>
        ///<param name="username"></param>
        /// <returns> True si se pudo actualizar la información  y False en caso contrario  </returns>
        bool Modify(Player player, String username);
        [OperationContract]
        /// <summary>
        /// Eliminar a un jugador del juego
        /// </summary>
        ///<param name="username"> </param>
        ///<returns>True si fue posible eliminarlo y false si fue el caso contrario</returns>
        bool Delete(string username);
        [OperationContract]
        /// <summary>
        /// Enviar una invitación a una nueva partida 
        /// </summary> 
        ///<param name="message"></param>
        /// <param name="username"></param>
        /// <param name="userReceptor"></param>
        bool SendInvitation(String toUsername,String fromUsername);
        [OperationContract]
        /// <summary>
        /// Mandar un correo de confirmaciób
        /// </summary> 
        /// <param name="to"> Quien va recibir el tablero</param>
        /// <param name="asunto"></param>
        /// <param name="body"></param>
        /// <returns> Mandar una cadena de exito o error al enviar el correo</returns>
        string SendMail(string to, string asunto, string body) ;
        [OperationContract]
        /// <summary>
        /// Recuperar la lista de Emails registrados en el sistema 
        /// </summary>
        /// <returns> Una lista de strings con los emails </returns>
        List<String> GetEmails();
        [OperationContract]

        /// <summary>
        /// Buscar email de un jugador por medio de nombre del usuario 
        /// </summary>
        ///<param name="username"> </param>
        /// <returns> El email de usuario  </returns>
        string GetEmailByUser(string username);

        [OperationContract]
        /// <summary>
        /// Actualizar la contraseña de un jugador 
        /// </summary>
        ///<param name="username"></param>
        ///<param name="newPassword"> </param>
        /// <returns> True si se pudo actualizar la  contraseña  y False en caso contrario  </returns>
        bool ChangePassword(string username, string newPassword);
        [OperationContract]
        /// <summary>
        /// Recuperar la lista de Usuarios  registrados en el sistema 
        /// </summary>
        /// <returns> Una lista de strings con los usuarios </returns>
        List<string> GetUsers();
        [OperationContract]
        /// <summary>
        /// Obtener a los usuarios conectados 
        /// </summary>
        /// <returns> Lista de cadenas con los nombres de los usuarios conectados</returns>
        List<string> GetUsersConnected();

        [OperationContract(IsOneWay = true)]
        /// <summary>
        /// Mandar la lista de puntajes
        /// </summary> 
        /// <param name="username"> Quien va recibir los puntajes</param>
        void GetScores(string username);
        [OperationContract(IsOneWay = true)]
        /// <summary>
        /// Obtener a los usuarios que estan jugando actualmente 
        /// </summary>
        void GetCurrentlyUserPlayed();

        /// <summary>
        /// Agregar usuarios a la lista de conectados
        /// </summary> 
        /// <param name="rival"> </param>
        /// <param name="fromUsername"> </param>
        [OperationContract(IsOneWay = true)]
        void ConnectCurrentlyUsers(string rival, string fromUsername);
        /// <summary>
        /// Devuelve true si el jugador se encuentra jugando actualmente
        /// </summary>
        /// <param name="username"></param>
        /// <returns>True si el jugador esta jugando, false en caso contrario</returns>
        [OperationContract]
        bool isPlaying(string username);

    }
}
