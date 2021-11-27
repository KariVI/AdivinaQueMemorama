using AdivinaQue.Client.Control;
using AdivinaQue.Client.Proxy;
using System;
using System.Windows;

namespace AdivinaQue.Client.Views
{
    /// <summary>
    /// Lógica de interacción para Modify.xaml
    /// </summary>
    public partial class Modify : Window
    {
        private string username;
        private Player player;
        private Player newPlayer;
        private ServiceClient server;
        private Home home;
        public Modify()
        {      
            InitializeComponent();
            newPlayer = new Player();
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUsername.Text) && !string.IsNullOrEmpty(pbPassword.Password.ToString() ) && !string.IsNullOrEmpty(tbName.Text ) &&
                !string.IsNullOrEmpty(tbEmail.Text) && !IsVoid())
            {
                if (ValidateData()== DataStatus.Correct)
                {
                    newPlayer.Name = tbName.Text.Trim();
                    newPlayer.Username = tbUsername.Text.Trim();
                    newPlayer.Email = tbEmail.Text.Trim();
                    newPlayer.Password = pbPassword.Password;
                    String code = server.GenerateCode();
                    server.SendMail(tbEmail.Text, "Modify confirm", "Ingrese el codigo en la aplicacion: " + code);
                    AuthMail authmail = new AuthMail(code, newPlayer);
                    authmail.setServer(server);
                    authmail.setUsername(username);
                    authmail.Show();
                    this.Close();
                }
                else
                {

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
                MessageBox.Show("Please write a valid username");
            }

            if (dataStatus == DataStatus.NameInvalid)
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

            if (dataStatus == DataStatus.EmailIncorrect)
            {
                MessageBox.Show("Email incorrect, please write again");
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
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            var option = MessageBox.Show("Delete user?", "Message", MessageBoxButton.YesNo);
            if(option == MessageBoxResult.Yes)
            {


                if (server.Delete(username))
                {
                    home.disconect();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Not be possible delete your account");
                }
            }   
        }

        private void closedWindow(object sender, EventArgs e)
        {
            this.Close();
        }

        internal void setServer(ServiceClient server)
        {
            this.server = server;
            server.SearchInfoPlayerByUsername(username);
        }

        internal void setUsername(string username)
        {
            this.username = username;
        }

        internal void setHome(Home home)
        {
            this.home = home;
        }

        public void setPlayer(Player player)
        {
            this.player = player;
            tbName.Text =player.Name;
            tbUsername.Text = player.Username;
            tbEmail.Text = player.Email;
            pbPassword.Password = player.Password;
        }


    }
}
