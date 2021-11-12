using AdivinaQue.Client.Control;
using System;
using System.Linq;
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
            LoadStringResource("es-MEX");
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUsername.Text))               
            {
                Boolean value=false;
                try
                {
                     value = server.Join(tbUsername.Text, Password.Password.ToString());
                }catch(EndpointNotFoundException ex)
                {
                    MessageBox.Show("Sorry, the server isn't running");
                }
                
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

        private void LoadStringResource(string locale)
        {
            var resources = new ResourceDictionary();

            resources.Source = new Uri("pack://application:,,,/Resources_" + locale + ";component/Strings.xaml", UriKind.Absolute);

            var current = Application.Current.Resources.MergedDictionaries.FirstOrDefault(
                             m => m.Source.OriginalString.EndsWith("Strings.xaml"));


            if (current != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(current);
            }

            Application.Current.Resources.MergedDictionaries.Add(resources);
        }

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ValidationCode validationCode = new ValidationCode(server);
            callback.SetValidateCode(validationCode);

            validationCode.Show();
        }

        private void btLanguageEnglish_Click(object sender, RoutedEventArgs e)
        {
            LoadStringResource("en-US");
            
        }

        private void btLanguageSpanish_Click(object sender, RoutedEventArgs e)
        {
            LoadStringResource("es-MEX");
        }
    }
}
