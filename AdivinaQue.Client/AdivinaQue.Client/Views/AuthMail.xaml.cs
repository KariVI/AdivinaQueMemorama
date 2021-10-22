using AdivinaQue.Client.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdivinaQue.Client.Views
{
    /// <summary>
    /// Lógica de interacción para AuthMail.xaml
    /// </summary>
    public partial class AuthMail : Window
    {
        private String code ;
        string username;
        Player player;
        Proxy.ServiceClient server;
        public AuthMail(String code, Player player)
        {
            InitializeComponent();
            this.code = code;
            this.player = player;
        }
        private void btConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (this.code == tbCode.Text)
            {
                Console.WriteLine("Ok");
                server.modify(player, username);
                MessageBox.Show("user modified successfully", "Message", MessageBoxButton.OK);
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect code", "Message", MessageBoxButton.OK);
            }
        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        internal void setServer(ServiceClient server)
        {
            this.server = server;
        }

        internal void setUsername(string username)
        {
            this.username = username;
        }

    }
}
