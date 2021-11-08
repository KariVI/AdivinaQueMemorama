using AdivinaQue.Client.Control;
using System;
using System.Windows;

namespace AdivinaQue.Client.Views
{
   
    public partial class Register : Window
    {

        Proxy.ServiceClient server;
        private String email;


        public Register(Proxy.ServiceClient server, String email)
        {

            InitializeComponent();
            this.server = server;
            this.email = email;

        }
        private void RegisterBt_Click(object sender, RoutedEventArgs e)
        {

            if (tbUsername.Text != "" && Password.Password.ToString() != "" && tbName.Text != "")
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
                MessageBox.Show("Exist empty fields");
            }
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
            if(dataStatus == DataStatus.UserNameInvalid)
            {
                MessageBox.Show("Please write a valid username");
            }

            if(dataStatus == DataStatus.NameInvalid)
            {
                MessageBox.Show("Name field doesn't have special characters");
            }

            if (dataStatus == DataStatus.PasswordInvalid)
            {
                MessageBox.Show("Password field doesn't have special characters");
            }

            if (dataStatus == DataStatus.ShortPassword)
            {
                MessageBox.Show("Password minimum 8 characters");
            }

            if (dataStatus == DataStatus.UserNameDuplicate)
            {
                MessageBox.Show("This username already exists");
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

        public void RegisterUser()
        {

            Proxy.Player player = new Proxy.Player();
            player.Username = tbUsername.Text;
            player.Password = Password.Password.ToString();
            player.Name = tbName.Text;
            player.Email = email;


            server.Register(player);
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

    public enum DataStatus
    {
        Correct = 0,
        UserNameInvalid,
        NameInvalid, 
        PasswordInvalid,
        UserNameDuplicate,
        ShortPassword
    }
}
