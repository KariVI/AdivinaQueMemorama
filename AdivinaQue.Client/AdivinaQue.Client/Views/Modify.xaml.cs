using AdivinaQue.Client.Control;
using AdivinaQue.Client.Proxy;
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
        private ServiceClient server;
        private Home home;
        private bool backHome = true;

        public Modify(CallBack callback,Home home)
        {      
            InitializeComponent();
            newPlayer = new Player();
            this.callback = callback;
            this.home = home;
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUsername.Text) && !string.IsNullOrEmpty(pbPassword.Password.ToString() ) && !string.IsNullOrEmpty(tbName.Text ) &&
                !string.IsNullOrEmpty(tbEmail.Text) && !IsVoid())
            {
                if (ValidateData() == DataStatus.Correct)
                {
                    newPlayer.Name = tbName.Text.Trim();
                    newPlayer.Username = tbUsername.Text.Trim();
                    newPlayer.Email = tbEmail.Text.Trim();
                    newPlayer.Password = pbPassword.Password;
     
                    try
                    {
                        String code = server.GenerateCode();
                        server.SendMail(tbEmail.Text, "Modify confirm", "Ingrese el codigo en la aplicacion: " + code);
                        AuthMail authmail = new AuthMail(code, newPlayer);
                        authmail.setServer(server);
                        authmail.setUsername(username);
                        authmail.Show();
                    }
                    catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException)
                    {
                        Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                        Login login = new Login();
                        backHome = false;
                        login.Show();
                    }
                    this.Close();
                }
            }
            else
            {
                Alert.ShowDialog(Application.Current.Resources["lbEmptyFields"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
        }

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

        private bool SearchDuplicateUsername()
        {
            bool value = false;
            string[] usernames = server.GetUsers();
            foreach (var username in usernames)
            {
                if (username.Equals(tbUsername.Text))
                {
                    value = true;
                }
            }

            return value;
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            home.Show();
        }
        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var option = Alert.ShowDialog(Application.Current.Resources["lbDeleteAccount"].ToString(), Application.Current.Resources["btNo"].ToString(), Application.Current.Resources["btYes"].ToString());
            if (option == AlertResult.Yes)
            {
                try
                {
                    if (server.Delete(username))
                    {
                        Alert.ShowDialog(Application.Current.Resources["lbDeleteAccountCorrect"].ToString(), Application.Current.Resources["btOk"].ToString());
                        home.disconect();
                        this.Close();
                    }
                    else
                    {
                        Alert.ShowDialog(Application.Current.Resources["lbDeleteAccountFailed"].ToString(), Application.Current.Resources["btOk"].ToString());
                    }
                }
                catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException)
                {
                    Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                    Login login = new Login();
                    backHome = false;
                    this.Close();
                    login.Show();
                } 
            }   
        }

        internal void SetServer(ServiceClient server)
        {
            this.server = server;
            try
            {
                server.SearchInfoPlayerByUsername(username);
            }
            catch (Exception ex) when(ex is EndpointNotFoundException || ex is TimeoutException)
            {
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                Login login = new Login();
                backHome = false;
                this.Close();
                login.Show();
            }  
        }

        internal void SetUsername(string username)
        {
            this.username = username;
        }

        internal void SetHome(Home home)
        {
            this.home = home;
        }

        public void SetPlayer(Player player)
        {
            tbName.Text =player.Name;
            tbUsername.Text = player.Username;
            tbEmail.Text = player.Email;
            pbPassword.Password = "";
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            if(backHome){
                home.Show();
            }
        }
    }
}
