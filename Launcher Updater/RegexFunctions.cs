using System;

namespace Launcher_Updater
{
    class RegexFunctions
    {
        public string GetRegexHTML(string url)
        {
            return "<img src=\".*\" alt=\"(?<type>.*)\"></td><td><a href=\"(?<name>.*)\">.*</a>";
        }

        public string GetRegexHashs(string text)
        {
            return "file=\"(?<file>.*)\", hash=\"(?<hash>.*)\";";
        }

        public string GetRegexCountHashs(string text)
        {
            return "totalfiles=(?<count>.*)";
        }

        public string GetTranslations(string text)
        {
            return "(?<name>.*)=\"(?<translation>.*)\"";
        }

        public string GetRegexVersion(string text)
        {
            return "current_ver=(?<ver>.*)\nmin_ver=(?<min_ver>.*)";
        }

        public string GetRegexLinks(string text)
        {
            return "facebook=(?<facebook>.*)\nsite=(?<site>.*)\ndonate=(?<donate>.*)\nregister=(?<register>.*)";
        }

        public string GetRegexOTML(string text)
        {
            return "locale: (?<locale>.*)\nfullscreen: (?<fullscreen>.*)\n";
        }

        public string GetRegexIni(string text)
        {
            return String.Format("{0}=(?<eng>.*)\n{1}=(?<vrs>.*)\n{2}=(?<lang>.*)\n", IniFile.DEFAULT_ENGINE_NAME, IniFile.DEFAULT_VERSION_NAME, IniFile.DEFAULT_LANGUAGE_NAME);
        }
    }
}
