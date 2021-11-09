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
    /// Lógica de interacción para GameConfiguration.xaml
    /// </summary>
    public partial class GameConfiguration : Window
    {
        public ObservableCollection<String> topicsCollection;
        Proxy.ServiceClient server;
        private String username;
        private String toUsername;
        public ListBox lbxTopic { get { return lbxTopics; } set { lbxTopics = value; } }
        public GameConfiguration(Proxy.ServiceClient server, String username, String toUsername)
        {

            InitializeComponent();
            topicsCollection = new ObservableCollection<string>();
            lbxTopic.ItemsSource = topicsCollection;
            this.server = server;
            this.username = username;
            this.toUsername = toUsername;
        }


        private void ConfirmBt_Click(object sender, RoutedEventArgs e)
        {
            string category = "All";

            if (lbxTopic.SelectedItem != null)
            {
                category = lbxTopic.SelectedItem.ToString();
            }

            int sizeBoard = 0;
            if (cbSizeBoard.SelectedItem.ToString().Equals("4 x 4"))
            {
                sizeBoard = 4;
            }
            else if (cbSizeBoard.SelectedItem.ToString().Equals("5 x 5"))
            {
                sizeBoard = 5;

            }
            else
            {
                sizeBoard = 6;

            }
            Game game = new Game(sizeBoard,category);
            server.SendBoard(toUsername,sizeBoard,category);
            game.SetUsername(username);
            game.SetUsernameRival(toUsername);
            game.Show();
            this.Close();
        }
    }
}

