using AdivinaQue.Client.Control;
using System;
using System.Collections.ObjectModel;
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
        private String username;
        CallBack callback;
        public PlayersList(Proxy.ServiceClient server, String username, CallBack callback)
        {
            InitializeComponent();
            this.server = server;
            usersCollection = new ObservableCollection<string>();
            listUsers.ItemsSource = usersCollection;
            this.username = username;
            this.callback = callback;
        }

        private void btSendEmail_Click(object sender, RoutedEventArgs e)
        {
            server.SendMail(tbEmail.Text, "Invitation to play", "Ingrese el codigo en la aplicacion: " + "Lo han invitado a jugar Adivina Que! Instale el juego AQUI");
            this.Close();
        }

        private void btSend_Click(object sender, RoutedEventArgs e)
        {
            
                if (listUsers.SelectedValue != null)
                {
                    var player = listUsers.SelectedValue.ToString();
                    var result  = server.SendInvitation(player,username);
                if (result)
                {
                    GameConfiguration gameConfiguration = new GameConfiguration(callback,username, player);
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
    }
}
