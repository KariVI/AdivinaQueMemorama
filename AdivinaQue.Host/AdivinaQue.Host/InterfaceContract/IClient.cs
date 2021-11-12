﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web.UI.WebControls;
using System.Windows.Media.Imaging;

namespace AdivinaQue.Host.InterfaceContract
{

    [ServiceContract]

    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void RecieveMessage(String message);
        [OperationContract(IsOneWay = true)]
        void RecieveUsers(Dictionary<String, IClient> users);
        [OperationContract(IsOneWay = true)]
        void RecievePlayer(Player player);
        [OperationContract]
        bool SendInvitationGame(String username);
        [OperationContract(IsOneWay = true)]
        void SendBoardConfigurate(String username, int size, string category);
        [OperationContract(IsOneWay = true)]
        void ReceiveRival(String rival);
        [OperationContract(IsOneWay = true)]
        void RecieveScores(Dictionary<string, int> globalScores);
        
        [OperationContract(IsOneWay = true)]
        void RecieveTopics(List<String> topics);
        [OperationContract(IsOneWay = true)]
        void ReceiveCardSeed(List<int> randomImageList, List<int> randomPositionList);
        [OperationContract(IsOneWay = true)]
        void ReceiveCorrectPair(Dictionary<BitmapImage, string> cards);
    }

    [DataContract]
    public class Player
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Email { get; set; }
    }
}

