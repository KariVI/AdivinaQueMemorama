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
    public partial class Game : Window
    {
        private int sizeBoard;
        private string category;
        private string username;
        private string usernarmeRival;
        
        public Game(int sizeBoard, string category)
        {
            this.sizeBoard = sizeBoard;
            wpCards = new WrapPanel();
            InitializeComponent();
            addButton();
        }

        public void SetUsername(string username)
        {
            this.username = username;
            lbUserName.Content = username;
        }

        public void SetUsernameRival(string usernameRival)
        {
            this.usernarmeRival = usernameRival;
            lbRival.Content = usernameRival;
        }
        public void addButton()
        {
            Button buttonAuxiliar;
            for (int i = 0; i < (sizeBoard * sizeBoard); i++)
            {
                buttonAuxiliar = new Button();
                buttonAuxiliar.Width = 100;
                buttonAuxiliar.Height = 70;
                buttonAuxiliar.Background = Brushes.PaleVioletRed;

                wpCards.Children.Add(buttonAuxiliar);
            }



        }
    }
}
