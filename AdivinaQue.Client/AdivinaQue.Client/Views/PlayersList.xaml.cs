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
        Proxy.ServiceClient server;
        public ListBox listUsers { get { return UsersConnected; } set { UsersConnected = value; } }
        public ObservableCollection<String> usersCollection;
        public ObservableCollection<string> usersPlayed;
        private String username;
        private Home home;
        Boolean backHome = true;
        CallBack callback;
        public PlayersList(Proxy.ServiceClient server, String username, Home home,CallBack callBack)
        {
            InitializeComponent();
            this.callback = callBack;
            this.server = server;
            usersCollection = new ObservableCollection<string>();
            usersPlayed = new ObservableCollection<string>();
            listUsers.ItemsSource = usersCollection;
            this.username = username;
            this.home = home;
        }

        private void btSendEmail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                server.SendMail(tbEmail.Text, "Invitation to play", "Ingrese el codigo en la aplicacion: " + "Lo han invitado a jugar Adivina Que! Instale el juego AQUI");
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
<<<<<<< HEAD
                     try { 
                        var player = listUsers.SelectedValue.ToString();
                        bool result  = server.SendInvitation(player,username);
                        showResponse(result,player);
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

=======
                    var player = listUsers.SelectedValue.ToString();

                        var result = server.SendInvitation(player, username);
                        if (result)
                        {
                             GameConfiguration gameConfiguration = new GameConfiguration(callback, username, player);
                            callback.SetGameConfiguration(gameConfiguration);

                            gameConfiguration.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(player + " decline your invitation", "Message", MessageBoxButton.OK);
                        }
                    }
                    
                }
>>>>>>> main

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
