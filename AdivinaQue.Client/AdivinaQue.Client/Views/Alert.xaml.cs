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
        
        private static DispatcherTimer timer;
        Alert alert;
        private int time = 0;

        public AlertResult Result { get; set; }
        /// <summary>
        /// Inicializa una nueva instancia de Alert.xaml.
        /// </summary>
        private Alert()
        {
            alert = this;
            InitializeComponent();

        }

        /// <summary>
        ///  Muestra una alerta esperando una respueesta.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="button1Text"></param>
        /// <param name="button2Text"></param>
        /// <returns></returns>
        public static AlertResult ShowDialog(string message,  string button1Text, string button2Text)
        {
            Alert messageBox = new Alert();
            return messageBox.ShowDialogInternal( message, button1Text, button2Text);
        }

        /// <summary>
        /// Muestra una alerta esperando una respueesta.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="button1Text"></param>
        /// <returns></returns>
        public static AlertResult ShowDialogWithResponse(string message, string button1Text)
        {
            Alert messageBox = new Alert();
            return messageBox.ShowDialogInternal(message, button1Text);
        }

        /// <summary>
        /// Muestra una alerta sin esperar respuesta.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="button1Text"></param>
        public static void ShowDialog(string message, string button1Text)
        {
            Alert messageBox = new Alert();
            messageBox.ShowDialogInternal(message, button1Text);
        }

        /// <summary>
        /// Muestra un mensaje con los parametros.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="button1Text"></param>
        /// <param name="button2Text"></param>
        /// <returns>Alert result dependiendo de la respuesta del usuario.</returns>
        public AlertResult ShowDialogInternal(string message, string button1Text, string button2Text)
        {
            tbMessage.Text = message;
            btNo.Content = button1Text;
            btYes.Content = button2Text;          
            SetTimer(); 
            timer.Start();
            ShowDialog();
            return Result;
        }

        /// <summary>
        /// Muestra un mensaje con los parametros.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="button1Text"></param>
        /// <returns>Alert result dependiendo de la respuesta del usuario.</returns>
        public AlertResult ShowDialogInternal(string message, string button1Text)
        {
            tbMessage.Text = message;
            btNo.Opacity = 0;
            btNo.IsEnabled = false;
            btYes.Content = button1Text;
            SetTimer();
            timer.Start();
            ShowDialog();
            return Result;
        }

        /// <summary>
        /// Controlador del botón sí.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btNo_Click(object sender, RoutedEventArgs e)
        {
            Result = AlertResult.No;
            Close();
        }

        /// <summary>
        /// Controlador del botón sí.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtYes_Click(object sender, RoutedEventArgs e)
        {
            Result = AlertResult.Yes;
            timer.Stop();
            Close();
        }

        /// <summary>
        /// Inicializa el timer de la alerta.
        /// </summary>
        private  void SetTimer()
        {        
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += OnTick;
        }
        
        /// <summary>
        /// Controlador para el evento onTick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private   void OnTick(object sender, EventArgs e)
        {
               time++;
               lbTime.Content = "("+ (40 - time).ToString()+")";
               if(time == 40) { 
                alert.Result = AlertResult.Unavaible;
                alert.Close();
                timer.Stop();
               }
        }

    }

}
