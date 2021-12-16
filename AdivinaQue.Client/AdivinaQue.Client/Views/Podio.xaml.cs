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
    /// <summary>
    /// Lógica de interacción para Podio.xaml.
    /// </summary>
    public partial class Podio : Window
    {
        public ObservableCollection<String> PlayersCollection { get; set; }
        public ObservableCollection<int> ScoresCollection { get; set; }
        private readonly Proxy.PlayerMgtClient serverPlayer;
        private readonly string username;
        private Home home;

        public ListView players { get { return lvPlayer; } set { lvPlayer = value; } }
        public ListView victories { get { return lvVictory; } set { lvVictory = value; } }

        /// <summary>
        /// Inicializa una nueva instancia de Podio.xaml.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="username"></param>
        /// <param name="home"></param>
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

        /// <summary>
        /// Controlador del botón para cerrar la ventana.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            home.Show();
        }

        /// <summary>
        /// Controlador del botón para regresar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            home.Show();
        }
    }
}