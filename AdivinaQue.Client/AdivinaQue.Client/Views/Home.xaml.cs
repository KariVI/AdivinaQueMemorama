using AdivinaQue.Client.Control;
using AdivinaQue.Client.Proxy;
using System;
using System.ServiceModel;
using System.Timers;
using System.Windows;

namespace AdivinaQue.Client.Views
{

    // Proxy.PlayerMgtClient serverPlayer;
    public partial class Home : Window
    {
        private Proxy.PlayerMgtClient serverPlayer;
        private string username;
        private Chat chat;
        private CallBack callback;
       
        public Home(Proxy.PlayerMgtClient server,CallBack callback)
        {
            InitializeComponent();
            this.serverPlayer = server;
            this.callback = callback;
            callback.SetHome(this);
            chat = new Chat(serverPlayer);
            
        }

        private void btModify_Click(object sender, RoutedEventArgs e)
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            serverPlayer = new Proxy.PlayerMgtClient(context);
            Modify modify = new Modify(callback,this);
            callback.SetModify(modify);
            modify.SetHome(this);
            modify.SetUsername(username);
            modify.SetServer(serverPlayer);
            this.Hide();
            modify.Show();
        }
        private void btLogout_Click(object sender, RoutedEventArgs e)
        {
            disconect();
            Login login = new Login();
            login.Show();
            this.Close();
        }

        public void disconect()
        {
            try
            {
                serverPlayer.DisconnectUser(username);
                if(chat != null)
                {
                    chat.Close();
                }
            } catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException  || ex is CommunicationObjectFaultedException )

            {
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
        }

        public void setUsername(string username)
        {
            this.username = username;
            setLabel();
            chat.setUsername(username);
            callback.SetChat(chat);
        }

        public void setLabel()
        {
            lbUser.Content = Application.Current.Resources["lbGretting"].ToString()+" " + username;
        }

        private void btStartGame_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext context = new InstanceContext(callback);
            serverPlayer = new Proxy.PlayerMgtClient(context);
            callback.SetCurrentUsername(username);
           
            try
            {
                PlayersList playersList = new PlayersList(serverPlayer, username, this,callback);
                callback.SetPlayersList(playersList);
                serverPlayer.GetConnectedUsers();
                serverPlayer.GetCurrentlyUserPlayed();
                playersList.Show();
                this.Hide();
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException )
            {
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                Login login = new Login();
                login.Show();
                this.Close();
            }    
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Podio podio = new Podio(serverPlayer, username,this);
            callback.SetPodio(podio);
            try
            {
                serverPlayer.GetScores(username);
                podio.Show();
                this.Hide();
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException )
            {
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                Login login = new Login();
                login.Show();
                this.Close();
            }
            
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            disconect();
        }

        private void btChat_Click(object sender, RoutedEventArgs e)
        {

            
            try
            {
                serverPlayer.GetConnectedUsers();
                chat.Show();
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException || ex is CommunicationObjectFaultedException )
            {
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                Login login = new Login();
                this.Close();
                login.Show();          
            }  
        }


    }
}
