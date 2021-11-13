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
        private List<Button> buttons = new List<Button>();
        private Dictionary<BitmapImage,string> upCards = new Dictionary<BitmapImage,string>();
        Dictionary<string,BitmapImage> gameCards = new Dictionary<string, BitmapImage>();
        private int[] randomImageList;
        private int[] randomPositionList;
        Proxy.ServiceClient server;
        public Game(Proxy.ServiceClient server,int sizeBoard, string category)
        {
            this.server = server;
            this.sizeBoard = sizeBoard;
            this.category = category;
            wpCards = new WrapPanel();
            if(server == null)
                MessageBox.Show(" a", "Message", MessageBoxButton.YesNo);

            InitializeComponent();  

        }
        public void InitializeBoard()
        {
            GetImages();

            AddButton();
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
        public void AddButton()
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
                buttons.Add(bt);
            
            }
        }

        public void GetImages()
        {


            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine(category);
                string locationQuestion = "images/" + category + "/" + i + "-1.png";
                string locationAnswer = "images/" + category + "/" + i + "-2.png";

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
            InitializeBoard();
        }

        public void GetRandomCards()
        {
            string btName = "";
            Random randomPosition = new Random();
            int indexButton = 0 ;
            for (int i = 0; i < (sizeBoard * sizeBoard) / 2; i++)
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

        internal void SetCorrectCards(Dictionary<BitmapImage, string> cards)
        {
            Console.WriteLine("sss");
            Console.WriteLine("sss");
            Button btCard1 = getButton(cards.Values.First());
            Button btCard2 = getButton(cards.Values.ElementAt(1));
            Image buttonAuxiliar1 = new Image();
            Image buttonAuxiliar2 = new Image();
            buttonAuxiliar1.Source = gameCards[btCard1.Name];
            btCard1.Content = buttonAuxiliar1;  
             buttonAuxiliar2.Source = gameCards[btCard2.Name];
            btCard2.Content = buttonAuxiliar2;
            btCard1.Name = "blocked";
            btCard2.Name = "blocked";
            
        }

        public void VerifyTurn()
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
            Button btCard1 = getButton(upCards.Values.First());
            Button btCard2 = getButton(upCards.Values.ElementAt(1));
            if (correct)
            {
               server.SendCorrectCards(usernarmeRival,upCards);
               var option = MessageBox.Show(" Yei", "Message", MessageBoxButton.YesNo);
                
                btCard1.Name = "blocked";
                btCard2.Name = "blocked";
            }
            else
            {
                var option = MessageBox.Show(" :(", "Message", MessageBoxButton.YesNo);
                btCard1.Content = null;
                btCard2.Content = null;
            }
            
            upCards = new Dictionary<BitmapImage, string>();

        }
        public Button getButton(string name)
        {
            int i = 0;
            while (i < buttons.Count() && buttons[i].Name != name)
            {
                i++;
            }
            return buttons[i];
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
                    upCards.Add(gameCards[bt.Name], bt.Name);
                    if (upCards.Count() == 2)
                    {
                        VerifyTurn();
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

