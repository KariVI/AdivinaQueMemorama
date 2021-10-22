using AdivinaQue.Client.Control;
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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUsername.Text))
            {
                CallBack callback = new CallBack();
                InstanceContext context = new InstanceContext(callback);
                Proxy.ServiceClient server = new Proxy.ServiceClient(context);

                Boolean value = server.join(tbUsername.Text, Password.Password.ToString());
                if (value == false)
                {
                    MessageBox.Show("Credenciales incorrectas", "Message", MessageBoxButton.OK);
                }

                else
                {
                    Chat chat = new Chat(server);
                    chat.setUsername(tbUsername.Text);
                    callback.setChat(chat);
                    Home home = new Home(server,callback);
                    home.setUsername(tbUsername.Text);
                    home.setChat(chat);
                    server.getConnectedUsers();
                    home.Show();
                    chat.Show();
                    this.Close();
                }

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
        }
    }
}
