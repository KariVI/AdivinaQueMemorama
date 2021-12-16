using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaQue.Host.InterfaceContract
{
    [DataContract]
    /// <summary>
    /// Guardar la información de una partida finalizada  
    /// </summary> 
    public class GameCurrently
    {
        [DataMember]
        public Dictionary<string,int> Players { get; set; }
        [DataMember]
        public int ScoreWinner { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string Winner { get; set; }
        [DataMember]
        public string Topic { get; set; }
        public GameCurrently()
        {
            Players = new Dictionary<string, int>();
        }
        

    }
}
