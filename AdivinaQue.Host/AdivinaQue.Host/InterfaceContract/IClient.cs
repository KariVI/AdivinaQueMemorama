using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaQue.Host.InterfaceContract
{
    [ServiceContract]

    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void RecieveMessage(string message);
        [OperationContract(IsOneWay = true)]
        void RecieveUsers(Dictionary<String, IClient> users);
        [OperationContract(IsOneWay = true)]
        void RecieveScores(Dictionary<String, int> globalScores);
        [OperationContract(IsOneWay = true)]
        void RecieveTopics(List<String> topics);
        [OperationContract(IsOneWay = true)]
        void RecieveEmails(List<String> emails);
    }

}
