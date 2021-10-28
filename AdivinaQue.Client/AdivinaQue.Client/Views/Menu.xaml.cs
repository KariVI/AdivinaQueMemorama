using AdivinaQue.Client.Control;
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
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        Proxy.ServiceClient server;
        CallBack callback;
        string username;
        public Menu(Proxy.ServiceClient server, string username, CallBack callback)
        {
            InitializeComponent();
            this.server=server;
            this.username = username;
            this.callback = callback;
            lbUserName.Content = "¡Hola " + username + "!";
        }

        private void ChatBt_Click(object sender, RoutedEventArgs e)
        {
            Chat chat = new Chat(server,callback);
            chat.setUsername(username);
            callback.setChat(chat);
            callback.setChat(chat);
            server.getConnectedUsers();
            chat.Show();
            this.Close();
        }

        private void QueryScoreBt_Click(object sender, RoutedEventArgs e)
        {
            Podio podio = new Podio(server, username);
            callback.setPodio(podio);
            server.getScores(username);
            podio.Show();
            this.Close();
        }
    }
}
