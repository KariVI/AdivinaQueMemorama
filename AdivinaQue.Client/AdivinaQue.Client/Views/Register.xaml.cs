using AdivinaQue.Client.Control;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace AdivinaQue.Client.Views
{
   
    public partial class Register : Window
    {

        Proxy.PlayerMgtClient serverPlayer;
        private String email;


        public Register(Proxy.PlayerMgtClient server, String email)
        {
          
            this.serverPlayer = server;
            this.email = email;
            InitializeComponent();


        }
        private void RegisterBt_Click(object sender, RoutedEventArgs e)
        {

            if ( ! string.IsNullOrEmpty(tbUsername.Text)  && !string.IsNullOrEmpty(Password.Password.ToString()) && !string.IsNullOrEmpty(tbName.Text)  && !IsVoid() )
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
                MessageBox.Show("Exists empty fields");
            }
        }

        private bool IsVoid()
        {
            bool value = false;
            if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbUsername.Text) || string.IsNullOrWhiteSpace(Password.Password.ToString()))
            {
                value = true;
            }
            return value;
        }
        private DataStatus  ValidateData()
        {
            Validate validate = new Validate();
            DataStatus dataStatus = DataStatus.Correct;

            if (!validate.ValidationAlphanumeric(tbUsername.Text)) {
                dataStatus = DataStatus.UserNameInvalid;
            } else if (SearchDuplicateUsername())
            {
                dataStatus = DataStatus.UserNameDuplicate;
            }

            if (!validate.ValidationString(tbName.Text))
            {
                dataStatus = DataStatus.NameInvalid;
            }

            if (!validate.ValidationAlphanumeric(Password.Password.ToString()))
            {
                dataStatus = DataStatus.PasswordInvalid;
            }

            if(Password.Password.ToString().Length < 9)
            {
                dataStatus = DataStatus.ShortPassword;

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

            if (dataStatus == DataStatus.UserNameDuplicate)
            {
                Alert.ShowDialog(Application.Current.Resources["lbDuplicateUsername"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
        }

        private string[] ConvertUpperStrings()
        {
            int numberUsers = serverPlayer.GetUsers().Length;
            string[] usernames = new string[numberUsers];
            for (int i = 0; i < numberUsers; i++)
            {
                usernames[i] = serverPlayer.GetUsers()[i].ToUpper();

            }

            return usernames;

        }

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

        public void RegisterUser()
        {

            Proxy.Player player = new Proxy.Player();
            player.Username = tbUsername.Text.Trim();
            player.Password = Password.Password.ToString();
            player.Name = tbName.Text.Trim();
            player.Email = email;
            serverPlayer.Register(player);
            MessageBox.Show("Saved Data");
            this.Close();

        }


        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void closedWindow(object sender, EventArgs e)
        {
            this.Close();
        }
    }

  
}
