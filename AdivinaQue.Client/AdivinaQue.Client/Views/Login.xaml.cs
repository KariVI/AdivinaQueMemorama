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
        CallBack callback;
        InstanceContext context;
        Proxy.ServiceClient server;
        public Login()
        {
            InitializeComponent();
            callback = new CallBack();
            context = new InstanceContext(callback);
            server = new Proxy.ServiceClient(context);
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUsername.Text))
            {
           

                Boolean value = server.Join(tbUsername.Text, Password.Password.ToString());
                if (value == false)
                {
                    MessageBox.Show("Credenciales incorrectas", "Message", MessageBoxButton.OK);
                }

                else
                {
                    Chat chat = new Chat(server);
                    chat.setUsername(tbUsername.Text);
                    callback.SetChat(chat);
                    Home home = new Home(server,callback);
                    home.setUsername(tbUsername.Text);
                    callback.SetCurrentUsername(tbUsername.Text);
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
            ValidationCode validationCode = new ValidationCode(server);
            callback.SetValidateCode(validationCode);

            validationCode.Show();
        }
    }
}
