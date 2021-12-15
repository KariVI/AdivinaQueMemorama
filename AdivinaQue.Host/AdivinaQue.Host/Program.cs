using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using AdivinaQue.Host.BusinessRules;
using AdivinaQue.Host.Exception;

namespace AdivinaQue.Host
{
    public static class Program
    {
        public static void Main(string[] args)
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
                    throw new BusinessException("Server isn't running", ex);
                }

                Console.ReadLine();
            }
        }
    }
}
