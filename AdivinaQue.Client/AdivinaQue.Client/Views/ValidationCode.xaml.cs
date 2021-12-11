using AdivinaQue.Client.Control;
using AdivinaQue.Client.Logs;
using log4net;
using System;

using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace AdivinaQue.Client.Views
{
    public partial class ValidationCode : Window
    {
        private String codeExpected;

        Proxy.PlayerMgtClient serverPlayer;
        private static readonly ILog Logs = Log.GetLogger();


        public String CodeExpected { get { return codeExpected; } set { codeExpected = value; } }
        public ValidationCode(Proxy.PlayerMgtClient server)
        {
            
            this.serverPlayer = server;
            InitializeComponent();
        }

        private void EnterBt_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCode.Text )  && !IsVoid())
            {
                string codeReceived = tbCode.Text;
                if (codeReceived.Equals(codeExpected))
                {
                    Alert.ShowDialog(Application.Current.Resources["lbCorrectEmail"].ToString(), Application.Current.Resources["btOk"].ToString());
                    Register register = new Register(serverPlayer, tbEmail.Text);
                    this.Close();
                    register.Show();

                }
                else
                {
                    Alert.ShowDialog(Application.Current.Resources["lbIncorrectCode"].ToString(), Application.Current.Resources["btOk"].ToString());
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
            if(string.IsNullOrWhiteSpace(tbEmail.Text)){
                value = true;
            }
            return value;
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
            if (!string.IsNullOrEmpty(tbEmail.Text) )
            {
                Validate validate = new Validate();
                if (validate.ValidationEmail(tbEmail.Text)){
                    if (!SearchDuplicateEmail())
                    {
                        String code = GenerateCodeValidation();
                        string message = Application.Current.Resources["lbEmailCodeMessage"].ToString();
                        codeExpected = code;
                        string body = @"<style>
                                            h2{color:#E267B4;}
                                            </style>
                                            <h2>" + message + ": " + code + "</h2>";

                        string subject = Application.Current.Resources["lbEmailCodeSubject"].ToString();

                        String messageEmailSuccesful="Error";
                        try
                        {
                            messageEmailSuccesful = serverPlayer.SendMail(tbEmail.Text, subject, body);
                            if (messageEmailSuccesful == "Exito")
                            {

                                Alert.ShowDialog(Application.Current.Resources["lbCodeSended"].ToString(), Application.Current.Resources["btOk"].ToString());
                            }
                            else
                            {
                                Alert.ShowDialog(Application.Current.Resources["lbEmailSendError"].ToString(), Application.Current.Resources["btOk"].ToString());

                            }
                        }
                        catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException)
                        {
                            Logs.Error($"Fallo la conexión ({ ex.Message})");
                        }


                       
                    }
                    else
                    {
                        Alert.ShowDialog(Application.Current.Resources["lbDuplicateEmail"].ToString(), Application.Current.Resources["btOk"].ToString());
   
                    }
                } else
                {
                    Alert.ShowDialog(Application.Current.Resources["lbIncorrectEmail"].ToString(), Application.Current.Resources["btOk"].ToString());

                }
            }
            else
            {
                Alert.ShowDialog(Application.Current.Resources["lbVoidEmail"].ToString(), Application.Current.Resources["btOk"].ToString());
               
            }
            
        }

        private bool SearchDuplicateEmail() {
            bool value = false;
            
            try
            {
                string[] emails=emails = serverPlayer.GetEmails();
                foreach (var email in emails)
                {
                    if (email.Equals(tbEmail.Text))
                    {
                        value = true;
                    }
                }
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException)
            {
                Logs.Error($"Fallo la conexión ({ ex.Message})");
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                this.Close();
            }
           
                         
            return value;
        }
    }
}