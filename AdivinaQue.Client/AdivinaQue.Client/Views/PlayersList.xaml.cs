using AdivinaQue.Client.Control;
using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace AdivinaQue.Client.Views
{
    /// <summary>
    /// Lógica de interacción para PlayersList.xaml
    /// </summary>
    public partial class PlayersList : Window
    {
        Proxy.PlayerMgtClient serverPlayer;
        public ListBox listUsers { get { return UsersConnected; } set { UsersConnected = value; } }
        public ListBox listUsersPlayed { get { return UsersPlaying; } set { UsersPlaying = value; } }
        public ObservableCollection<String> usersCollection;
        public ObservableCollection<string> usersPlayedCollection;
        private String username;
        private Home home;
        Boolean backHome = true;
        CallBack callback;
        public PlayersList(Proxy.PlayerMgtClient server, String username, Home home, CallBack callBack)
        {
            InitializeComponent();

            this.callback = callBack;
            this.serverPlayer = server;
            usersCollection = new ObservableCollection<string>();
            usersPlayedCollection = new ObservableCollection<string>();
            listUsers.ItemsSource = usersCollection; 
            listUsersPlayed.ItemsSource = usersPlayedCollection;
            this.username = username;
            this.home = home;
            
        }
        private void btSendEmail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                serverPlayer.SendMail(tbEmail.Text, "Invitation to play", "Ingrese el codigo en la aplicacion: " + "Lo han invitado a jugar Adivina Que! Instale el juego AQUI");
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException)
            {
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                backHome = false;
                Login login = new Login();
                login.Show();
                this.Close();
            }
            this.Close();
        }

        private void btSend_Click(object sender, RoutedEventArgs e)
        {

            if (listUsers.SelectedValue != null)
            {
                try
                {
                    var player = listUsers.SelectedValue.ToString();
                    bool result = serverPlayer.SendInvitation(player, username);
                    showResponse(result, player);
                }
                catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException)
                {
                    Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                    backHome = false;
                    Login login = new Login();
                    login.Show();
                    this.Close();
                }
            }
        }
        private void showResponse(bool result, string player)
        {
            if (result)
            {
                GameConfiguration gameConfiguration = new GameConfiguration(callback, username, player);
                gameConfiguration.Home = home;
                callback.SetGameConfiguration(gameConfiguration);
                backHome = false;
                gameConfiguration.Show();
                this.Close();
            }
            else
            {
                Alert.ShowDialog(player + " " + Application.Current.Resources["lbDeclineInvitation"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
        }



        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            home.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            if (backHome)
            {
                home.Show();
            }

        }
    }
    
}
