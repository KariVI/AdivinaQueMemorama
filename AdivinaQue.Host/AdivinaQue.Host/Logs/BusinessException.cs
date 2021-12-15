using AdivinaQue.Host.Logs;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AdivinaQue.Host.Exception
{
  

    public class BusinessException : SystemException
    {
       
        private static readonly ILog Logs = Log.GetLogger();
        private readonly string message;

    

        public BusinessException(string message, SmtpException inner) {
             this.message = message;
             Logs.Error($"Fallo la conexión ({ inner.Message})");
        
        }

        public BusinessException(string message, SystemException inner)
        {
            this.message = message;
            Logs.Error($"Fallo la conexión ({ inner.Message})");

        }

      
    }
}
