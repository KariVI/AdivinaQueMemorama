using AdivinaQue.Client.Control;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AdivinaQue.Client.Views
{
    /// <summary>
    /// Lógica de interacción para GameConfiguration.xaml
    /// </summary>
    public partial class GameConfiguration : Window
    {
        Proxy.GameMgtClient serverGame;
        Proxy.PlayerMgtClient serverPlayer;
        private String username;
        private String toUsername;
        CallBack callback;
        private int sizeBoard;
        public ListBox lbxTopic { get { return lbxTopics; } set { lbxTopics = value; } }

        public Home Home { get; set ; }

        Dictionary<BitmapImage, BitmapImage> pairCards = new Dictionary<BitmapImage, BitmapImage>();
        private List<BitmapImage> totalImages = new List<BitmapImage>();
        Dictionary<string, BitmapImage> gameCards = new Dictionary<string, BitmapImage>();
        private bool backHome = true;
        private  int TOTAL_CARDS_DESIGN = 48;
        private  int TOTAL_CARDS_TESTS = 44;
        private  int TOTAL_CARDS_ADMIN = 40;

        /// <summary>
        /// Inicializa una nueva instancia de  GameConfiguration.xaml.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="username"></param>
        /// <param name="toUsername"></param>
        public GameConfiguration(CallBack callback, String username, String toUsername)
        {
            sizeBoard = 12;
            InitializeComponent();
            cbSizeBoard = new ComboBox();
            this.callback = callback;
            InstanceContext context = new InstanceContext(callback);
            serverPlayer = new Proxy.PlayerMgtClient(context);
            serverGame = new Proxy.GameMgtClient(context);
            this.username = username;
            this.toUsername = toUsername;
        }
        /// <summary>
        /// Controlador del botón confirmar configuración.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtConfirm_Click(object sender, RoutedEventArgs e)
        {
            string category;

            if (lbxTopic.SelectedItem != null)
            {
                string categoryAuxiliar = lbxTopic.SelectedItem.ToString();
                int found = categoryAuxiliar.IndexOf(": ");
                category = categoryAuxiliar.Substring(found + 2);
                Game game = new Game(serverGame, sizeBoard, category);
                
                int[] randomPositionList = GenerateRandomNumbers(sizeBoard, sizeBoard);
                int cardsNumber = GetNumberCards(category);
                int[] randomImageList = GenerateRandomNumbers(sizeBoard,cardsNumber / 2);

                try
                {
                    serverGame.SendBoard(toUsername, sizeBoard, category);
                    serverGame.SendBoardLists(toUsername, randomImageList, randomPositionList);
                     Thread.Sleep(100);
                    serverGame.SendRival(username, toUsername);
                    callback.SetServer(serverGame);
                    callback.SetGame(game);
                    serverPlayer.GetCurrentlyUserPlayed();
                }
                catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException )
                {
                    Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                    backHome = false;
                    Login login = new Login();
                    login.Show();
                    this.Close();
                }


                game.SetUsername(username);
                game.SetUsernameRival(toUsername);
                game.SetRandomLists(randomImageList, randomPositionList);
                game.NextTurn = true;
                game.home = Home;
                game.Show();
                backHome = false;
                this.Close();
            }
            else
            {
                Alert.ShowDialog(Application.Current.Resources["lbSelected"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
        }

        /// <summary>
        /// Devuelve el número de cartas dependiendo de la categoría seleccionada.
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Número entero de cartas de determinada categoría.</returns>
        public int GetNumberCards(string category)
        {
            int cardsNumber = 0;
            switch (category)
            {

                case "Disenio":

                    cardsNumber = TOTAL_CARDS_DESIGN;
                    break;
                case "Pruebas":
                    cardsNumber = TOTAL_CARDS_TESTS;
                    break;
                case "Administración":
                    cardsNumber = TOTAL_CARDS_ADMIN;
                    break;

            }
            return cardsNumber;
        }

        /// <summary>
        /// Genera números aleatorios.
        /// </summary>
        /// <param name="sizeList">Tanaño de la lista de números aleatorios.</param>
        /// <param name="sizeRandom">Tamaño máximo que tendra cada número aleatorio.</param>
        /// <returns>Lista con números enteros aleatorios no repetidos</returns>
        public int[] GenerateRandomNumbers(int sizeList,int sizeRandom)
        {
            Random randomNumber = new Random();
            List<int> randomList = new List<int>();
            while (randomList.Count < sizeList)
            {
                int buttonPosition = randomNumber.Next(sizeRandom);
                if (!randomList.Contains(buttonPosition))
                {
                    randomList.Add(buttonPosition);
                }
            }
            return randomList.ToArray();
        }

        /// <summary>
        /// Controlador del botón para cerrar la ventana.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (backHome)
            {
                Home.Show();
            }
        }

        /// <summary>
        /// Controlador para el evento del combobox de tamaño del tablero.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbSizeBoard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = ((sender as ComboBox).SelectedItem as ComboBoxItem);

            string sizeSelected = comboBoxItem.Content.ToString();

            if (sizeSelected.Equals("3 x 4"))
            {
                sizeBoard = 12;

            }
            else if (sizeSelected.Equals("4 x 4"))
            {
                sizeBoard = 16;

            }
            else if (sizeSelected.Equals("5 x 4"))
            {
                sizeBoard = 20;

            }
          
            
        }
    }
}

