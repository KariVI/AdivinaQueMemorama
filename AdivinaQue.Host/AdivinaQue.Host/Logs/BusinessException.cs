using AdivinaQue.Host.Logs;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaQue.Host.Exception
{

    [Serializable]
    public class BusinessException: SystemException {
        private static readonly ILog Logs = Log.GetLogger();
        private string message;


        public BusinessException(string message)
        : base(message)
        {
        }

        public BusinessException(string message, SystemException inner) {
             this.message = message;
             Logs.Error($"Fallo la conexión ({ inner.Message})");
        
        }
    }
}
