﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Lógica de interacción para Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        public Chat()
        {
            InitializeComponent();
        }
        Proxy.PlayerMgtClient server;
        private string username;
        private string typeMessage;

        public ObservableCollection<String> messagesCollection;
        public ObservableCollection<String> usersCollection;


        public Label MessageTitle { get { return Title; } set { Title = value; } }
        public Button SendMessageButton { get { return SendButton; } set { SendButton = value; } }
        public ScrollViewer ScrollTextBox { get { return ContentScroller; } set { ContentScroller = value; } }
        public TextBox MessageContainer { get { return MessageContent; } set { MessageContent = value; } }
        public ListBox ListUsers { get { return UsersConnected; } set { UsersConnected = value; } }
        public ListView Messages { get { return listMessages; } set { listMessages = value; } }


        public Chat(Proxy.PlayerMgtClient server)
        {
            InitializeComponent();
            this.server = server;
            messagesCollection = new ObservableCollection<String>();
            usersCollection = new ObservableCollection<string>();
            typeMessage = "Todos";
            Messages.ItemsSource = messagesCollection;
            ListUsers.ItemsSource = usersCollection;

        }

        public void setUsername(string username)
        {
            
            this.username = username;
            setLabel();
        }
        public void setLabel()
        {
            lbUser.Content = Application.Current.Resources["lbGretting"].ToString() + " " + username;
        }


        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MessageContent.Text))

                if (ListUsers.SelectedValue != null)
                {
                    Console.WriteLine(typeMessage);
                    typeMessage = ListUsers.SelectedValue.ToString();
                }

                server.SendMessage(MessageContent.Text, username, typeMessage);
       
            
            ListUsers.SelectedValue = null;
            MessageContent.Clear();
            typeMessage = "Todos";

        }

        private void Window_Closed(object sender, EventArgs e)
        {
           
            this.Close();
        }
    }
}
