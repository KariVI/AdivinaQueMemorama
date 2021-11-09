using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            Console.WriteLine(sizeBoard);
            wpCards = new WrapPanel();
          
            InitializeComponent();
            addButton();

        }

        public void SetUsername(string username)
        {
            this.username = username;
          
        }

        public void SetUsernameRival(string usernameRival)
        {
            this.usernarmeRival = usernameRival;
            
        }
        public void addButton()
        {

            for (int i = 0; i < (sizeBoard * sizeBoard); i++)
            {

                Button bt = new Button();

                bt.Click += new RoutedEventHandler(button_onclick);
                bt.Width = 639 / sizeBoard;
                bt.Height = 624 / sizeBoard;
                bt.Background = Brushes.LavenderBlush;
                wpCards.Children.Add(bt);
            
            }
        }

        public void button_onclick(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            Image buttonAuxiliar = new Image();
            buttonAuxiliar.Source = new BitmapImage(new Uri(@"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name
+ ";component/"
+ "images/testImage2.jpg", UriKind.Absolute));
            bt.Content = buttonAuxiliar;
        }
    }
    }

