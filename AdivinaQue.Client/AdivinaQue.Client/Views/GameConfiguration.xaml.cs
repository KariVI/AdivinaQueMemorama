using AdivinaQue.Client.Control;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
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
    /// <summary>
    /// Lógica de interacción para GameConfiguration.xaml
    /// </summary>
    public partial class GameConfiguration : Window
    {
        public ObservableCollection<String> topicsCollection;
        Proxy.ServiceClient server;
        private String username;
        private String toUsername;
        CallBack callback;
        public ListBox lbxTopic { get { return lbxTopics; } set { lbxTopics = value; } }
        Dictionary<BitmapImage, BitmapImage> pairCards = new Dictionary<BitmapImage, BitmapImage>();
        private List<BitmapImage> totalImages = new List<BitmapImage>();
        Dictionary<string, BitmapImage> gameCards = new Dictionary<string, BitmapImage>();

        public GameConfiguration(CallBack callback, String username, String toUsername)
        {

            InitializeComponent();
            topicsCollection = new ObservableCollection<string>();
            this.callback = callback;
            InstanceContext context = new InstanceContext(callback);
            server = new Proxy.ServiceClient(context);
            this.username = username;
            this.toUsername = toUsername;
        }

        private void ConfirmBt_Click(object sender, RoutedEventArgs e)
        {
            string category = "All";

            if (lbxTopic.SelectedItem != null)
            {
                string categoryAuxiliar = lbxTopic.SelectedItem.ToString();
                int found = categoryAuxiliar.IndexOf(": ");
                category = categoryAuxiliar.Substring(found + 2);

            }

            int sizeBoard = 0;
            if (cbSizeBoard.Text.Equals("2 x 2"))
            {
                sizeBoard = 2;
            }
            else if (cbSizeBoard.Text.Equals("3 x 3"))
            {
                sizeBoard = 3;

            }
            else if (cbSizeBoard.Text.Equals("4 x 4"))
            {
                sizeBoard = 4;

            }
            else if (cbSizeBoard.Text.Equals("5 x 5"))
            {
                sizeBoard = 5;

            }
            else
            {
                sizeBoard = 6;

            }
            Game game = new Game( server, sizeBoard, category);
            int[] randomPositionList = GenerateRandomNumbers(sizeBoard * sizeBoard);
            int[] randomImageList = GenerateRandomNumbers((sizeBoard * sizeBoard) / 2);
            server.SendBoard(toUsername, sizeBoard, category);     
            server.SendBoardLists(toUsername, randomImageList, randomPositionList);
            callback.setServer(server);
            callback.SetGame(game);
            server.SendRival(username, toUsername);
            game.SetUsername(username);
            game.SetUsernameRival(toUsername);
            game.SetRandomLists(randomImageList, randomPositionList);
            game.Show();
            this.Close();
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
    }
}

