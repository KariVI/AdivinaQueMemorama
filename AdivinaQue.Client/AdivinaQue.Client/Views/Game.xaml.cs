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
        private Dictionary<BitmapImage,Button> upCards = new Dictionary<BitmapImage,Button>();
        Dictionary<string,BitmapImage> gameCards = new Dictionary<string, BitmapImage>();
        private int[] randomImageList;
        private int[] randomPositionList;
        public Game(int sizeBoard, string category)
        {
            this.sizeBoard = sizeBoard;
            this.category = category;
            wpCards = new WrapPanel();
            
          
            InitializeComponent();
           

        }
        public void initializeBoard()
        {
            getImages();

            addButton();
            GetRandomCards();
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
                bt.Content = null;
                string btName = "bt" + i.ToString();
                bt.Name = btName;
                wpCards.Children.Add(bt);
            
            }
        }

        public void getImages()
        {


            for (int i = 1; i < 4; i++)
            {
                Console.WriteLine(category);
                string locationQuestion = "images/" + category + "/" + i + "-1.png";
                string locationAnswer = "images/Diseño/" + i + "-2.png";

                BitmapImage imageQuestion = new BitmapImage(new Uri(@"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name
               + ";component/"
                 + locationQuestion, UriKind.Absolute));
                BitmapImage imageAnswer = new BitmapImage(new Uri(@"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name
           + ";component/"
             + locationAnswer, UriKind.Absolute));
                pairCards.Add(imageQuestion, imageAnswer);
                totalImages.Add(imageQuestion);


            }
        }
        public void SetRandomLists(int[] randomImageList, int[] randomPositionList)
        {
            this.randomImageList = randomImageList;
            this.randomPositionList = randomPositionList;
            initializeBoard();
        }

        public void GetRandomCards()
        {
            string btName = "";
            Random randomPosition = new Random();
            int indexButton = 0 ;
            for (int i = 0; i < sizeBoard; i++)
            {
                int index = randomImageList[i];
                btName = "bt" + randomPositionList[indexButton].ToString();
                indexButton++;                
                gameCards.Add(btName, totalImages[index]);
                btName = "bt" + randomPositionList[indexButton].ToString();
                indexButton++;
                gameCards.Add(btName, pairCards[totalImages[index]]);
              
            }
            
        }
        public void verifyTurn()
        {
            bool correct = false;
            if (pairCards.ContainsKey(upCards.Keys.First())) {

                if (pairCards[upCards.Keys.First()].Equals(upCards.Keys.ElementAt(1))){
                  
                    correct = true;
                }
            }
            else if (pairCards.ContainsKey(upCards.Keys.ElementAt(1)))
            {
                if (pairCards[upCards.Keys.ElementAt(1)].Equals(upCards.Keys.First())){
                    
                    correct = true;
                }
            }
            if(correct)
            {
                
                var option = MessageBox.Show(" Yei", "Message", MessageBoxButton.YesNo);
                upCards.Values.First().Name = "blocked";
                upCards.Values.ElementAt(1).Name = "blocked";
            }
            else
            {
                var option = MessageBox.Show(" :(", "Message", MessageBoxButton.YesNo);
                upCards.Values.First().Content = null;
                upCards.Values.ElementAt(1).Content = null;
            }
            
            upCards = new Dictionary<BitmapImage, Button>();

        }
        public void button_onclick(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            Image buttonAuxiliar = new Image();
            if (bt.Name != "blocked")
            {
                if (bt.Content == null)
                {

                    buttonAuxiliar.Source = gameCards[bt.Name];
                    bt.Content = buttonAuxiliar;
                    upCards.Add(gameCards[bt.Name], bt);
                    if (upCards.Count() == 2)
                    {

                        verifyTurn();
                    }
                }
                else
                {
                    bt.Content = null;
                    upCards.Remove(gameCards[bt.Name]);
                }
            }
           
        }
    }
    }

