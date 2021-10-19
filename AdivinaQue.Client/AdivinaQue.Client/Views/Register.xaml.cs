using AdivinaQue.Client.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdivinaQue.Client.Views
{
    /// <summary>
    /// Lógica de interacción para Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        private void RegisterBt_Click(object sender, RoutedEventArgs e)
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            Proxy.ServiceClient server = new Proxy.ServiceClient(context);
            if (tbUsername.Text != "" && Password.Password.ToString() != "" && tbEmail.Text != "" && tbName.Text != "")
            {
               // (string username, string password, string name, string email)


                    server.register(tbUsername.Text, Password.Password.ToString(), tbName.Text, tbEmail.Text);
                    MessageBox.Show("User register succesful ");
                    this.Close();
                    MessageBox.Show("Please write another username ");
                

            }
            else
            {
                MessageBox.Show("Exist empty fields");
            }
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
