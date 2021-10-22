using AdivinaQue.Host.DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaQue.Host.InterfaceContract
{
    [ServiceContract(CallbackContract = typeof(IClient))]

    public interface IService
    {
        [OperationContract]
        Boolean join(string username, string password);
        [OperationContract(IsOneWay = true)]
        void sendMessage(string message, String username, string userReceptor);
        [OperationContract(IsOneWay = true)]
        void getConnectedUsers();
        [OperationContract(IsOneWay = true)]
        void disconnectUser(String username);
        [OperationContract(IsOneWay = true)]
        void register(String username, String password, String name, String email);
        [OperationContract]
        bool searchUsername(String newUsername);
        [OperationContract(IsOneWay = true)]

        void searchInfoPlayerByUsername(String username);
        [OperationContract]
        string sendMail(String email);
        [OperationContract(IsOneWay = true)]
        void modify(Player player, String username);
        [OperationContract(IsOneWay = true)]
        void delete(string username);
    }
}
