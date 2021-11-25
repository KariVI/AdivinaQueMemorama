using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Lógica de interacción para Alert.xaml
    /// </summary>
    public partial class Alert : Window
    {
        AlertResult result;
        private Alert()
        {
            InitializeComponent();
        }
        public static AlertResult ShowDialog( string message, string button1Text, string button2Text)
        {
            Alert messageBox = new Alert();
            return messageBox.ShowDialogInternal( message, button1Text, button2Text);
        }
        public static void ShowDialog(string message, string button1Text)
        {
            Alert messageBox = new Alert();
            messageBox.ShowDialogInternal(message, button1Text);
        }

        public AlertResult ShowDialogInternal(string message, string button1Text, string button2Text)
        {
            lbMessage.Content = message;
            btNo.Content = button1Text;
            btYes.Content = button2Text;
            ShowDialog();
            return result;
        }

        public void ShowDialogInternal(string message, string button1Text)
        {
            lbMessage.Content = message;
            btNo.Opacity = 0;
            btNo.IsEnabled = false;
            btYes.Content = button1Text;
            ShowDialog();
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            result = AlertResult.No;
            Close();
        }

        private void yes_Click(object sender, RoutedEventArgs e)
        {
            result = AlertResult.Yes;
            Close();
        }

      
    }

    public enum AlertResult
    {
        No = 1,
        Yes = 2,
    }
}
