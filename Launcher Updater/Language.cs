using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Launcher_Updater
{
    class Language
    {
        private static string path = AppDomain.CurrentDomain.BaseDirectory + "lang";
        private static Hashtable LANGUAGE = new Hashtable();

        private static int ext_len = 5;

        static Log log = new Log();

        public static Hashtable LoadLanguages()
        {
            try
            {
                RegexFunctions rfunc = new RegexFunctions();

                string[] languages = Directory.GetFiles(path);

                foreach (string file in languages)
                {

                    Hashtable LANGUAGE_WORDS = new Hashtable();

                    string translations;
                    FileInfo transl = new FileInfo(file);
                    StreamReader rTransl = new StreamReader(transl.OpenRead());
                    translations = rTransl.ReadToEnd();
                    rTransl.Close();

                    Regex regex = new Regex(rfunc.GetTranslations(translations));
                    MatchCollection matches = regex.Matches(translations);

                    Regex lang_regex = new Regex("[a-zA-Z]+");
                    Match lang_match = lang_regex.Match(translations);

                    foreach (Match m in matches)
                    {
                        LANGUAGE_WORDS.Add(m.Groups["name"].ToString(), m.Groups["translation"].ToString());
                    }

                    string m_file = file.Remove(0, path.Length);
                    m_file = m_file.Replace("/", "").Replace(@"\", "");
                    m_file = m_file.Remove(m_file.Length - ext_len);

                    ComboboxItem item = new ComboboxItem();
                    item.Text = LANGUAGE_WORDS[lang_match.ToString()].ToString();
                    item.TranslationName = lang_match.ToString();
                    item.Value = m_file;

                    Launcher.staticCBOLanguages.Items.Add(item);
                    LANGUAGE.Add(m_file, LANGUAGE_WORDS);
                }
            }
            catch (Exception ex)
            {
                StackFrame[] stFrames = new StackTrace(ex).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", ex.Message), String.Format("Details: {0}", ex.ToString()) };
                log.WriteLog(write);
            }
            return LANGUAGE;
        }
    }
}
