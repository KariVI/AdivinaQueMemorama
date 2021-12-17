using AdivinaQue.Client.Control;
using AdivinaQue.Client.Logs;
using log4net;
using System;
using System.ServiceModel;
using System.Windows;

namespace AdivinaQue.Client.Views
{
    /// <summary>
    /// Lógica de interacción para Register.xaml.
    /// </summary>
    public partial class Register : Window
    {
        private Proxy.PlayerMgtClient serverPlayer;
        private string email;
        private static readonly ILog Logs = Log.GetLogger();

        /// <summary>
        /// Inicializa una nueva instancia de Register.xaml.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="email"></param>
        public Register(Proxy.PlayerMgtClient server, String email)
        {
            this.serverPlayer = server;
            this.email = email;
            InitializeComponent();
        }

        /// <summary>
        /// Controlador del botón para registrar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtRegister_Click(object sender, RoutedEventArgs e)
        {

            if ( ! string.IsNullOrEmpty(tbUsername.Text)  && !string.IsNullOrEmpty(pbPassword.Password.ToString()) && !string.IsNullOrEmpty(tbName.Text)  && !IsVoid() )
            {
                if (ValidateData() == DataStatus.Correct)
                {
                    RegisterUser();
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
        /// Verifica que los campos ingresados esten vacios.
        /// </summary>
        /// <returns>True si algún campo es vacío, false ningún campo es vacío</returns>
        private bool IsVoid()
        {
            bool value = false;
            if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbUsername.Text) || string.IsNullOrWhiteSpace(pbPassword.Password.ToString()))
            {
                value = true;
            }
            return value;
        }

        /// <summary>
        /// Valida los datos de registro ingresados en los textbox y passwordbox.
        /// </summary>
        /// <returns>Datastatus dependiento de los datos.</returns>
        private DataStatus  ValidateData()
        {
            Validate validate = new Validate();
            DataStatus dataStatus = DataStatus.Correct;

            if (!validate.ValidationAlphanumeric(tbUsername.Text.Trim())) {
                dataStatus = DataStatus.UserNameInvalid;
            } else if (SearchDuplicateUsername())
            {
                dataStatus = DataStatus.UserNameDuplicate;
            }

            if (!validate.ValidationString(tbName.Text.Trim()))
            {
                dataStatus = DataStatus.NameInvalid;
            }

            if (!validate.ValidationAlphanumeric(pbPassword.Password.ToString()))
            {
                dataStatus = DataStatus.PasswordInvalid;
            }

            if(pbPassword.Password.ToString().Length < 9)
            {
                dataStatus = DataStatus.ShortPassword;

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

            if (dataStatus == DataStatus.UserNameDuplicate)
            {
                Alert.ShowDialog(Application.Current.Resources["lbDuplicateUsername"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
        }

        /// <summary>
        /// Convierte los nombres de usuario de los usuarios registrados a mayusculas.
        /// </summary>
        /// <returns>Arrego de string con los username de los usuarios registrados en mayusculas</returns>
        private string[] ConvertUpperStrings()
        {
            int numberUsers=0;
            try
            {
                numberUsers=serverPlayer.GetUsers().Length;
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException)
            {
                Logs.Error($"Fallo la conexión ({ ex.Message})");
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                this.Close();
            }

            string[] usernames = new string[numberUsers];
            for (int i = 0; i < numberUsers; i++)
            {
                usernames[i] = serverPlayer.GetUsers()[i].ToUpper();

            }

            return usernames;

        }

        /// <summary>
        /// Verifica que el nombre de usuario ya se encuentre registrado.
        /// </summary>
        /// <returns>true si el nombre de usuario ya esta registrado, false en caso contrario.</returns>
        private bool SearchDuplicateUsername()
        {
            bool value = false;
            string[] usernames = ConvertUpperStrings();

            foreach (var username in usernames)
            {
                if (username.Equals(tbUsername.Text.ToUpper()))
                {
                    value = true;
                }
            }

            return value;
        }

        /// <summary>
        /// Envia el usuario al servidor para su registro.
        /// </summary>
        public void RegisterUser()
        {

            Proxy.Player player = new Proxy.Player();
            player.Username = tbUsername.Text.Trim();
            player.Password = pbPassword.Password.ToString();
            player.Name = tbName.Text.Trim();
            player.Email = email;
            try
            {
                serverPlayer.Register(player);
                Alert.ShowDialog(Application.Current.Resources["lbSavedData"].ToString(), Application.Current.Resources["btOk"].ToString());
                this.Close();
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException)
            {
                Logs.Error($"Fallo la conexión ({ ex.Message})");
             
            }
          

        }

        /// <summary>
        /// Cancela el registro.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Controlador del botón para cerrar la ventana.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closedWindow(object sender, EventArgs e)
        {
            this.Close();
        }
    }

  
}
