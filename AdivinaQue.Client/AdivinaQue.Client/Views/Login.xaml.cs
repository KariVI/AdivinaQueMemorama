using AdivinaQue.Client.Control;
using System;
using System.ServiceModel;
using System.Windows;

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

                Boolean value = server.Join(tbUsername.Text, Password.Password.ToString());
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
                    server.GetConnectedUsers();
                    home.Show();
                    chat.Show();
                    this.Close();
                }

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ValidationCode validationCode = new ValidationCode();
            validationCode.Show();
        }
    }
}
