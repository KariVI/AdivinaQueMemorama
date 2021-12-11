using System;
using System.Windows;
using System.Windows.Threading;

namespace AdivinaQue.Client.Views
{
    /// <summary>
    /// Lógica de interacción para Alert.xaml
    /// </summary>
    public partial class Alert : Window
    {
        AlertResult result;
        private static DispatcherTimer timer;
        Alert alert;
        public AlertResult Result { get => result; set => result = value; }

        private Alert()
        {
            alert = this;
                InitializeComponent();
           

        }
        public static AlertResult ShowDialog( string message, string button1Text, string button2Text)
        {
            Alert messageBox = new Alert();
            return messageBox.ShowDialogInternal( message, button1Text, button2Text);
        }
        public static AlertResult ShowDialogWithResponse(string message, string button1Text)
        {
            Alert messageBox = new Alert();
            return messageBox.ShowDialogInternal(message, button1Text);
        }
        public static void ShowDialog(string message, string button1Text)
        {
            Alert messageBox = new Alert();
            messageBox.ShowDialogInternal(message, button1Text);
        }

        public AlertResult ShowDialogInternal(string message, string button1Text, string button2Text)
        {
            tbMessage.Text = message;
            btNo.Content = button1Text;
            btYes.Content = button2Text;          
            SetTimer(); 
            timer.Start();
            ShowDialog();
            return result;
        }

        public AlertResult ShowDialogInternal(string message, string button1Text)
        {
            tbMessage.Text = message;
            btNo.Opacity = 0;
            btNo.IsEnabled = false;
            btYes.Content = button1Text;
            SetTimer();
            timer.Start();
            ShowDialog();
            return result;
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            result = AlertResult.No;
            Close();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            result = AlertResult.Yes;
            timer.Stop();
            Close();
        }
        private void SetTimer()
        {        
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += OnTick;
        }
        private int time = 0;
        private  void OnTick(object sender, EventArgs e)
        {
               time++;
                lbTime.Content = "("+ (40 - time).ToString()+")";
               if(time == 40) { 
                alert.result = AlertResult.Unavaible;
                alert.Close();
                timer.Stop();
            }
        }
        public  void CloseAlert()
        {
            this.Close();
        }
    }

    public enum AlertResult
    {
        No = 1,
        Yes = 2,
        Unavaible = 3,
    }
   
    
}
