using AdivinaQue.Client.Control;
using System;
using System.Linq;
using System.ServiceModel;
using System.Windows;

namespace AdivinaQue.Client.Views
{

    public partial class Login : Window
    {
        CallBack callback;
        InstanceContext context;
        Proxy.ServiceClient server;
        private int numberFailedEnter = 0;
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
                try
                {
                    Boolean value = server.Join(tbUsername.Text, Password.Password.ToString());
                    if (!value && numberFailedEnter < 3)
                    {
                        Alert.ShowDialog(Application.Current.Resources["lbWrongCredentials"].ToString(), Application.Current.Resources["btOk"].ToString());
                        numberFailedEnter++;
                    }
                    else if (!value && numberFailedEnter == 3)
                    {

                    }
                    else if (value)
                    {
                        Home home = new Home(server, callback);
                        home.setUsername(tbUsername.Text);
                        callback.SetCurrentUsername(tbUsername.Text);
                        callback.setServer(server);
                        server.GetConnectedUsers();
                        home.Show();
                        this.Close();
                    }
                }
                catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException)
                {

                    Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                    Login login = new Login();
                    this.Close();
                    login.Show();
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

        public string GenerateCodeValidation()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var charsArray = new char[5];
            var random = new Random();

            for (int i = 0; i < charsArray.Length; i++)
            {
                charsArray[i] = characters[random.Next(characters.Length)];
            }

            var resultString = new String(charsArray);
            return resultString;
        }


        private void btPassword_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUsername.Text) && !string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                if (server.FindUsername(tbUsername.Text))
                {
                    String passwordDefault = GenerateCodeValidation();

                    if (server.ChangePassword(tbUsername.Text, passwordDefault))
                    {
                        string email = server.GetEmailByUser(tbUsername.Text);

                        string body = @"<style>     
                                                        h3{color:#E267B4;}
                                                        </style>
                                                        <p> Tu nueva contraseña es: </p>
                                                        <h4>" + passwordDefault + "</h3>" + "<p> Recuerda cambiar tu contraseña cuando inicies sesión " +
                                                        "<br> Si no fuiste tu el que solicito el cambio de contraseña, ignora el mensaje</p>  ";

                        String messageEmailSuccesful = server.SendMail(email, "Nueva contraseña", body);
                        MessageBox.Show("Check your email for a new password");
                    }
                }
                else
                {
                    MessageBox.Show("This username doesn't exist in the app");
                }
            }
            else
            {
                MessageBox.Show("Please write an username for send a new password");
            }

        }
    }
}

