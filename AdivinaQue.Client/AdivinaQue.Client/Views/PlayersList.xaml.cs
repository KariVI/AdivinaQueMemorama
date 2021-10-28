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
        public PlayersList(Proxy.ServiceClient server, String username)
        {
            InitializeComponent();
            this.server = server;
            usersCollection = new ObservableCollection<string>();
            listUsers.ItemsSource = usersCollection;
            this.username = username;
        }

        private void btSendEmail_Click(object sender, RoutedEventArgs e)
        {
            server.SendMailInvitation(tbEmail.Text);
            this.Close();
        }

        private void btSend_Click(object sender, RoutedEventArgs e)
        {
            
                if (listUsers.SelectedValue != null)
                {
                    var player = listUsers.SelectedValue.ToString();
                    var result  = server.SendInvitation(player,username);
                if (!result)
                {
                    MessageBox.Show(player + " decline your invitation", "Message", MessageBoxButton.OK);
                }
                }

           
        }
    }
}
