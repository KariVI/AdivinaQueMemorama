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
    public partial class ValidationCode : Window
    {
        private String codeExpected;

        Proxy.ServiceClient server;

        public String CodeExpected { get { return codeExpected; } set { codeExpected = value; } }
        public ValidationCode(Proxy.ServiceClient server)
        {
            this.server = server;
            InitializeComponent();
        }

        private void EnterBt_Click(object sender, RoutedEventArgs e)
        {
            if (tbCode.Text != "")
            {
                string codeReceived = tbCode.Text;
                if (codeReceived.Equals(codeExpected))
                {
                    MessageBox.Show("Email validate");
                    Register register = new Register(server, tbEmail.Text);
                    this.Close();
                    register.Show();

                }
                else
                {
                    MessageBox.Show("Please write the correct code ");
                }
            }
            else
            {
                MessageBox.Show("Please write a code");
            }
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

        private void SendCodeBt_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text != "")
            {
                Validate validate = new Validate();
                if (validate.ValidationEmail(tbEmail.Text)){
                    if (!SearchDuplicateEmail())
                    {
                        String code = GenerateCodeValidation();
                        codeExpected = code;
                        string body = @"<style>
                                            h2{color:#E267B4;}
                                            </style>
                                            <h2>" + code + "</h2>";
                        String messageEmailSuccesful = server.SendMail(tbEmail.Text, "Código de validación", body);

                        if (messageEmailSuccesful == "Exito")
                        {

                            MessageBox.Show("Code sended");
                        }
                    }
                    else
                    {
                        MessageBox.Show("This email already exists , try again ");
                    }
                } else
                {
                    MessageBox.Show("Please write a correct email");
                }
            }
            else
            {
                MessageBox.Show("Please write an email");
            }
            
        }

        private bool SearchDuplicateEmail() {
            bool value = false;
            string[] emails = server.GetEmails();
                foreach(var email in emails ){  
                    if(email.Equals(tbEmail.Text)){ 
                        value=true;
                    }
                }
                         
            return value;
        }
    }
}