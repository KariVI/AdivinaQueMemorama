using AdivinaQue.Client.Control;
using AdivinaQue.Client.Proxy;
using System;
using System.ServiceModel;
using System.Timers;
using System.Windows;

namespace AdivinaQue.Client.Views
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private Proxy.PlayerMgtClient serverPlayer;
        private string username;
        private Chat chat;
        private CallBack callback;

       /// <summary>
       /// Inicializa una nueva instancia de la clase Home.xaml.
       /// </summary>
       /// <param name="server"></param>
       /// <param name="callback"></param>
        public Home(PlayerMgtClient server,CallBack callback)
        {
            InitializeComponent();
            this.serverPlayer = server;
            this.callback = callback;
            callback.SetHome(this);
            chat = new Chat(serverPlayer);
            
        }
        /// <summary>
        /// Controlador del botón para modificar la cuenta del usuario actual.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtModify_Click(object sender, RoutedEventArgs e)
        {
           
            Modify modify = new Modify(callback,this);
            callback.SetModify(modify);
            modify.SetHome(this);
            modify.SetUsername(username);
            modify.SetServer(serverPlayer);
            this.Hide();
            modify.Show();
        }

        /// <summary>
        /// Cierra la ventana.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
          
        }
        
        /// <summary>
        /// Inicializar instanciancias que requieran del nombre de usuario.
        /// </summary>
        /// <param name="username">Nombre de usuario del jugador actual.</param>
        public void SetUsername(string username)
        {
            this.username = username;
            lbUser.Content = Application.Current.Resources["lbGretting"].ToString() + " " + username;
            chat.SetUsername(username);
            callback.SetChat(chat);
        }

        /// <summary>
        /// Controlador del botón para iniciar una partida.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtStartGame_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext context = new InstanceContext(callback);
            serverPlayer = new Proxy.PlayerMgtClient(context);
            callback.SetCurrentUsername(username);
           
            try
            {
                PlayersList playersList = new PlayersList(serverPlayer, username, this,callback);
                callback.SetPlayersList(playersList);
                serverPlayer.GetConnectedUsers();
                serverPlayer.GetCurrentlyUserPlayed();
                playersList.Show();
                this.Hide();
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException )
            {
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                Login login = new Login();
                login.Show();
                this.Close();
            }    
        }

        /// <summary>
        /// Control del botón para ver el scoreboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtPodio_Click(object sender, RoutedEventArgs e)
        {
            Podio podio = new Podio(serverPlayer, username,this);
            callback.SetPodio(podio);
            try
            {
                serverPlayer.GetScores(username);
                podio.Show();
                this.Hide();
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException )
            {
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                Login login = new Login();
                login.Show();
                this.Close();
            }
            
        }

        /// <summary>
        /// Controlador del botón para cerrar la ventana.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            Disconect();
        }

        /// <summary>
        /// Cierra la sesión del usuario, regresa al login.xaml.
        /// </summary>
        public void Disconect()
        {
            try
            {
                serverPlayer.DisconnectUser(username);
                if (chat != null)
                {
                    chat.Close();
                }
                Login login = new Login();
                login.Show();
                this.Close();
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException)
            {
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
            }

        }

        /// <summary>
        /// Controlador del botón para abrir el chat.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btChat_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                serverPlayer.GetConnectedUsers();
                chat.Show();
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException )
            {
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                Login login = new Login();
                this.Close();
                login.Show();
            }
        }


    }
}
