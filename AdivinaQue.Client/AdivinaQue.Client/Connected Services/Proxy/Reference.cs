﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdivinaQue.Client.Proxy {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Player", Namespace="http://schemas.datacontract.org/2004/07/AdivinaQue.Host.InterfaceContract")]
    [System.SerializableAttribute()]
    public partial class Player : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GameCurrently", Namespace="http://schemas.datacontract.org/2004/07/AdivinaQue.Host.InterfaceContract")]
    [System.SerializableAttribute()]
    public partial class GameCurrently : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<string, int> PlayersField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ScoreWinnerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TopicField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WinnerField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Date {
            get {
                return this.DateField;
            }
            set {
                if ((object.ReferenceEquals(this.DateField, value) != true)) {
                    this.DateField = value;
                    this.RaisePropertyChanged("Date");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<string, int> Players {
            get {
                return this.PlayersField;
            }
            set {
                if ((object.ReferenceEquals(this.PlayersField, value) != true)) {
                    this.PlayersField = value;
                    this.RaisePropertyChanged("Players");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ScoreWinner {
            get {
                return this.ScoreWinnerField;
            }
            set {
                if ((this.ScoreWinnerField.Equals(value) != true)) {
                    this.ScoreWinnerField = value;
                    this.RaisePropertyChanged("ScoreWinner");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Topic {
            get {
                return this.TopicField;
            }
            set {
                if ((object.ReferenceEquals(this.TopicField, value) != true)) {
                    this.TopicField = value;
                    this.RaisePropertyChanged("Topic");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Winner {
            get {
                return this.WinnerField;
            }
            set {
                if ((object.ReferenceEquals(this.WinnerField, value) != true)) {
                    this.WinnerField = value;
                    this.RaisePropertyChanged("Winner");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Proxy.IService", CallbackContract=typeof(AdivinaQue.Client.Proxy.IServiceCallback))]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Join", ReplyAction="http://tempuri.org/IService/JoinResponse")]
        bool Join(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Join", ReplyAction="http://tempuri.org/IService/JoinResponse")]
        System.Threading.Tasks.Task<bool> JoinAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendMessage")]
        void SendMessage(string message, string username, string userReceptor);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendMessage")]
        System.Threading.Tasks.Task SendMessageAsync(string message, string username, string userReceptor);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/GetConnectedUsers")]
        void GetConnectedUsers();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/GetConnectedUsers")]
        System.Threading.Tasks.Task GetConnectedUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/DisconnectUser")]
        void DisconnectUser(string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/DisconnectUser")]
        System.Threading.Tasks.Task DisconnectUserAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Register", ReplyAction="http://tempuri.org/IService/RegisterResponse")]
        bool Register(AdivinaQue.Client.Proxy.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Register", ReplyAction="http://tempuri.org/IService/RegisterResponse")]
        System.Threading.Tasks.Task<bool> RegisterAsync(AdivinaQue.Client.Proxy.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SearchUsername", ReplyAction="http://tempuri.org/IService/SearchUsernameResponse")]
        bool SearchUsername(string newUsername);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SearchUsername", ReplyAction="http://tempuri.org/IService/SearchUsernameResponse")]
        System.Threading.Tasks.Task<bool> SearchUsernameAsync(string newUsername);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SearchInfoPlayerByUsername")]
        void SearchInfoPlayerByUsername(string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SearchInfoPlayerByUsername")]
        System.Threading.Tasks.Task SearchInfoPlayerByUsernameAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GenerateCode", ReplyAction="http://tempuri.org/IService/GenerateCodeResponse")]
        string GenerateCode();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GenerateCode", ReplyAction="http://tempuri.org/IService/GenerateCodeResponse")]
        System.Threading.Tasks.Task<string> GenerateCodeAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Modify", ReplyAction="http://tempuri.org/IService/ModifyResponse")]
        bool Modify(AdivinaQue.Client.Proxy.Player player, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Modify", ReplyAction="http://tempuri.org/IService/ModifyResponse")]
        System.Threading.Tasks.Task<bool> ModifyAsync(AdivinaQue.Client.Proxy.Player player, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Delete", ReplyAction="http://tempuri.org/IService/DeleteResponse")]
        bool Delete(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/Delete", ReplyAction="http://tempuri.org/IService/DeleteResponse")]
        System.Threading.Tasks.Task<bool> DeleteAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SendInvitation", ReplyAction="http://tempuri.org/IService/SendInvitationResponse")]
        bool SendInvitation(string toUsername, string fromUsername);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SendInvitation", ReplyAction="http://tempuri.org/IService/SendInvitationResponse")]
        System.Threading.Tasks.Task<bool> SendInvitationAsync(string toUsername, string fromUsername);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SendMail", ReplyAction="http://tempuri.org/IService/SendMailResponse")]
        string SendMail(string to, string asunto, string body);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SendMail", ReplyAction="http://tempuri.org/IService/SendMailResponse")]
        System.Threading.Tasks.Task<string> SendMailAsync(string to, string asunto, string body);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetEmails", ReplyAction="http://tempuri.org/IService/GetEmailsResponse")]
        string[] GetEmails();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetEmails", ReplyAction="http://tempuri.org/IService/GetEmailsResponse")]
        System.Threading.Tasks.Task<string[]> GetEmailsAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/GetTopics")]
        void GetTopics(string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/GetTopics")]
        System.Threading.Tasks.Task GetTopicsAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/GetScores")]
        void GetScores(string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/GetScores")]
        System.Threading.Tasks.Task GetScoresAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendBoard")]
        void SendBoard(string toUsername, int size, string category);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendBoard")]
        System.Threading.Tasks.Task SendBoardAsync(string toUsername, int size, string category);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendRival")]
        void SendRival(string rival, string fromUsername);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendRival")]
        System.Threading.Tasks.Task SendRivalAsync(string rival, string fromUsername);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendBoardLists")]
        void SendBoardLists(string toUsername, int[] randomImageList, int[] randomPositionList);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendBoardLists")]
        System.Threading.Tasks.Task SendBoardListsAsync(string toUsername, int[] randomImageList, int[] randomPositionList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetUsers", ReplyAction="http://tempuri.org/IService/GetUsersResponse")]
        string[] GetUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetUsers", ReplyAction="http://tempuri.org/IService/GetUsersResponse")]
        System.Threading.Tasks.Task<string[]> GetUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendCorrectCards")]
        void SendCorrectCards(string toUsername, System.Collections.Generic.Dictionary<System.Windows.Media.Imaging.BitmapImage, string> cards);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendCorrectCards")]
        System.Threading.Tasks.Task SendCorrectCardsAsync(string toUsername, System.Collections.Generic.Dictionary<System.Windows.Media.Imaging.BitmapImage, string> cards);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendScoreRival")]
        void SendScoreRival(string toUsername, int score);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendScoreRival")]
        System.Threading.Tasks.Task SendScoreRivalAsync(string toUsername, int score);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendNextTurnRival")]
        void SendNextTurnRival(string toUsername, bool nextTurn);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendNextTurnRival")]
        System.Threading.Tasks.Task SendNextTurnRivalAsync(string toUsername, bool nextTurn);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendNumberCardsFinded")]
        void SendNumberCardsFinded(string toUsername, int numberCardsFinded);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendNumberCardsFinded")]
        System.Threading.Tasks.Task SendNumberCardsFindedAsync(string toUsername, int numberCardsFinded);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendWinner")]
        void SendWinner(string toUsername, string winner);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendWinner")]
        System.Threading.Tasks.Task SendWinnerAsync(string toUsername, string winner);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SendGame", ReplyAction="http://tempuri.org/IService/SendGameResponse")]
        bool SendGame(AdivinaQue.Client.Proxy.GameCurrently gameCurrently);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SendGame", ReplyAction="http://tempuri.org/IService/SendGameResponse")]
        System.Threading.Tasks.Task<bool> SendGameAsync(AdivinaQue.Client.Proxy.GameCurrently gameCurrently);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/RecieveMessage")]
        void RecieveMessage(string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/RecieveUsers")]
        void RecieveUsers(System.Collections.Generic.Dictionary<string, object> users);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/RecievePlayer")]
        void RecievePlayer(AdivinaQue.Client.Proxy.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/SendInvitationGame", ReplyAction="http://tempuri.org/IService/SendInvitationGameResponse")]
        bool SendInvitationGame(string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendBoardConfigurate")]
        void SendBoardConfigurate(string username, int size, string category);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/ReceiveRival")]
        void ReceiveRival(string rival);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/RecieveScores")]
        void RecieveScores(System.Collections.Generic.Dictionary<string, int> globalScores);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/RecieveTopics")]
        void RecieveTopics(string[] topics);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/ReceiveCardSeed")]
        void ReceiveCardSeed(int[] randomImageList, int[] randomPositionList);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/ReceiveCorrectPair")]
        void ReceiveCorrectPair(System.Collections.Generic.Dictionary<System.Windows.Media.Imaging.BitmapImage, string> cards);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/ReceiveScoreRival")]
        void ReceiveScoreRival(int score);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/ReceiveNextTurn")]
        void ReceiveNextTurn(bool nextTurn);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/ReceiveNumberCardsFinded")]
        void ReceiveNumberCardsFinded(int numberCardsFinded);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/ReceiveWinner")]
        void ReceiveWinner(string winner);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : AdivinaQue.Client.Proxy.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.DuplexClientBase<AdivinaQue.Client.Proxy.IService>, AdivinaQue.Client.Proxy.IService {
        
        public ServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public bool Join(string username, string password) {
            return base.Channel.Join(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> JoinAsync(string username, string password) {
            return base.Channel.JoinAsync(username, password);
        }
        
        public void SendMessage(string message, string username, string userReceptor) {
            base.Channel.SendMessage(message, username, userReceptor);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(string message, string username, string userReceptor) {
            return base.Channel.SendMessageAsync(message, username, userReceptor);
        }
        
        public void GetConnectedUsers() {
            base.Channel.GetConnectedUsers();
        }
        
        public System.Threading.Tasks.Task GetConnectedUsersAsync() {
            return base.Channel.GetConnectedUsersAsync();
        }
        
        public void DisconnectUser(string username) {
            base.Channel.DisconnectUser(username);
        }
        
        public System.Threading.Tasks.Task DisconnectUserAsync(string username) {
            return base.Channel.DisconnectUserAsync(username);
        }
        
        public bool Register(AdivinaQue.Client.Proxy.Player player) {
            return base.Channel.Register(player);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterAsync(AdivinaQue.Client.Proxy.Player player) {
            return base.Channel.RegisterAsync(player);
        }
        
        public bool SearchUsername(string newUsername) {
            return base.Channel.SearchUsername(newUsername);
        }
        
        public System.Threading.Tasks.Task<bool> SearchUsernameAsync(string newUsername) {
            return base.Channel.SearchUsernameAsync(newUsername);
        }
        
        public void SearchInfoPlayerByUsername(string username) {
            base.Channel.SearchInfoPlayerByUsername(username);
        }
        
        public System.Threading.Tasks.Task SearchInfoPlayerByUsernameAsync(string username) {
            return base.Channel.SearchInfoPlayerByUsernameAsync(username);
        }
        
        public string GenerateCode() {
            return base.Channel.GenerateCode();
        }
        
        public System.Threading.Tasks.Task<string> GenerateCodeAsync() {
            return base.Channel.GenerateCodeAsync();
        }
        
        public bool Modify(AdivinaQue.Client.Proxy.Player player, string username) {
            return base.Channel.Modify(player, username);
        }
        
        public System.Threading.Tasks.Task<bool> ModifyAsync(AdivinaQue.Client.Proxy.Player player, string username) {
            return base.Channel.ModifyAsync(player, username);
        }
        
        public bool Delete(string username) {
            return base.Channel.Delete(username);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteAsync(string username) {
            return base.Channel.DeleteAsync(username);
        }
        
        public bool SendInvitation(string toUsername, string fromUsername) {
            return base.Channel.SendInvitation(toUsername, fromUsername);
        }
        
        public System.Threading.Tasks.Task<bool> SendInvitationAsync(string toUsername, string fromUsername) {
            return base.Channel.SendInvitationAsync(toUsername, fromUsername);
        }
        
        public string SendMail(string to, string asunto, string body) {
            return base.Channel.SendMail(to, asunto, body);
        }
        
        public System.Threading.Tasks.Task<string> SendMailAsync(string to, string asunto, string body) {
            return base.Channel.SendMailAsync(to, asunto, body);
        }
        
        public string[] GetEmails() {
            return base.Channel.GetEmails();
        }
        
        public System.Threading.Tasks.Task<string[]> GetEmailsAsync() {
            return base.Channel.GetEmailsAsync();
        }
        
        public void GetTopics(string username) {
            base.Channel.GetTopics(username);
        }
        
        public System.Threading.Tasks.Task GetTopicsAsync(string username) {
            return base.Channel.GetTopicsAsync(username);
        }
        
        public void GetScores(string username) {
            base.Channel.GetScores(username);
        }
        
        public System.Threading.Tasks.Task GetScoresAsync(string username) {
            return base.Channel.GetScoresAsync(username);
        }
        
        public void SendBoard(string toUsername, int size, string category) {
            base.Channel.SendBoard(toUsername, size, category);
        }
        
        public System.Threading.Tasks.Task SendBoardAsync(string toUsername, int size, string category) {
            return base.Channel.SendBoardAsync(toUsername, size, category);
        }
        
        public void SendRival(string rival, string fromUsername) {
            base.Channel.SendRival(rival, fromUsername);
        }
        
        public System.Threading.Tasks.Task SendRivalAsync(string rival, string fromUsername) {
            return base.Channel.SendRivalAsync(rival, fromUsername);
        }
        
        public void SendBoardLists(string toUsername, int[] randomImageList, int[] randomPositionList) {
            base.Channel.SendBoardLists(toUsername, randomImageList, randomPositionList);
        }
        
        public System.Threading.Tasks.Task SendBoardListsAsync(string toUsername, int[] randomImageList, int[] randomPositionList) {
            return base.Channel.SendBoardListsAsync(toUsername, randomImageList, randomPositionList);
        }
        
        public string[] GetUsers() {
            return base.Channel.GetUsers();
        }
        
        public System.Threading.Tasks.Task<string[]> GetUsersAsync() {
            return base.Channel.GetUsersAsync();
        }
        
        public void SendCorrectCards(string toUsername, System.Collections.Generic.Dictionary<System.Windows.Media.Imaging.BitmapImage, string> cards) {
            base.Channel.SendCorrectCards(toUsername, cards);
        }
        
        public System.Threading.Tasks.Task SendCorrectCardsAsync(string toUsername, System.Collections.Generic.Dictionary<System.Windows.Media.Imaging.BitmapImage, string> cards) {
            return base.Channel.SendCorrectCardsAsync(toUsername, cards);
        }
        
        public void SendScoreRival(string toUsername, int score) {
            base.Channel.SendScoreRival(toUsername, score);
        }
        
        public System.Threading.Tasks.Task SendScoreRivalAsync(string toUsername, int score) {
            return base.Channel.SendScoreRivalAsync(toUsername, score);
        }
        
        public void SendNextTurnRival(string toUsername, bool nextTurn) {
            base.Channel.SendNextTurnRival(toUsername, nextTurn);
        }
        
        public System.Threading.Tasks.Task SendNextTurnRivalAsync(string toUsername, bool nextTurn) {
            return base.Channel.SendNextTurnRivalAsync(toUsername, nextTurn);
        }
        
        public void SendNumberCardsFinded(string toUsername, int numberCardsFinded) {
            base.Channel.SendNumberCardsFinded(toUsername, numberCardsFinded);
        }
        
        public System.Threading.Tasks.Task SendNumberCardsFindedAsync(string toUsername, int numberCardsFinded) {
            return base.Channel.SendNumberCardsFindedAsync(toUsername, numberCardsFinded);
        }
        
        public void SendWinner(string toUsername, string winner) {
            base.Channel.SendWinner(toUsername, winner);
        }
        
        public System.Threading.Tasks.Task SendWinnerAsync(string toUsername, string winner) {
            return base.Channel.SendWinnerAsync(toUsername, winner);
        }
        
        public bool SendGame(AdivinaQue.Client.Proxy.GameCurrently gameCurrently) {
            return base.Channel.SendGame(gameCurrently);
        }
        
        public System.Threading.Tasks.Task<bool> SendGameAsync(AdivinaQue.Client.Proxy.GameCurrently gameCurrently) {
            return base.Channel.SendGameAsync(gameCurrently);
        }
    }
}
