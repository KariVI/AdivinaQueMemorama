using AdivinaQue.Client.Control;
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
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private ServiceClient server;
        private string username;
        private Chat chat;
        private CallBack callback;
        public Home(ServiceClient server,CallBack callback)
        {
            InitializeComponent();
            this.server = server;
            this.callback = callback;
        }

        private void btModify_Click(object sender, RoutedEventArgs e)
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            server = new Proxy.ServiceClient(context);
            Modify modify = new Modify();
            callback.setModify(modify);
            modify.setHome(this);
            modify.setUsername(username);
            modify.setServer(server);
            modify.Show();
        }
        private void btLogout_Click(object sender, RoutedEventArgs e)
        {
            disconect();
        }

        public void disconect()
        {
            server.DisconnectUser(username);
            Login login = new Login();
            login.Show();
            chat.Close();
            this.Close();
        }

        public void setUsername(string username)
        {
            lbUser.Content = "Welcome, " + username;
            this.username = username;
        }

        public void setChat(Chat chat)
        {
            this.chat = chat;
        }

        private void btStartGame_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext context = new InstanceContext(callback);
            server = new Proxy.ServiceClient(context);
            callback.SetCurrentUsername(username);
            PlayersList playersList = new PlayersList(server,username,callback);
            callback.setPlayersList(playersList); 
            server.GetConnectedUsers();
            playersList.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Podio podio = new Podio(server, username);
            callback.setPodio(podio);
            server.GetScores(username);
            podio.Show();
            this.Close();
        }
    }
}
