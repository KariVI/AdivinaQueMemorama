using AdivinaQue.Client.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AdivinaQue.Client
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Controlador del evento StartUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void Aplication_StartUp(object sender, StartupEventArgs eventArgs) {
            Login login = new Login();
            login.ShowDialog(); 
        }        
    }
}
