using AdivinaQue.Client.Logs;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
      
        Proxy.PlayerMgtClient server;
        private string username;
        private string typeMessage;

        public ObservableCollection<String> MessagesCollection { get; set;  }
        public ObservableCollection<String> UsersCollection { get; set; }
        private static readonly ILog Logs = Log.GetLogger();



        public Label MessageTitle { get { return Title; } set { Title = value; } }
        public Button SendMessageButton { get { return SendButton; } set { SendButton = value; } }
        public ScrollViewer ScrollTextBox { get { return ContentScroller; } set { ContentScroller = value; } }
        public TextBox MessageContainer { get { return MessageContent; } set { MessageContent = value; } }
        public ListBox ListUsers { get { return UsersConnected; } set { UsersConnected = value; } }
        public ListView Messages { get { return listMessages; } set { listMessages = value; } }

        /// <summary>
        /// Inicializa una nueva instancia de chat.xaml.
        /// </summary>
        /// <param name="server"></param>
        public Chat(Proxy.PlayerMgtClient server)
        {
            InitializeComponent();
            this.server = server;
            MessagesCollection = new ObservableCollection<String>();
            UsersCollection = new ObservableCollection<string>();
            typeMessage = "Todos";
            Messages.ItemsSource = MessagesCollection;
            ListUsers.ItemsSource = UsersCollection;

        }

        /// <summary>
        /// Inicializa el nombre de usuario y llama a los métodos que depende de este valor.
        /// </summary>
        /// <param name="username"></param>
        public void SetUsername(string username)
        {
            this.username = username;
            SetLabelGretting();
        }

        /// <summary>
        /// Inicializa la label de saludo.
        /// </summary>
        public void SetLabelGretting()
        {
            lbUser.Content = Application.Current.Resources["lbGretting"].ToString() + " " + username;
        }

        /// <summary>
        /// Controlador del botón para enviar un mensaje.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtSendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MessageContent.Text) && !string.IsNullOrWhiteSpace(MessageContent.Text))
            {

                if (ListUsers.SelectedValue != null)
                {
                    typeMessage = ListUsers.SelectedValue.ToString();
                }
                try
                {
                    server.SendMessage(MessageContent.Text, username, typeMessage);
                }
                catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException)

                {

                    Logs.Error($"Fallo la conexión ({ ex.Message})");
                    Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                    this.Close();

                }
                ListUsers.SelectedValue = null;
                MessageContent.Clear();
                typeMessage = "Todos";
            }

        }

        /// <summary>
        /// Controlador del botón para cerrar la ventana.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
