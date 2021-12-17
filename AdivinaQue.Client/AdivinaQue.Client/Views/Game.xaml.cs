using AdivinaQue.Client.Logs;
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
    /// <summary>
    /// Lógica de interacción para Game.xaml.
    /// </summary>
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
        private  Dictionary<BitmapImage, string> UpCards = new Dictionary<BitmapImage, string>();
        public Dictionary<BitmapImage, string> UpCardRival { set; get; }
        public Dictionary<BitmapImage, string> UpCardsRival { set; get; }
        public Dictionary<string, BitmapImage> GameCards { set; get; }
        private static readonly ILog Logs = Log.GetLogger();

        public bool NextTurn { set; get; }

        public Home home { get; internal set; }

        private int[] randomImageList;
        private int[] randomPositionList;
        private  DispatcherTimer timer;
        Proxy.PlayerMgtClient serverPlayer;
         Proxy.GameMgtClient server;
        private bool backHome = true;
        private  DispatcherTimer timerButton;
     
        /// <summary>
        /// Inicializa una nueva instancia de Game.xaml.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="sizeBoard"></param>
        /// <param name="category"></param>
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

            if (category == "Disenio")
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
            SetTurnTimer(this);
            //SetCardButtonTimer(this);
        }

        /// <summary>
        /// Inicializa el servidor del jugador.
        /// </summary>
        /// <param name="playerMgtClient"></param>
        public void SetServerPlayer(Proxy.PlayerMgtClient playerMgtClient)
        {
            serverPlayer = playerMgtClient;
        }

        /// <summary>
        /// Actualiza el tamaño del tablero.
        /// </summary>
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

        /// <summary>
        /// Inicializa el tablero.
        /// </summary>
        public void InitializeBoard()
        {
            GetImages();
            AddButton();
            GetRandomCards();
        }

        /// <summary>
        /// Inicialia el nombre de usuario del jugador actual.
        /// </summary>
        /// <param name="username"></param>
        public void SetUsername(string username)
        {
            this.username = username;
            lbPlayerScore.Content = username;

            lbUserName.Content = username;

        }

        /// <summary>
        /// Inicializa el nombre de usuario del rival.
        /// </summary>
        /// <param name="usernameRival"></param>
        public void SetUsernameRival(string usernameRival)
        {
            this.usernameRival = usernameRival;
            lbRivalScore.Content = usernameRival;

        }
        /// <summary>
        /// Añade los botones de las cartas al tablero.
        /// </summary>
        public void AddButton()
        {

            for (int i = 0; i < sizeBoard ; i++)
            {
                Button bt = new Button();

                bt.Click += new RoutedEventHandler(BtCard_Onclick);
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
        /// <summary>
        /// Recupera el total de imagenes de los recursos.
        /// </summary>
        public void GetImages()
        {


            for (int i = 1; i <= (totalCards / 2); i++)
            {
                string locationQuestion = "/images/" + category + "/" + i + "-1.png";
                string locationAnswer = "/images/" + category + "/" + i + "-2.png";

                BitmapImage imageQuestion = new BitmapImage(new Uri(locationQuestion, UriKind.Relative));
                BitmapImage imageAnswer = new BitmapImage(new Uri( locationAnswer, UriKind.Relative));
                pairCards.Add(imageQuestion, imageAnswer);
                totalImages.Add(imageQuestion);


            }
        }

        /// <summary>
        /// Inicializa la semilla(numeros aleatorios) del juego.
        /// </summary>
        /// <param name="randomImageList"></param>
        /// <param name="randomPositionList"></param>
        public void SetRandomLists(int[] randomImageList, int[] randomPositionList)
        {
            this.randomImageList = randomImageList;
            this.randomPositionList = randomPositionList;
            InitializeBoard();
        }

        /// <summary>
        /// Inicializa las cartas de acuerdo a la semilla de numeros aleatorios.
        /// </summary>
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
        /// <summary>
        /// Muestra el ganador en la GUI.
        /// </summary>
        /// <param name="winnerUsername">Nombre del ganador.</param>
        internal void ShowWinner(string winnerUsername)
        { 
            endGame = true;
            timer.Stop();
            if (winnerUsername.Equals("both"))
            {
                Alert.ShowDialog(Application.Current.Resources["lbTie"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
            else if (winnerUsername.Equals(username))
            {
                Alert.ShowDialog(Application.Current.Resources["lbWin"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
            else 
            {
                Alert.ShowDialog(Application.Current.Resources["lbLost"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
          
            this.Close();
        }

        /// <summary>
        /// Bloquea las cartas correctas.
        /// </summary>
        /// <param name="cards">Diccionario que contiene las cartas correctas.</param>
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

        /// <summary>
        /// Verifica las cartas del turno.
        /// </summary>
        /// <returns>True si las cartas se corresponden, false en caso contrario</returns>
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

        /// <summary>
        /// Actualiza el tablero dependiendo del turno jugado.
        /// </summary>
       public  void UpdateBoard()
        {
            if (DisponseButton(UpCards.Values.First()) && DisponseButton(UpCards.Values.ElementAt(1)))
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
                    
                    Alert.ShowDialogWithoutButton(Application.Current.Resources["incorrectPair"].ToString());

                    Thread.Sleep(100);
                    server.SendNextTurnRival(usernameRival, true);
                    btCard1.Content = null;
                    btCard2.Content = null;
                }


                if (NumberCardsFinded == GameCards.Count)
                {
                    AssignWinner();
                }
                UpCards.Clear();
            }
           




        }

        /// <summary>
        /// Conocer si el botón esta disponible o bloqueado
        /// </summary>
        /// <param name="name">Identificador del botón.</param>
        /// <returns>True si el botón esta disponible y false en caso contrario.</returns>
        public bool DisponseButton(string name)
        {
            bool value = false;
            int i = 0;
            while (i < buttons.Count() )
            {
               if(buttons[i].Name.Equals(name))
                {
                    value = true;
                }
                i++;
            }
            return value;
        }
        /// <summary>
        /// Obtiene el botón dependiendo de su identificador.
        /// </summary>
        /// <param name="name">Identificador del botón.</param>
        /// <returns>Botón que cumple con los parametros.</returns>
        public Button GetButton(string name)
        {
            int i = 0;
            while (i < buttons.Count() && buttons[i].Name != name)
            {
                i++;
            }
            return buttons[i];
        }

        /// <summary>
        /// Controlador del botón para voltear una carta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtCard_Onclick(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if (NumberCardsFinded != GameCards.Count )
            {
                if (NextTurn && UpCards.Count<=2)
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
                                UpCards.Add(GameCards[bt.Name], bt.Name);

                            if (UpCards.Count() == 2)
                                {
                                    UpdateBoard();
                                }

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
                    }
                }
                else
                {
                    lbMessage.Text = Application.Current.Resources["lbTurn"].ToString();
                   
                }
            }
            

        }
        /// <summary>
        /// Asigna un ganador.
        /// </summary>
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
            ShowWinner(winner);

        }

        /// <summary>
        /// Muestra las cartas que selecciono el rival.
        /// </summary>
        public void TurnRivalSelection()
        {
            if (DisponseButton(UpCardRival.Values.First()))
            {
                Button btCard1 = GetButton(UpCardRival.Values.First());
                Image buttonAuxiliar1 = new Image();
                buttonAuxiliar1.Source = GameCards[btCard1.Name];
                btCard1.Content = buttonAuxiliar1;
            }


        }

        /// <summary>
        /// Oculta las cartas que selecciono el rival.
        /// </summary>
        public void TurnOffRivalCards()
        {
            if (UpCardsRival.Count == 2)
            {
                if (DisponseButton(UpCardsRival.Values.First()) && DisponseButton(UpCardsRival.Values.ElementAt(1)))
                {
                    Button btCard1 = GetButton(UpCardsRival.Values.First());
                    Button btCard2 = GetButton(UpCardsRival.Values.ElementAt(1));
                    btCard1.Content = null;
                    btCard2.Content = null;
                    UpCardRival.Clear();
                    UpCardsRival.Clear();
                }
            }
        
            
        }

        /// <summary>
        /// Controlador del botón para cerrrar la ventana.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Inicializa el timer del turno.
        /// </summary>
        /// <param name="game"></param>
       public  void SetTurnTimer(Game game)
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(30) };
            timer.Tick += delegate {
              var response =  Alert.ShowDialogWithResponse(Application.Current.Resources["lbAvailability"].ToString(), Application.Current.Resources["btYes"].ToString());
                if(response == AlertResult.Unavaible)
                {
                    this.Close();
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


        /// <summary>
        /// Controlador para el evento de MouseMove.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            timer.Stop();
            timer.Start();
        }
    }
}

