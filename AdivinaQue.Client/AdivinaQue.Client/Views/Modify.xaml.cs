using AdivinaQue.Client.Control;
using AdivinaQue.Client.Logs;
using AdivinaQue.Client.Proxy;
using log4net;
using System;
using System.ServiceModel;
using System.Windows;

namespace AdivinaQue.Client.Views
{
    /// <summary>
    /// Lógica de interacción para Modify.xaml
    /// </summary>
    public partial class Modify : Window
    {
        private string username;
        private Player newPlayer;
        private CallBack callback;
        private PlayerMgtClient server;
        private Home home;
        private bool backHome = true;
        private static readonly ILog Logs = Log.GetLogger();

        /// <summary>
        /// Inicializa una nueva instancia de Modify.xaml.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="home"></param>
        public Modify(CallBack callback, Home home)
        {
            InitializeComponent();
            newPlayer = new Player();
            this.callback = callback;
            this.home = home;
        }

        /// <summary>
        /// Controlador del botón para actualizar el jugador.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUsername.Text) && !string.IsNullOrEmpty(pbPassword.Password.ToString()) && !string.IsNullOrEmpty(tbName.Text) &&
                !string.IsNullOrEmpty(tbEmail.Text) && !IsVoid())
            {
                if (ValidateData() == DataStatus.Correct)
                {
                    newPlayer.Name = tbName.Text.Trim();
                    newPlayer.Username = tbUsername.Text.Trim();
                    newPlayer.Email = tbEmail.Text.Trim();
                    newPlayer.Password = pbPassword.Password;
                    SendEmail();
                    this.Close();
                }
                else
                {
                    SendMessage(ValidateData());
                }
            }
            else
            {
                
                Alert.ShowDialog(Application.Current.Resources["lbEmptyFields"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
        }

        /// <summary>
        /// Envia un correo electrónico al correo que se encuentra escrito actualmente en el textbox.
        /// </summary>
        private void SendEmail()
        {
            try
            {
                Validate validate = new Validate();
                if (validate.ValidationEmail(tbEmail.Text))
                {
                    String code = server.GenerateCode();
                    string message = Application.Current.Resources["lbEmailCodeMessage"].ToString();
                    string body = @"<style>
                                            h2{color:#E267B4;}
                                            </style>
                                            <h2>" + message + " " + code+ "</h2>";
                    string subject = Application.Current.Resources["lbEmailCodeSubject"].ToString();
                    String messageEmailSuccesful = server.SendMail(tbEmail.Text, subject, body);
                    AuthMail authmail = new AuthMail(code, newPlayer, home);
                    backHome = false;
                    authmail.SetServer(server);
                    authmail.SetUsername(username);
                    authmail.Show();
                    if (messageEmailSuccesful == "Exito")
                    {

                        Alert.ShowDialog(Application.Current.Resources["lbCodeSended"].ToString(), Application.Current.Resources["btOk"].ToString());
                    }
                    else
                    {
                        Alert.ShowDialog(Application.Current.Resources["lbEmailSendError"].ToString(), Application.Current.Resources["btOk"].ToString());

                    }
                }else
                    {
                        Alert.ShowDialog(Application.Current.Resources["lbIncorrectEmail"].ToString(), Application.Current.Resources["btOk"].ToString());

                    }
             }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException )
            {
                Logs.Error($"Fallo la conexión ({ ex.Message})");

                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                Login login = new Login();
                backHome = false;
                login.Show();
            }
        }

        /// <summary>
        ///  Verifica que los campos ingresados esten vacios.
        /// </summary>
        /// <returns>True si algún campo es vacío, false ningún campo es vacío</returns>
        private bool IsVoid()
        {
            bool value = false;
            if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbUsername.Text) ||
                string.IsNullOrWhiteSpace(pbPassword.Password.ToString()) || string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                value = true;
            }
            return value;
        }

        /// <summary>
        /// Valida los datos de registro ingresados en los textbox y passwordbox.
        /// </summary>
        /// <returns>Datastatus dependiento de los datos.</returns>
        private DataStatus ValidateData()
        {
            Validate validate = new Validate();
            DataStatus dataStatus = DataStatus.Correct;

            if (!validate.ValidationAlphanumeric(tbUsername.Text))
            {
                dataStatus = DataStatus.UserNameInvalid;
            }
            if (!validate.ValidationString(tbName.Text))
            {
                dataStatus = DataStatus.NameInvalid;
            }

            if (!validate.ValidationAlphanumeric(pbPassword.Password.ToString()))
            {
                dataStatus = DataStatus.PasswordInvalid;
            }

            if (pbPassword.Password.ToString().Length < 9)
            {
                dataStatus = DataStatus.ShortPassword;

            }

            if (!validate.ValidationEmail(tbEmail.Text))
            {
                dataStatus = DataStatus.EmailIncorrect;
            }

           
            return dataStatus;

        }

        /// <summary>
        /// Envia un alerta al usuario dependiendo de la variable dataStatus.
        /// </summary>
        /// <param name="dataStatus"></param>
        private void SendMessage(DataStatus dataStatus)
        {
            if (dataStatus == DataStatus.UserNameInvalid)
            {
                Alert.ShowDialog(Application.Current.Resources["lbValidUsername"].ToString(), Application.Current.Resources["btOk"].ToString());
            }

            if (dataStatus == DataStatus.NameInvalid)
            {
                Alert.ShowDialog(Application.Current.Resources["lbNameInvalid"].ToString(), Application.Current.Resources["btOk"].ToString());
            }

            if (dataStatus == DataStatus.PasswordInvalid)
            {
                Alert.ShowDialog(Application.Current.Resources["lbPasswordInvalid"].ToString(), Application.Current.Resources["btOk"].ToString());
            }

            if (dataStatus == DataStatus.ShortPassword)
            {
                Alert.ShowDialog(Application.Current.Resources["lbPasswordShort"].ToString(), Application.Current.Resources["btOk"].ToString());
            }

            if (dataStatus == DataStatus.EmailIncorrect)
            {
                Alert.ShowDialog(Application.Current.Resources["lbEmailInvalid"].ToString(), Application.Current.Resources["btOk"].ToString());
            }

        }

     
        /// <summary>
        /// Controlador del botón para cancelar la modificación.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            home.Show();
        }

        /// <summary>
        /// Controlador del botón eliminar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var option = Alert.ShowDialogWithResponse(Application.Current.Resources["lbDeleteAccount"].ToString(), Application.Current.Resources["btNo"].ToString(), Application.Current.Resources["btYes"].ToString());
            if (option == AlertResult.Yes)
            {
                try
                {
                    if (server.Delete(username))
                    {
                        Alert.ShowDialog(Application.Current.Resources["lbDeleteAccountCorrect"].ToString(), Application.Current.Resources["btOk"].ToString());
                        home.Disconect();
                        this.Close();
                    }
                    else
                    {
                        Alert.ShowDialog(Application.Current.Resources["lbDeleteAccountFailed"].ToString(), Application.Current.Resources["btOk"].ToString());
                    }
                }
                catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException )
                {
                    Logs.Error($"Fallo la conexión ({ ex.Message})");

                    Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                    Login login = new Login();
                    backHome = false;
                    this.Close();
                    login.Show();
                }
            }
        }

        /// <summary>
        /// Inicializa el servidor y llama a los metodos que dependen de el.
        /// </summary>
        /// <param name="server"></param>
        internal void SetServer(PlayerMgtClient server)
        {
            this.server = server;
            try
            {

                server.SearchInfoPlayerByUsername(username);
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException )
            {
                Logs.Error($"Fallo la conexión ({ ex.Message})");

                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                Login login = new Login();
                backHome = false;
                this.Close();
                login.Show();
            }
        }

        /// <summary>
        /// Inicializa la variable username.
        /// </summary>
        /// <param name="username"></param>
        internal void SetUsername(string username) => this.username = username;

        /// <summary>
        /// Inicializa la variable home.
        /// </summary>
        /// <param name="home"></param>
        internal void SetHome(Home home) => this.home = home;

        /// <summary>
        /// Inicializa los datos del jugador en los textbox.
        /// </summary>
        /// <param name="player">Jugador conectado actualmente.</param>
        public void SetPlayer(Player player)
        {
            tbName.Text = player.Name;
            tbUsername.Text = player.Username;
            tbEmail.Text = player.Email;
            pbPassword.Password = "";
        }

        /// <summary>
        /// Controlador del botón para cerrar la ventana. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            if (backHome)
            {
                home.Show();
            }
        }

    }
}
