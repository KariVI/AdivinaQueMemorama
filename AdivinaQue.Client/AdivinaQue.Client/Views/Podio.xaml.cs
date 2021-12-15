using AdivinaQue.Client.Control;
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
   
    public partial class Podio : Window
    {
        public ObservableCollection<String> PlayersCollection { get; set; }
        public ObservableCollection<int> ScoresCollection { get; set; }
        private readonly Proxy.PlayerMgtClient serverPlayer;
        private readonly string username;
        private Home home;

        public ListView players { get { return lvPlayer; } set { lvPlayer = value; } }
        public ListView victories { get { return lvVictory; } set { lvVictory = value; } }

        public Podio(Proxy.PlayerMgtClient server, String username, Home home)
        {
            InitializeComponent();
            PlayersCollection = new ObservableCollection<String>();
            ScoresCollection = new ObservableCollection<int>();
            this.serverPlayer = server;
            this.username = username;
            this.home = home;
            players.ItemsSource = PlayersCollection;
            lvVictory.ItemsSource = ScoresCollection;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            home.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            home.Show();
        }
    }
}