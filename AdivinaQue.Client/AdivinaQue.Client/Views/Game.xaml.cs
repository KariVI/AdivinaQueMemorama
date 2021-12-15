﻿using AdivinaQue.Client.Logs;
using AdivinaQue.Client.Proxy;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace AdivinaQue.Client.Views
{
    public partial class Game : Window
    {
        private int sizeBoard;
        private string category;
        private string username;
        private string usernameRival;
        private bool endGame = false;
        private int totalCards;
        private readonly int TOTAL_CARDS_DESIGN = 48;
        private readonly int TOTAL_CARDS_TESTS = 44;
        private readonly int TOTAL_CARDS_ADMIN = 40;
        private int column;
        private int row;
        public int ScorePlayer { set; get; }
        public int ScoreRival { set ; get; }
        public int NumberCardsFinded { set ; get; }
        Dictionary<BitmapImage, BitmapImage> pairCards = new Dictionary<BitmapImage, BitmapImage>();
        private List<BitmapImage> totalImages = new List<BitmapImage>();
        private List<Button> buttons = new List<Button>();
        private static Dictionary<BitmapImage, string> UpCards = new Dictionary<BitmapImage, string>();
        public Dictionary<BitmapImage, string> UpCardRival { set; get; }
        public Dictionary<BitmapImage, string> UpCardsRival { set; get; }
        public Dictionary<string, BitmapImage> GameCards { set; get; }
        private static readonly ILog Logs = Log.GetLogger();

        public bool NextTurn { set; get; }

        public Home home { get; internal set; }

        private int[] randomImageList;
        private int[] randomPositionList;
        private static DispatcherTimer timer;
        Proxy.PlayerMgtClient serverPlayer;
         Proxy.GameMgtClient server;
        private bool backHome = true;
        private static DispatcherTimer timerButton;
     

        public Game(Proxy.GameMgtClient server, int sizeBoard, string category)
        {
            this.server = server;
            this.sizeBoard = sizeBoard;
            this.category = category;
            wpCards = new WrapPanel();
            tbRivalScore = new TextBox();
            tbPlayerScore = new TextBox();
            ScorePlayer = 0;
            ScoreRival = 0;
            UpCardRival = new Dictionary<BitmapImage, string>();
            UpCardsRival = new Dictionary<BitmapImage, string>();
            GameCards = new Dictionary<string, BitmapImage>();

            NumberCardsFinded = 0;
            NextTurn = false;
            UpdateSizes();

            if (category == "Diseño")
            {
                totalCards = TOTAL_CARDS_DESIGN;
            }else if (category == "Pruebas")
            {
                totalCards = TOTAL_CARDS_TESTS;
            }else
            {
                totalCards = TOTAL_CARDS_ADMIN;
            }

            InitializeComponent();
            SetTimer(this);
            SetTimerButton(this);
        }

        public void SetServerPlayer(Proxy.PlayerMgtClient playerMgtClient)
        {
            serverPlayer = playerMgtClient;
        }

        private void UpdateSizes()
        {
            if (sizeBoard == 12)
            {
                row = 4;
                column = 3;
            }else if (sizeBoard==16)
            {
                row = 4;
                column = 4;
            }else if (sizeBoard == 20)
            {
                row = 5;
                column = 4;
            }else if (sizeBoard == 30)
            {
                row = 6;
                column = 5;
            }
            else
            {
                row = 6;
                column = 6;
            }
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

            lbUserName.Content = username;

        }

        public void SetUsernameRival(string usernameRival)
        {
            this.usernameRival = usernameRival;
            lbRivalScore.Content = usernameRival;

        }
        public void AddButton()
        {

            for (int i = 0; i < sizeBoard ; i++)
            {
                Button bt = new Button();

                bt.Click += new RoutedEventHandler(Button_Onclick);
                bt.Width =  (double) 639 / column;
                bt.Height = (double) 624 / row;
                Color color = (Color)ColorConverter.ConvertFromString("#CCCCFF");
                bt.Background = new SolidColorBrush(color);
                bt.Content = null;
                string btName = "bt" + i.ToString();
                bt.Name = btName;

                wpCards.Children.Add(bt);
                buttons.Add(bt);

            }
        }

        public void GetImages()
        {


            for (int i = 1; i <= (totalCards / 2); i++)
            {
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
            
            int indexButton = 0;
            for (int i = 0; i < (sizeBoard/ 2); i++)
            {
                int index = randomImageList[i];
                btName = "bt" + randomPositionList[indexButton].ToString();
                indexButton++;
                GameCards.Add(btName, totalImages[index]);
                btName = "bt" + randomPositionList[indexButton].ToString();
                indexButton++;
                GameCards.Add(btName, pairCards[totalImages[index]]);

            }

        }

        internal void ShowWinner(string winner)
        {
            timer.Stop();
            if (winner.Equals("both"))
            {
                Alert.ShowDialogWithResponse(Application.Current.Resources["lbTie"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
            else if (winner.Equals(username))
            {
                Alert.ShowDialogWithResponse(Application.Current.Resources["lbWin"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
            else 
            {
                Alert.ShowDialogWithResponse(Application.Current.Resources["lbLost"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
            endGame = true;
            server.DisconnectPlayers(username, usernameRival);
            this.Close();
        }

        internal void SetCorrectCards(Dictionary<BitmapImage, string> cards)
        {
            Button btCard1 = GetButton(cards.Values.First());
            Button btCard2 = GetButton(cards.Values.ElementAt(1));
            Image buttonAuxiliar1 = new Image();
            Image buttonAuxiliar2 = new Image();
            buttonAuxiliar1.Source = GameCards[btCard1.Name];
            btCard1.Content = buttonAuxiliar1;
            buttonAuxiliar2.Source = GameCards[btCard2.Name];
            btCard2.Content = buttonAuxiliar2;
            btCard1.Name = "blocked";
            btCard2.Name = "blocked";

        }
        public bool VerifyTurn()
        {
           
            bool correct = false;
            if (pairCards.ContainsKey(UpCards.Keys.First()))
            {

                if (pairCards[UpCards.Keys.First()].Equals(UpCards.Keys.ElementAt(1)))
                {
                    lbMessage.Text = Application.Current.Resources["correctPair"].ToString();
                    correct = true;
                    
                }
            }
            else if (pairCards.ContainsKey(UpCards.Keys.ElementAt(1)) &&
               pairCards[UpCards.Keys.ElementAt(1)].Equals(UpCards.Keys.First()) )
            {               
                    lbMessage.Text = Application.Current.Resources["correctPair"].ToString();
                    correct = true;
                
            }
            return correct;
        }

       public  void UpdateBoard()
        {
            bool correct = VerifyTurn();            
            Button btCard1 = GetButton(UpCards.Values.First());
            Button btCard2 = GetButton(UpCards.Values.ElementAt(1));

            if (correct)
            {
                server.SendCorrectCards(usernameRival, UpCards);
                ScorePlayer++;
                server.SendScoreRival(usernameRival, ScorePlayer);
                NumberCardsFinded = NumberCardsFinded + 2;
                server.SendNumberCardsFinded(usernameRival, NumberCardsFinded);
                tbPlayerScore.Text = Convert.ToString(ScorePlayer);
                btCard1.Name = "blocked";
                btCard2.Name = "blocked";
            }
            else
            {
                NextTurn = false;
                lbMessage.Text = Application.Current.Resources["incorrectPair"].ToString();
                Thread.Sleep(1000);
                server.SendNextTurnRival(usernameRival, true);
                btCard1.Content = null;
                btCard2.Content = null;
            }
           
            
            if (NumberCardsFinded == GameCards.Count)
            {
                AssignWinner();
            }
            UpCards = new Dictionary<BitmapImage, string>();
            timerButton.Start();
          

        }

        public Button GetButton(string name)
        {
            int i = 0;
            while (i < buttons.Count() && buttons[i].Name != name)
            {
                i++;
            }
            return buttons[i];
        }
        
        public void Button_Onclick(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if (NumberCardsFinded != GameCards.Count )
            {
                if (NextTurn && UpCards.Count < 2)
                {             
                    Image buttonAuxiliar = new Image();
                    if (bt.Name != "blocked" && bt.Content == null)
                    {
                        
                            
                            GameCards[bt.Name].DecodePixelWidth= 639/column;
                            GameCards[bt.Name].DecodePixelHeight = 624 / row;
                            buttonAuxiliar.Source = GameCards[bt.Name];
                            bt.Content = buttonAuxiliar;
                            try { 
                                 server.SendCardTurn(usernameRival,GameCards[bt.Name], bt.Name);
                             }
                            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException)
                            {
                                Logs.Error($"Fallo la conexión ({ ex.Message})");
                                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                                backHome = false;
                                endGame = false;
                                this.Close();
                                Login login = new Login();
                                login.Show();

                                
                            }
                            UpCards.Add(GameCards[bt.Name], bt.Name);
                    }
                }
                else
                {
                    lbMessage.Text = Application.Current.Resources["lbTurn"].ToString();
                   
                }
            }
            

        }

        public void AssignWinner()
        {
           
            string winner = "";
            DateTime thisDay = DateTime.Now;
            GameCurrently gameCurrently = new GameCurrently();
            gameCurrently.Date = thisDay.ToString();
            gameCurrently.Topic = category;
            gameCurrently.Players = new Dictionary<string, int>();
            gameCurrently.Players.Add(username, ScorePlayer);
            gameCurrently.Players.Add(usernameRival, ScoreRival);

            if (ScorePlayer > ScoreRival)
            {
                winner = username;
                gameCurrently.Winner = username;
                gameCurrently.ScoreWinner = ScorePlayer;
            }
            else if(ScoreRival > ScorePlayer)
            {
                winner = usernameRival;
                gameCurrently.Winner = usernameRival;
                gameCurrently.ScoreWinner = ScoreRival;
            }
            else
            {
                winner = "both";
                gameCurrently.Winner = winner;
            }

            server.SendGame(gameCurrently);
            server.SendWinner(usernameRival, winner);
            ShowWinner( winner);

        }

        public void TurnRivalSelection()
        {
            Button btCard1 = GetButton(UpCardRival.Values.First());
            Image buttonAuxiliar1 = new Image();
            buttonAuxiliar1.Source = GameCards[btCard1.Name];
            btCard1.Content = buttonAuxiliar1;

        }

        public void TurnOffRivalCards()
        {
            if (UpCardsRival.Count == 2)
            {
                
                    Button btCard1 = GetButton(UpCardsRival.Values.First());
                    Button btCard2 = GetButton(UpCardsRival.Values.ElementAt(1));
                    btCard1.Content =null ;
                    btCard2.Content = null;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!endGame)
            {
                DateTime thisDay = DateTime.Now;
                GameCurrently gameCurrently = new GameCurrently();
                gameCurrently.Date = thisDay.ToString();
                gameCurrently.Topic = category;
                gameCurrently.Players = new Dictionary<string, int>();
                gameCurrently.Players.Add(username, ScorePlayer);
                gameCurrently.Players.Add(usernameRival, ScoreRival);
                gameCurrently.Winner = usernameRival;
                gameCurrently.ScoreWinner = ScorePlayer;
                server.SendGame(gameCurrently);
                server.SendWinner(usernameRival, usernameRival);
            }
            if (backHome)
            {
                home.Show();
            }
            
            server.DisconnectPlayers(username, usernameRival);
            
            timer.Stop();
        }

        public static void SetTimer(Game game)
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(30) };
            timer.Tick += delegate {
              var response =  Alert.ShowDialogWithResponse(Application.Current.Resources["lbAvailability"].ToString(), Application.Current.Resources["btYes"].ToString());
                if(response == AlertResult.Unavaible)
                {
                    game.Close();
                    timer.Stop();
                }
                else
                {
                    timer.Stop();
                    timer.Start();
                }
            };
            timer.Start();
        }

        public static void SetTimerButton(Game game)
        {
            timerButton = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timerButton.Tick += delegate {
                timerButton.Stop();
                if (UpCards.Count == 2)
                {
                    game.UpdateBoard();
                }
                timerButton.Start();
            };
            timerButton.Start();
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            timer.Stop();
            timer.Start();
        }
    }
}

