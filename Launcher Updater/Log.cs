using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher_Updater
{
    class Log
    {
        OperatingSystem os = Environment.OSVersion;
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string logFile = "log.txt";

        public void CreateLog()
        {
            if (!File.Exists(baseDirectory + logFile))
            {
                File.Create(baseDirectory + logFile).Close();
            }
        }

        public void ClearLog()
        {
            CreateLog();
            StreamWriter swLog = new StreamWriter(baseDirectory + logFile, false);
            swLog.Write("");
            swLog.Close();
        }

        public void WriteLog(string[] toWrite)
        {
            CreateLog();
            DateTime dtNow = DateTime.UtcNow;
            StreamWriter swLog = new StreamWriter(baseDirectory + logFile, true);
            swLog.WriteLine("Date/Time: {0} [UTC+0] [GMT+0]", dtNow);
            swLog.WriteLine(String.Format("Operating System: {0}", os.VersionString));
            foreach (string w in toWrite)
            {
                swLog.WriteLine(w);
            }
            swLog.WriteLine("-");
            swLog.Close();
        }

    }
}
