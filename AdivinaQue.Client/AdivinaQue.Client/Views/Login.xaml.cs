using AdivinaQue.Client.Control;
using AdivinaQue.Client.Logs;
using log4net;
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
        Proxy.PlayerMgtClient serverPlayer;
        Proxy.GameMgtClient serverGame;

        private static readonly ILog Logs = Log.GetLogger();
        private int numberFailedEnter = 0;

        /// <summary>
        /// Inicializa una nueva instancia de la clase Login.xaml.
        /// </summary>
        public Login()
        {
            InitializeComponent();
            callback = new CallBack();
            context = new InstanceContext(callback);
            serverPlayer = new Proxy.PlayerMgtClient(context);
            serverGame = new Proxy.GameMgtClient(context);

            LoadStringResource("es-MEX");
        }

        /// <summary>
        /// Verifica que el usuario no se encuentre actualmente conectado.
        /// </summary>
        /// <returns> True si ya se encuentra conectado y false en caso contrario </returns>
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

        /// <summary>
        /// Controlador del botón para iniciar sesión.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUsername.Text) && !string.IsNullOrEmpty(pbPassword.Password.ToString())
                && !string.IsNullOrWhiteSpace(tbUsername.Text) && !string.IsNullOrWhiteSpace(pbPassword.Password.ToString()))
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
                catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException)
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

        /// <summary>
        /// Inicia sesión, manda los datos de los textbox al servidor y si son correctos entra al menú principal.
        /// </summary>
        private void Join()
        {
            Boolean value = serverPlayer.Join(tbUsername.Text, pbPassword.Password.ToString());
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

        /// <summary>
        /// Carga los recursos para determinado idioma.
        /// </summary>
        /// <param name="locale">String que indica el idioma a cargar</param>
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

        /// <summary>
        /// Controlador del botón para registrarse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtRegister_Click(object sender, RoutedEventArgs e)
        {
            ValidationCode validationCode = new ValidationCode(serverPlayer);
            callback.SetValidateCode(validationCode);

            validationCode.Show();
        }

        /// <summary>
        /// Controlador del botón para cambiar el idioma a inglés.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtLanguageEnglish_Click(object sender, RoutedEventArgs e)
        {
            LoadStringResource("en-US");

        }

        /// <summary>
        /// Controlador del botón para cambiar el idioma a español.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtLanguageSpanish_Click(object sender, RoutedEventArgs e)
        {
            LoadStringResource("es-MEX");
        }

        /// <summary>
        /// Genera un código aleatorio.
        /// </summary>
        /// <returns>String de 5 caracteres aleatorios.</returns>
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

        /// <summary>
        /// Cambia la contraseña del usuario que intenta iniciar sesión.
        /// </summary>
        /// <returns>true si fue posible cambiar la contraseña, false en caso contrario</returns>
        private bool ChangePasswordSucessful()
        {
            bool value = false;
            String passwordDefault = GenerateCodeValidation();
            try
            {
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
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException)
            {
                Logs.Error($"Fallo la conexión ({ ex.Message})");
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                numberFailedEnter = 0;
                this.Close();

            }

            return value;

        }

        /// <summary>
        /// Controlador del botón para cambiar la contraseña.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        ///  Controlador para el botón de cerrar ventana.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
    
    }
}

