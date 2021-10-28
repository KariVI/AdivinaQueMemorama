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
    /// Lógica de interacción para Podio.xaml
    /// </summary>
    public partial class Podio : Window
    {
        public ObservableCollection<String> playersCollection;
        public ObservableCollection<int> scoresCollection;
        Proxy.ServiceClient server;
        String username;
        public ListView players { get { return lvPlayer; } set { lvPlayer = value; } }
        public ListView victories { get { return lvVictory; } set { lvVictory = value; } }

        public Podio(Proxy.ServiceClient server, String username)
        {
         
            InitializeComponent();
            playersCollection = new ObservableCollection<String>();
            scoresCollection = new ObservableCollection<int>();
            this.server = server;
            this.username = username;
            players.ItemsSource = playersCollection;
            lvVictory.ItemsSource = scoresCollection;
    }

      
    }
}
