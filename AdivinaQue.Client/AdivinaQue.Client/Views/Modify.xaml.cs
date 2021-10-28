using AdivinaQue.Client.Proxy;
using System;
using System.Windows;

namespace AdivinaQue.Client.Views
{
    /// <summary>
    /// Lógica de interacción para Modify.xaml
    /// </summary>
    public partial class Modify : Window
    {
        private string username;
        private Player player;
        private Player newPlayer;
        private ServiceClient server;
        private Home home;
        public Modify()
        {      
            InitializeComponent();
            newPlayer = new Player();
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            newPlayer.Name = tbName.Text;
            newPlayer.Username = tbUsername.Text;
            newPlayer.Email = tbEmail.Text;
            newPlayer.Password = pbPassword.Password;
            String code = server.SendMailValidation(tbEmail.Text);
            AuthMail authmail = new AuthMail(code,newPlayer);
            authmail.setServer(server);
            authmail.setUsername(username);
            authmail.Show();
            this.Close();
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            var option = MessageBox.Show("Delete user?", "Message", MessageBoxButton.YesNo);
            if(option == MessageBoxResult.Yes)
            {
                server.Delete(username);
                home.disconect();
                this.Close();
            }   
        }

        private void closedWindow(object sender, EventArgs e)
        {
            this.Close();
        }

        internal void setServer(ServiceClient server)
        {
            this.server = server;
            server.SearchInfoPlayerByUsername(username);
        }

        internal void setUsername(string username)
        {
            this.username = username;
        }

        internal void setHome(Home home)
        {
            this.home = home;
        }

        public void setPlayer(Player player)
        {
            this.player = player;
            tbName.Text =player.Name;
            tbUsername.Text = player.Username;
            tbEmail.Text = player.Email;
            pbPassword.Password = player.Password;
        }


    }
}
