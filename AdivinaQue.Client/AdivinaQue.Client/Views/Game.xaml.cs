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
    /// Lógica de interacción para Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private int sizeBoard;
        public Game(int sizeBoard)
        {
            this.sizeBoard = sizeBoard;
            wpCards = new WrapPanel();
            InitializeComponent();
            addButton();
        }

        public void addButton() {
            Button buttonAuxiliar;
            for(int i=0; i<(sizeBoard * sizeBoard); i++)
            {
                buttonAuxiliar = new Button();
                buttonAuxiliar.Width = 100;
                buttonAuxiliar.Height = 70;
                buttonAuxiliar.Background = Brushes.Black; 
               
                wpCards.Children.Add(buttonAuxiliar);
            }
            


        }
    }
}
