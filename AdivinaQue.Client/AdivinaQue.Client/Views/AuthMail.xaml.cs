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
    public partial class AuthMail : Window
    {
        private String code ;
        string username;
        Player player;
        Proxy.PlayerMgtClient server;
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
                
                server.Modify(player, username);
                Alert.ShowDialog(Application.Current.Resources["lbModifySucces"].ToString(), Application.Current.Resources["btOk"].ToString());
                this.Close();
            }
            else
            {
                
               Alert.ShowDialog(Application.Current.Resources["lbIncorrectCode"].ToString(), Application.Current.Resources["btOk"].ToString());
            
            }
        }
        private void btCancel_Click()
        {
            this.Close();
        }

        internal void setServer(PlayerMgtClient server)
        {
            this.server = server;
        }

        internal void setUsername(string username)
        {
            this.username = username;
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
