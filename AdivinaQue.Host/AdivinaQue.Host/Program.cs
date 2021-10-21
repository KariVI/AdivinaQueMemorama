using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using AdivinaQue.Host.BusinessRules;
using AdivinaQue.Host.Logs;
using log4net;

namespace AdivinaQue.Host
{
    public class Program
    {
        private static readonly ILog Logs = Log.GetLogger();
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Service)))
            {
                try
                {
                    host.Open();
                    Console.WriteLine("Server is running");

                }
                catch (AddressAccessDeniedException ex)
                {
                    Logs.Error($"Fallo la conexión ({ ex.Message})");
                }
              
                Console.ReadLine();
            }
        }
    }
}
