using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        public Chat()
        {
            InitializeComponent();
        }
        Proxy.ServiceClient server;
        private string username;
        private string typeMessage;

        public ObservableCollection<String> messagesCollection;
        public ObservableCollection<String> usersCollection;


        public Label MessageTitle { get { return Title; } set { Title = value; } }
        public Button SendMessageButton { get { return SendButton; } set { SendButton = value; } }
        public ScrollViewer ScrollTextBox { get { return ContentScroller; } set { ContentScroller = value; } }
        public TextBox MessageContainer { get { return MessageContent; } set { MessageContent = value; } }
        public ListBox listUsers { get { return UsersConnected; } set { UsersConnected = value; } }
        public ListView messages { get { return listMessages; } set { listMessages = value; } }


        public Chat(Proxy.ServiceClient server)
        {
            InitializeComponent();
            this.server = server;
            messagesCollection = new ObservableCollection<String>();
            usersCollection = new ObservableCollection<string>();
            typeMessage = "Todos";
            messages.ItemsSource = messagesCollection;
            listUsers.ItemsSource = usersCollection;

        }

        public void setUsername(string username)
        {
            User.Content = "Bienvenido, " + username;
            this.username = username;
        }



        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MessageContent.Text))

                if (listUsers.SelectedValue != null)
                {
                    Console.WriteLine(typeMessage);
                    typeMessage = listUsers.SelectedValue.ToString();
                }

            server.sendMessage(MessageContent.Text, username, typeMessage);
            listUsers.SelectedValue = null;
            MessageContent.Clear();
            typeMessage = "Todos";

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            server.disconnectUser(username);
            this.Close();
        }
    }
}
