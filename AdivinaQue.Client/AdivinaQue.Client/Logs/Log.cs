
using System.Runtime.CompilerServices;
using log4net;



namespace AdivinaQue.Client.Logs
{
    public static class Log
    {
        public static ILog GetLogger([CallerFilePath] string filename = "")
        {
            return LogManager.GetLogger(filename);
        }
    }
}
