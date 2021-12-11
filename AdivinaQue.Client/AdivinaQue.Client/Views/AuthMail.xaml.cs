using AdivinaQue.Client.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
        private Home home;
        Proxy.PlayerMgtClient server;
        private bool backHome = true;

        public AuthMail(String code, Player player,Home home)
        {
            InitializeComponent();
            this.code = code;
            this.player = player;
            this.home = home;
        }
        private void btConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCode.Text) && !IsVoid())
            {
                if (this.code == tbCode.Text)
            {
                try
                {
                    server.Modify(player, username);
                }
                catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException )
                {
                    Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                    backHome = false;
                    Login login = new Login();
                    login.Show();
                    this.Close();
                }
                Alert.ShowDialog(Application.Current.Resources["lbModifySucces"].ToString(), Application.Current.Resources["btOk"].ToString());
                this.Close();
            }
            else
            {
                
               Alert.ShowDialog(Application.Current.Resources["lbIncorrectCode"].ToString(), Application.Current.Resources["btOk"].ToString());
            
            }
            }
            else
            {
                Alert.ShowDialog(Application.Current.Resources["lbEmptyFields"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
        }
        private bool IsVoid()
        {
            bool value = false;
            if (string.IsNullOrWhiteSpace(tbCode.Text))
            {
                value = true;
            }
            return value;
        }
        internal void SetServer(PlayerMgtClient server)
        {
            this.server = server;
        }

        internal void SetUsername(string username)
        {
            this.username = username;
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (backHome)
            {
                home.Show();
            }
        }
    }
}
