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
    /// Lógica de interacción para PlayersList.xaml
    /// </summary>
    public partial class PlayersList : Window
    {
        Proxy.ServiceClient server;
        public ListBox listUsers { get { return UsersConnected; } set { UsersConnected = value; } }
        public ObservableCollection<String> usersCollection;

        public PlayersList(Proxy.ServiceClient server)
        {
            InitializeComponent();
            this.server = server;
            usersCollection = new ObservableCollection<string>();
            listUsers.ItemsSource = usersCollection;
        }
    }
}
