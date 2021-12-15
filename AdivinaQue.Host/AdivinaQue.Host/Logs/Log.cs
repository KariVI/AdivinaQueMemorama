using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace AdivinaQue.Host.Logs
{
    public static class Log
    {

             public static ILog GetLogger([CallerFilePath] string filename = "")
            {
                return LogManager.GetLogger(filename);
            }
        }
 }


