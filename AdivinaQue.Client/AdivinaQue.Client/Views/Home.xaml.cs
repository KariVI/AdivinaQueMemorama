using AdivinaQue.Client.Control;
using AdivinaQue.Client.Proxy;
using System;
using System.ServiceModel;
using System.Timers;
using System.Windows;

namespace AdivinaQue.Client.Views
{
    public partial class Home : Window
    {
        private ServiceClient server;
        private string username;
        private Chat chat;
        private CallBack callback;
       
        public Home(ServiceClient server,CallBack callback)
        {
            InitializeComponent();
            this.server = server;
            this.callback = callback;
            callback.setHome(this);
        }

        private void btModify_Click(object sender, RoutedEventArgs e)
        {
            CallBack callback = new CallBack();
            InstanceContext context = new InstanceContext(callback);
            server = new Proxy.ServiceClient(context);
            Modify modify = new Modify(callback,this);
            callback.SetModify(modify);
            modify.SetHome(this);
            modify.SetUsername(username);
            modify.SetServer(server);
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
                server.DisconnectUser(username);
                if(chat != null)
                {
                    chat.Close();
                }
            } catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException  )

            {
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
            }
        }

        public void setUsername(string username)
        {
            this.username = username;
            setLabel();     
        }

        public void setLabel()
        {
            lbUser.Content = Application.Current.Resources["lbGretting"].ToString()+" " + username;
        }

        private void btStartGame_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext context = new InstanceContext(callback);
            server = new Proxy.ServiceClient(context);
            callback.SetCurrentUsername(username);
           
            try
            {
                PlayersList playersList = new PlayersList(server, username, this,callback);
                callback.SetPlayersList(playersList);
                server.GetCurrentlyUserPlayed();
                server.GetConnectedUsers();
                playersList.Show();
                this.Hide();
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException)
            {
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                Login login = new Login();
                login.Show();
                this.Close();
            }    
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Podio podio = new Podio(server, username,this);
            callback.SetPodio(podio);
            server.GetScores(username);
            podio.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            disconect();
        }

        private void btChat_Click(object sender, RoutedEventArgs e)
        {

            chat = new Chat(server);
            chat.setUsername(username);
            callback.SetChat(chat);
            try
            {
                server.GetConnectedUsers();
                chat.Show();
            }
            catch (Exception ex) when (ex is EndpointNotFoundException || ex is TimeoutException)
            {
                Alert.ShowDialog(Application.Current.Resources["lbServerError"].ToString(), Application.Current.Resources["btOk"].ToString());
                Login login = new Login();
                this.Close();
                login.Show();          
            }  
        }


    }
}
