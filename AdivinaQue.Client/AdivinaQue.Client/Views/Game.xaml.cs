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
       // private List<BitmapImage> imagesQuestions = new List<BitmapImage>();
        //private List<BitmapImage> imagesAnswers = new List<BitmapImage>();
        Dictionary<BitmapImage, BitmapImage> pairCards = new Dictionary<BitmapImage, BitmapImage>();
        private List<BitmapImage> totalImages = new List<BitmapImage>();

        public Game(int sizeBoard, string category)
        {
            this.sizeBoard = sizeBoard;
            this.category = category;
            wpCards = new WrapPanel();
            
          
            InitializeComponent();
            getImages();

            addButton();

        }

        public void SetUsername(string username)
        {
            this.username = username;
            lbPlayerScore.Content = username;

        }

        public void SetUsernameRival(string usernameRival)
        {
            this.usernarmeRival = usernameRival;
            lbRivalScore.Content = usernameRival;

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

        public void getImages()
        {


            for (int i = 1; i < 4; i++)
            {
                Console.WriteLine(category);
                string locationQuestion = "images/"+category+"/" + i +"-1.png";
                string locationAnswer = "images/Diseño/" + i + "-2.png";

                BitmapImage imageQuestion = new BitmapImage(new Uri(@"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name
               + ";component/"
                 + locationQuestion, UriKind.Absolute));
                BitmapImage imageAnswer = new BitmapImage(new Uri(@"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name
           + ";component/"
             + locationAnswer, UriKind.Absolute));
                pairCards.Add(imageQuestion, imageAnswer);
                totalImages.Add(imageQuestion);
                totalImages.Add(imageAnswer);

            }


        }
        public void button_onclick(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            Image buttonAuxiliar = new Image();
            Random random = new Random();
            int index = random.Next(totalImages.Count);
            buttonAuxiliar.Source = totalImages[index];
            bt.Content = buttonAuxiliar;
        }
    }
    }

