using System;
using System.Windows;

namespace AdivinaQue.Client.Views
{
    /// <summary>
    /// Lógica de interacción para Register.xaml
    /// </summary>
    public partial class Register : Window
    {

        Proxy.ServiceClient server;
        private String email;


        public Register(Proxy.ServiceClient server, String email)
        {

            InitializeComponent();
            this.server = server;
            this.email = email;

        }
        private void RegisterBt_Click(object sender, RoutedEventArgs e)
        {

            if (tbUsername.Text != "" && Password.Password.ToString() != "" && tbName.Text != "")
            {
                register();
            }
            else
            {
                MessageBox.Show("Exist empty fields");
            }
        }

        public void register()
        {

            Proxy.Player player = new Proxy.Player();
            player.Username = tbUsername.Text;
            player.Password = Password.Password.ToString();
            player.Name = tbName.Text;
            player.Email = email;


            server.Register(player);
            MessageBox.Show("Saved Data");
            this.Close();

        }


        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void closedWindow(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
