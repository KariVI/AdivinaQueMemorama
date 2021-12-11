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

    public partial class GameConfiguration : Window
    {
        public ObservableCollection<String> topicsCollection;
        Proxy.GameMgtClient serverGame;
        Proxy.PlayerMgtClient serverPlayer;
        private String username;
        private String toUsername;
        CallBack callback;
        private int sizeBoard;
        public ListBox lbxTopic { get { return lbxTopics; } set { lbxTopics = value; } }

        public Home Home { get => home; set => home = value; }

        Dictionary<BitmapImage, BitmapImage> pairCards = new Dictionary<BitmapImage, BitmapImage>();
        private List<BitmapImage> totalImages = new List<BitmapImage>();
        Dictionary<string, BitmapImage> gameCards = new Dictionary<string, BitmapImage>();
        private Home home;
        private bool backHome = true;

        
        public GameConfiguration(CallBack callback, String username, String toUsername)
        {
            sizeBoard = 12;
            InitializeComponent();
            cbSizeBoard = new ComboBox();
            topicsCollection = new ObservableCollection<string>();
            this.callback = callback;
            InstanceContext context = new InstanceContext(callback);
            serverPlayer = new Proxy.PlayerMgtClient(context);
            serverGame = new Proxy.GameMgtClient(context);
            this.username = username;
            this.toUsername = toUsername;
        }

        private void ConfirmBt_Click(object sender, RoutedEventArgs e)
        {
            string category;

            if (lbxTopic.SelectedItem != null)
            {
                string categoryAuxiliar = lbxTopic.SelectedItem.ToString();
                int found = categoryAuxiliar.IndexOf(": ");
                category = categoryAuxiliar.Substring(found + 2);              
                Game game = new Game(serverGame, sizeBoard, category);
                
                int[] randomPositionList = GenerateRandomNumbers(sizeBoard);
                int[] randomImageList = GenerateRandomNumbers(sizeBoard / 2);
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
                game.home = home;
                game.Show();
                backHome = false;
                this.Close();
            }
            else
            {
                Alert.ShowDialogWithResponse(Application.Current.Resources["lbSelected"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
        }

        public int[] GenerateRandomNumbers(int size)
        {
            Random randomNumber = new Random();
            List<int> randomList = new List<int>();
            while (randomList.Count() < size)
            {
                int buttonPosition = randomNumber.Next(size);
                if (!randomList.Contains(buttonPosition))
                {
                    randomList.Add(buttonPosition);
                }
            }
            return randomList.ToArray();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (backHome)
            {
                Home.Show();
            }
        }

        private void cbSizeBoard_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            else if (sizeSelected.Equals("6 x 5"))
            {
                sizeBoard = 30;

            }
            
        }
    }
}

