using AdivinaQue.Client.Control;
using AdivinaQue.Client.Logs;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Windows;

namespace AdivinaQue.Client.Views
{

    public partial class Login : Window
    {
        CallBack callback;
        InstanceContext context;
        Proxy.PlayerMgtClient serverPlayer;
        Proxy.GameMgtClient serverGame;
        private static readonly ILog Logs = Log.GetLogger();

        private int numberFailedEnter = 0;
        public Login()
        {
            InitializeComponent();
            callback = new CallBack();
            context = new InstanceContext(callback);
            serverPlayer = new Proxy.PlayerMgtClient(context);
            serverGame = new Proxy.GameMgtClient(context);

            LoadStringResource("es-MEX");
        }

        private bool VerifyUserConnected()
        {
            bool value = false;
            string[] usersConnected = serverPlayer.GetUsersConnected();
            if (usersConnected.Contains(tbUsername.Text))
            {
                value = true;
            }
            return value;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUsername.Text) && !string.IsNullOrEmpty(Password.Password.ToString()) 
                && !string.IsNullOrWhiteSpace(tbUsername.Text) && !string.IsNullOrWhiteSpace(Password.Password.ToString()))
            {
                try
                {
                    if (!VerifyUserConnected()) {
                        Join();
                    }
                    else
                    {
                        Alert.ShowDialog(Application.Current.Resources["lbUserConnected"].ToString(), Application.Current.Resources["btOk"].ToString());

                    }
                }
                catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException )
                {
                    Logs.Error($"Fallo la conexión ({ ex.Message})");                                        
                    Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                    numberFailedEnter = 0;
                    this.Close();

                }
            }
            else
            {
                Alert.ShowDialog(Application.Current.Resources["lbEmptyFields"].ToString(), Application.Current.Resources["btOk"].ToString());

            }

        }

        private void Join()
        {
            Boolean value = serverPlayer.Join(tbUsername.Text, Password.Password.ToString());
            if (!value && numberFailedEnter < 3)
            {
                Alert.ShowDialog(Application.Current.Resources["lbWrongCredentials"].ToString(), Application.Current.Resources["btOk"].ToString());
                numberFailedEnter++;
            }
            else if (!value && numberFailedEnter == 3)
            {
                if (ChangePasswordSucessful())
                {
                    Alert.ShowDialog(Application.Current.Resources["lbRebaseEnters"].ToString(), Application.Current.Resources["btOk"].ToString());
                }
            }
            else if (value)
            {
                Home home = new Home(serverPlayer, callback);
                home.SetUsername(tbUsername.Text);
                callback.SetCurrentUsername(tbUsername.Text);
                callback.SetServer(serverGame);
                callback.SetServerPlayer(serverPlayer);
                serverPlayer.GetConnectedUsers();
                this.Hide();
                home.Show();
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
            ValidationCode validationCode = new ValidationCode(serverPlayer);
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

        private bool ChangePasswordSucessful()
        {
            bool value = false;
            String passwordDefault = GenerateCodeValidation();

            if (serverPlayer.ChangePassword(tbUsername.Text, passwordDefault))
            {
                string email = serverPlayer.GetEmailByUser(tbUsername.Text);

                string message = Application.Current.Resources["lbDefaultPassword"].ToString();
                string subject = Application.Current.Resources["lbSubjetcPassword"].ToString();
                string body = @"<style>     
                                 h3{color:#E267B4;}
                                 </style>
                                  <p> Tu nueva contraseña es: </p>
                                  <h4>" + message + " " + passwordDefault + "</h3>" + "<p> Recuerda cambiar tu contraseña cuando inicies sesión " +
                                  "<br> Si no fuiste tu el que solicito el cambio de contraseña, ignora el mensaje</p>  ";

                String messageEmailSuccesful = serverPlayer.SendMail(email, subject, body);
                if (messageEmailSuccesful == "Exito")
                {
                    value = true;
                }
            }

                return value;

        }
        private void btPassword_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUsername.Text) && !string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                if (serverPlayer.FindUsername(tbUsername.Text))
                {
                    if (ChangePasswordSucessful())
                    {
                        Alert.ShowDialog(Application.Current.Resources["lbNewPassword"].ToString(), Application.Current.Resources["btOk"].ToString());
                    }
                }
                else
                {
                    Alert.ShowDialog(Application.Current.Resources["lbNoExistentUsername"].ToString(), Application.Current.Resources["btOk"].ToString());
                }
            }
            else
            {
                Alert.ShowDialog(Application.Current.Resources["lbNewPasswordDataError"].ToString(), Application.Current.Resources["btOk"].ToString());
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
      

      
    }
}

