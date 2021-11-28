﻿using AdivinaQue.Client.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
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
        private int scorePlayer;
        private int scoreRival;
        private int numberCardsFinded;
        private bool endGame = false;
        public int ScorePlayer { set { scorePlayer = value; } get { return scorePlayer; } }
        public int ScoreRival { set { scoreRival = value; } get { return scoreRival; } }
        public int NumberCardsFinded { set { numberCardsFinded = value; } get { return numberCardsFinded; } }
        Dictionary<BitmapImage, BitmapImage> pairCards = new Dictionary<BitmapImage, BitmapImage>();
        private List<BitmapImage> totalImages = new List<BitmapImage>();
        private List<Button> buttons = new List<Button>();
        private Dictionary<BitmapImage, string> upCards = new Dictionary<BitmapImage, string>();
        public Dictionary<BitmapImage, string> upCardRival = new Dictionary<BitmapImage, string>();
        public Dictionary<BitmapImage, string> upCardsRival = new Dictionary<BitmapImage, string>();
        public Dictionary<string, BitmapImage> gameCards = new Dictionary<string, BitmapImage>();
        private bool nextTurn;
        public bool NextTurn { set { nextTurn = value; } get { return nextTurn; } }

        public Home home { get; internal set; }

        private int[] randomImageList;
        private int[] randomPositionList;
        private static DispatcherTimer timer;

        Proxy.ServiceClient server;
        private bool backHome = true;

        public Game(Proxy.ServiceClient server, int sizeBoard, string category)
        {
            this.server = server;
            this.sizeBoard = sizeBoard;
            this.category = category;
            wpCards = new WrapPanel();
            tbRivalScore = new TextBox();
            tbPlayerScore = new TextBox();
            scorePlayer = 0;
            scoreRival = 0;
            
            numberCardsFinded = 0;
            nextTurn = true;

            InitializeComponent();
            SetTimer(this);
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
            lbRival.Content = usernameRival;

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
            int indexButton = 0;
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
            this.Close();
        }

        internal void SetCorrectCards(Dictionary<BitmapImage, string> cards)
        {
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
        public bool VerifyTurn()
        {
            nextTurn = false;
            bool correct = false;
            if (pairCards.ContainsKey(upCards.Keys.First()))
            {

                if (pairCards[upCards.Keys.First()].Equals(upCards.Keys.ElementAt(1)))
                {

                    correct = true;
                }
            }
            else if (pairCards.ContainsKey(upCards.Keys.ElementAt(1)))
            {
                if (pairCards[upCards.Keys.ElementAt(1)].Equals(upCards.Keys.First()))
                {

                    correct = true;
                }
            }
            return correct;
        }

       public void UpdateBoard()
        {
            bool correct = VerifyTurn();            
            Button btCard1 = getButton(upCards.Values.First());
            Button btCard2 = getButton(upCards.Values.ElementAt(1));
            if (correct)
            {

                server.SendCorrectCards(usernameRival, upCards);
                scorePlayer++;
                server.SendScoreRival(usernameRival, scorePlayer);
                numberCardsFinded = numberCardsFinded + 2;
                server.SendNumberCardsFinded(usernameRival, numberCardsFinded);
                tbPlayerScore.Text = Convert.ToString(scorePlayer);


                btCard1.Name = "blocked";
                btCard2.Name = "blocked";
            }
            else
            {

                btCard1.Content = null;
                btCard2.Content = null;
            }
            server.SendNextTurnRival(usernameRival, true);


            if (numberCardsFinded == gameCards.Count)
            {
                AssignWinner();
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
            if (numberCardsFinded != gameCards.Count )
            {
                if (nextTurn)
                {             
                    Image buttonAuxiliar = new Image();
                    if (bt.Name != "blocked")
                    {
                        if (bt.Content == null)
                        {

                            buttonAuxiliar.Source = gameCards[bt.Name];
                            bt.Content = buttonAuxiliar;
                            server.SendCardTurn(usernameRival,gameCards[bt.Name], bt.Name);
                            upCards.Add(gameCards[bt.Name], bt.Name);
                            if (upCards.Count() == 2)
                            {
                                UpdateBoard();
                            }
                        }
                    }
                }
                else
                {
                    Alert.ShowDialogWithResponse(Application.Current.Resources["lbTurn"].ToString(), Application.Current.Resources["btOk"].ToString());
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
            gameCurrently.Players.Add(usernameRival, scoreRival);

            if (scorePlayer > scoreRival)
            {
                winner = username;
                gameCurrently.Winner = username;
                gameCurrently.ScoreWinner = scorePlayer;
            }
            else if(scoreRival > scorePlayer)
            {
                winner = usernameRival;
                gameCurrently.Winner = usernameRival;
                gameCurrently.ScoreWinner = scoreRival;
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

        public void turnRivalSelection()
        {
            Button btCard1 = getButton(upCardRival.Values.First());
            Image buttonAuxiliar1 = new Image();
            buttonAuxiliar1.Source = gameCards[btCard1.Name];
            btCard1.Content = buttonAuxiliar1;

        }

        public void turnOffRivalCards()
        {
            if (upCardsRival.Count() == 2)
            {
                
                    Button btCard1 = getButton(upCardsRival.Values.First());
                    Button btCard2 = getButton(upCardsRival.Values.ElementAt(1));
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
                gameCurrently.Players.Add(usernameRival, scoreRival);
                gameCurrently.Winner = usernameRival;
                gameCurrently.ScoreWinner = scorePlayer;
                server.SendGame(gameCurrently);
                server.SendWinner(usernameRival, usernameRival);
            }
            if (backHome)
            {
                home.Show();
            }
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

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            timer.Stop();
            timer.Start();
        }
    }
}

