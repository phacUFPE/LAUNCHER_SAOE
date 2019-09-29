using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System;

namespace Launcher_Updater
{
    class IniFile
    {
        RegexFunctions rf = new RegexFunctions();

        string Path;

        public static string DEFAULT_SECTOR = "Geral";

        public static string DEFAULT_ENGINE_NAME = "engine";
        public static string DEFAULT_VERSION_NAME = "version";
        public static string DEFAULT_LANGUAGE_NAME = "language";

        public static string DEFAULT_ENGINE = "OPGL";
        public static string DEFAULT_VERSION = "0";
        public static string DEFAULT_LANGUAGE = "pt";

        public static string EXE = "Config.ini";

        public void CreateIni(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE).FullName.ToString();
        }

        public void NewRead()
        {
            string text = File.ReadAllText(Path);
            Regex regex = new Regex(rf.GetRegexIni(text));
            MatchCollection matches = regex.Matches(text);
            if (matches.Count > 0)
            {
                foreach (Match mtc in matches)
                {
                    Launcher.G_ENGINE = mtc.Groups["eng"].ToString();
                    Launcher.G_VERSION = mtc.Groups["vrs"].ToString();
                    Launcher.G_LANGUAGE = mtc.Groups["lang"].ToString();
                }
            }
        }

        public void NewWrite()
        {
            StreamWriter wr = new StreamWriter(Path, false);
            wr.WriteAsync(String.Format("[{0}]\n{1}={2}\n{3}={4}\n{5}={6}\n", DEFAULT_SECTOR, DEFAULT_ENGINE_NAME, Launcher.G_ENGINE, DEFAULT_VERSION_NAME, Launcher.G_VERSION, DEFAULT_LANGUAGE_NAME, Launcher.G_LANGUAGE));
            wr.Close();
        }      
    }
}
