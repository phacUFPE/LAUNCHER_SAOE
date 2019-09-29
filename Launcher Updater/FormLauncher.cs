using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Drawing;
using System.Threading.Tasks;

namespace Launcher_Updater
{
    public partial class Launcher : Form
    {

        protected string KEY = "A3L5p9isp0623sap2";

        //HASH LIST
        HashSet<string> hset = new HashSet<string>();

        //CLASSES
        HashFunctions h = new HashFunctions();
        IniFile ini = new IniFile();
        RegexFunctions rfunc = new RegexFunctions();
        static Log log = new Log();

        //LINKS
        string FACEBOOK;
        public static string SITE_PWONDER;
        string CREATE_ACCOUNT;
        string DONATE;

        //CONST's    
        public static bool G_THREAD_RUNNING;

        public static string G_ENGINE = IniFile.DEFAULT_ENGINE;
        public static string G_VERSION = IniFile.DEFAULT_VERSION;
        public static string G_LANGUAGE = IniFile.DEFAULT_LANGUAGE;
        public static string G_OTC_LANG;
        public static string G_OTC_NLANG;
        public static bool G_OTC_FULLSCREEN;
        public static bool G_OTC_NFULLSCREEN;
        //public static string WEB_DIRECTORY = "http://localhost/updates/";
        public static string WEB_DIRECTORY = "http://swordarteron.com.br/content/client/updates/";
        //http://swordarteron.com.br/content/client/updates/
        //http://saoeronclosed2.servegame.com:8000/

        public static Hashtable LANGUAGES;
        public static Hashtable LANG;

        static PictureBox staticProgressFile;
        static PictureBox staticProgressTotal;
        static BackgroundWorker staticBckWorker;
        static Label staticLblInfo;
        static Button staticBtnMain;
        public static ComboBox staticCBOLanguages;

        //USEFUL VARIABLES     
        bool eWarned = false;
        static string SERVER_CURRENT_VER;
        static string SERVER_MIN_VER;
        //static string TEXT_OTML;
        static string CURRENT_USER = Environment.UserName;
        //static string CONFIG_OTML = "config.otml";
        //static string OTC_NAME = "PWO";
        //static string PATH_OTML = @"C:\Users\{0}\{1}\";

        //[DIR] AND [PARENTDIR] ALWAYS THE LAST IN ARRAY
        string[] doNotDownload = { "Parent Directory", "Description", "_version", "_hlist", "SAOE.exe", "_links", "Hasher.exe", "[PARENTDIR]", "[DIR]" };

        string serverHashList = "_hlist";
        string serverVersionName = "_version";
        string serverLinksName = "_links";

        string launcherNAME = "SAOE";
        string oldLauncherNAME = "SAOE_old";
        string newLauncherNAME = "SAOE_new";

        string fileDownloading;

        bool openNewLauncher = false;

        static int status;
        static bool repairing = false;
        static int fullWidthPCT;

        int fileCount;
        double filesDownloaded = 1;

        static List<string> usedFiles = new List<string>();

        decimal totalBytes;
        decimal bytesReceived;               

        bool warned = false;

        bool openingClient = false;

        //BORDERLESS FORM MOVEMENT

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.ApplicationExitCall) && !openingClient)
            {
                if (MessageBox.Show(LANG["EXIT_LAUNCHER"].ToString(), LANG["EXIT"].ToString(), MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            ini.NewWrite();

            if (openNewLauncher)
            {
                Process processLauncher = new Process();
                processLauncher.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + doNotDownload[4];
                processLauncher.Start();
            }

            base.OnFormClosing(e);
        }

        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StackFrame[] stFrames = new StackTrace((e.ExceptionObject as Exception)).GetFrames();
            string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", (e.ExceptionObject as Exception).Message), String.Format("Details: {0}", (e.ExceptionObject as Exception).ToString()) };
            log.WriteLog(write);
        }

        public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            StackFrame[] stFrames = new StackTrace(e.Exception).GetFrames();
            string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", e.Exception.Message), String.Format("Details: {0}", e.Exception.ToString()) };
            log.WriteLog(write);
        }

        public Launcher()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            InitializeComponent();

            ComboboxItem dx9Item = new ComboboxItem();
            ComboboxItem opglItem = new ComboboxItem();
            dx9Item.Value = "Dx9";
            dx9Item.Text = "Directx9";

            opglItem.Value = "OPGL";
            opglItem.Text = "OpenGL";

            cboEngine.Items.Add(dx9Item);
            cboEngine.Items.Add(opglItem);

            staticProgressFile = pctFileProgress;
            staticProgressTotal = pctTotalProgress;
            staticLblInfo = lblInfo;
            staticBckWorker = backgroundWorker;
            staticBtnMain = btnMain;
            staticCBOLanguages = cboLanguages;

            fullWidthPCT = pctTotalProgress.Width;
            pctFileProgress.Width = 0;
            pctTotalProgress.Width = 0;

            LANGUAGES = Language.LoadLanguages();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CboLanguages_SelectionChangeCommitted(object sender, EventArgs e)
        {
            G_LANGUAGE = (cboLanguages.SelectedItem as ComboboxItem).Value.ToString();
            UpdateLangLabels();
            UpdateComboboxLanguages();
        }

        public void UpdateComboboxLanguages()
        {
            for (int i = 0; i < cboLanguages.Items.Count; i++)
            {
                ComboboxItem item = new ComboboxItem();
                item.Value = (cboLanguages.Items[i] as ComboboxItem).Value.ToString();
                item.TranslationName = (cboLanguages.Items[i] as ComboboxItem).TranslationName;
                item.Text = LANG[(cboLanguages.Items[i] as ComboboxItem).TranslationName].ToString();
                cboLanguages.Items[i] = item;
                if ((cboLanguages.Items[i] as ComboboxItem).Value.ToString() == G_LANGUAGE) { cboLanguages.SelectedIndex = i; }
            }
        }

        public void UpdateLangLabels()
        {
            LANG = (Hashtable)LANGUAGES[G_LANGUAGE];
            UpdateComboboxLanguages();
            lblRegister.Text = LANG["REGISTER"].ToString();
            lblDonate.Text = LANG["DONATE"].ToString();
            lblCurrent.Text = LANG["FILE"].ToString();
            lblTotal.Text = LANG["TOTAL"].ToString();
            btnRepair.Text = LANG["REPAIR_GAME"].ToString();
            if (status == 0)
            {
                btnMain.Text = LANG["UPDATE"].ToString();
                lblInfo.Text = LANG["CHECK_FILES"].ToString();
            }
            else if (status == 1)
            {
                btnMain.Text = LANG["UPDATE"].ToString();
                lblInfo.Text = String.Format("{0}{1}",LANG["DOWNLOADING"].ToString(), fileDownloading);
            }
            else if (status == 2)
            {
                btnMain.Text = LANG["PLAY"].ToString();
                lblInfo.Text = LANG["UPDATED"].ToString();
            }
            else if (status == 3)
            {
                btnMain.Text = LANG["UPDATE"].ToString();
                lblInfo.Text = LANG["AVAILABLE_UPDATE"].ToString();
            }
        }

        public void WriteVersion()
        {
            try
            {
                G_VERSION = SERVER_CURRENT_VER;
                ini.NewWrite();                
            }
            catch (FileNotFoundException fnfEx)
            {
                StackFrame[] stFrames = new StackTrace(fnfEx).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", fnfEx.Message), String.Format("Details: {0}", fnfEx.ToString()) };
                log.WriteLog(write);
                MessageBox.Show(String.Format("File Error: {0}", fnfEx.Message), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                StackFrame[] stFrames = new StackTrace(ex).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", ex.Message), String.Format("Details: {0}", ex.ToString()) };
                log.WriteLog(write);
                MessageBox.Show(String.Format("Unknow Error: {0}", ex.Message));
            }
        }

        public void CheckForUpdates()
        {
            string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (SERVER_CURRENT_VER == null) { return; }
            if (G_VERSION == "")
            {
                backgroundWorker.RunWorkerAsync();
                WriteVersion();
            }
            else if (G_VERSION != SERVER_CURRENT_VER)
            {
                status = 3;
                btnMain.Text = LANG["UPDATE"].ToString();
                lblInfo.Text = LANG["AVAILABLE_UPDATE"].ToString();
            }
            else
            {
                status = 2;
                lblInfo.Text = LANG["UPDATED"].ToString();
                btnMain.Text = LANG["PLAY"].ToString();
                pctFileProgress.Width = fullWidthPCT;
                pctTotalProgress.Width = fullWidthPCT;
            }
        }

        public void GetLinks()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(WEB_DIRECTORY + serverLinksName);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string links = reader.ReadToEnd();
                        Regex regex = new Regex(rfunc.GetRegexLinks(links));
                        MatchCollection matches = regex.Matches(links);
                        if (matches.Count > 0)
                        {
                            foreach (Match match in matches)
                            {
                                FACEBOOK = match.Groups["facebook"].ToString().Replace("\n", "").Trim();
                                SITE_PWONDER = match.Groups["site"].ToString().Replace("\n", "").Trim();
                                DONATE = match.Groups["donate"].ToString().Replace("\n", "").Trim();
                                CREATE_ACCOUNT = match.Groups["register"].ToString().Replace("\n", "").Trim();
                            }

                            lblFacebook.Visible = true;
                            lblSite.Visible = true;
                            lblDonate.Visible = true;
                            lblRegister.Visible = true;
                        }
                    }
                }
            }
            catch (WebException wEx)
            {
                if (SITE_PWONDER is null || SITE_PWONDER == "") { SITE_PWONDER = "www.pokewonder.com.br"; }
                StackFrame[] stFrames = new StackTrace(wEx).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", wEx.Message), String.Format("Details: {0}", wEx.ToString()) };
                log.WriteLog(write);
                if (eWarned) { return; }
                MessageBox.Show(String.Format("Connection Error: {0}", wEx.Message), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                eWarned = true;
            }
            catch (Exception ex)
            {
                StackFrame[] stFrames = new StackTrace(ex).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", ex.Message), String.Format("Details: {0}", ex.ToString()) };
                log.WriteLog(write);
                MessageBox.Show(String.Format("Unknow Error: {0}", ex.Message), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GetServerVersion()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(WEB_DIRECTORY + serverVersionName);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string version = reader.ReadToEnd();
                        Regex regex = new Regex(rfunc.GetRegexVersion(version));
                        MatchCollection matches = regex.Matches(version);
                        if (matches.Count > 0)
                        {
                            foreach (Match match in matches)
                            {
                                SERVER_CURRENT_VER = match.Groups["ver"].ToString().Replace("\n", "").Trim();
                                SERVER_MIN_VER = match.Groups["min_ver"].ToString().Replace("\n", "").Trim();
                            }
                        }
                    }
                }
            }
            catch (WebException wEx)
            {
                StackFrame[] stFrames = new StackTrace(wEx).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", wEx.Message), String.Format("Details: {0}", wEx.ToString()) };
                log.WriteLog(write);
                if (eWarned) { return; }
                MessageBox.Show(String.Format("Connection Error: {0}", wEx.Message), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                eWarned = true;
            }
            catch(Exception ex)
            {
                StackFrame[] stFrames = new StackTrace(ex).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", ex.Message), String.Format("Details: {0}", ex.ToString()) };
                log.WriteLog(write);
                if (eWarned) { return; }
                MessageBox.Show(String.Format("Unknow Error: {0}", ex.Message), "Erro!",MessageBoxButtons.OK, MessageBoxIcon.Error);
                eWarned = true;
            }
        }

        private void BtnMain_Click(object sender, EventArgs e)
        {
            if (btnMain.Text == LANG["PLAY"].ToString())
            {
                ini.NewWrite();
                string otc = ConfigurationManager.AppSettings[G_ENGINE];
                try
                {
                    openingClient = true;
                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + String.Format("{0}.exe", otc), KEY);
                    Application.Exit();
                }
                catch (FileNotFoundException fnfEx)
                {
                    StackFrame[] stFrames = new StackTrace(fnfEx).GetFrames();
                    string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", fnfEx.Message), String.Format("Details: {0}", fnfEx.ToString()) };
                    log.WriteLog(write);
                    MessageBox.Show(String.Format("File Error: {0}", fnfEx.Message), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    StackFrame[] stFrames = new StackTrace(ex).GetFrames();
                    string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", ex.Message), String.Format("Details: {0}", ex.ToString()) };
                    log.WriteLog(write);
                    MessageBox.Show(String.Format("Unknow Error: {0}", ex.Message), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            lblInfo.Text = LANG["CHECK_UPDATES"].ToString();
            btnMain.Enabled = false;
            backgroundWorker.RunWorkerAsync();
        }        

        public void CountTotalFiles(string directory)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(directory);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string text = reader.ReadToEnd();
                    Regex regex = new Regex(rfunc.GetRegexCountHashs(text));
                    MatchCollection matches = regex.Matches(text);
                    if (matches.Count > 0)
                    {                        
                        foreach (Match match in matches)
                        {
                            fileCount = Convert.ToInt32(match.Groups["count"].ToString());
                        }
                    }
                }
            }
        }

        public void FillHashSet(string webDir)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webDir);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string hashs = reader.ReadToEnd();
                Regex reg = new Regex(rfunc.GetRegexHashs(webDir));
                MatchCollection mc = reg.Matches(hashs);
                if (mc.Count > 0)
                {
                    foreach (Match match in mc)
                    {
                        string hash = match.Groups["hash"].ToString();
                        try
                        {
                            hset.Add(hash);
                        }
                        catch
                        {
                            //MessageBox.Show(String.Format("NAME: {0}\nHASH: {1}\nERROR: {2}", name, hash, ex.Message));
                        }
                    }
                }
            }
            catch (WebException wEx)
            {
                StackFrame[] stFrames = new StackTrace(wEx).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", wEx.Message), String.Format("Details: {0}", wEx.ToString()) };
                log.WriteLog(write);
                if (eWarned) { return; }
                MessageBox.Show(String.Format("Connection Error: {0}", wEx.Message), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                eWarned = true;
            }
            catch (Exception ex)
            {
                StackFrame[] stFrames = new StackTrace(ex).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", ex.Message), String.Format("Details: {0}", ex.ToString()) };
                log.WriteLog(write);
                MessageBox.Show(String.Format("Unknow Error: {0}", ex.Message), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            long lRec = e.BytesReceived;
            long lTRec = e.TotalBytesToReceive;
            bytesReceived = (e.BytesReceived / 1024);
            totalBytes = (e.TotalBytesToReceive / 1024);
            //bytesPerSecond = Math.Round(((double)bytesReceived / watch.Elapsed.TotalSeconds), 2);
            int iRProgress;
            try
            {
                iRProgress = (int)((bytesReceived / totalBytes) * 100);
            }
            catch
            {
                iRProgress = 100;
            }
            backgroundWorker.ReportProgress(iRProgress);
        }

        static Stopwatch sw = new Stopwatch();

        private void DownloadFile(string BaseDirectory, string directory, string fileName)
        {
            try
            {
                if (filesDownloaded < fileCount) { filesDownloaded += 1; }
                fileDownloading = fileName.Replace("%20", " ");

                status = 1;

                WebClient wClient = new WebClient();

                Uri url = new Uri(directory + fileName);

                if (fileName == String.Format("{0}.exe", launcherNAME)) { fileName = fileName.Replace(launcherNAME, newLauncherNAME); }
                using (Stream streamRemote = wClient.OpenRead(url))
                using (FileStream streamLocal = new FileStream(BaseDirectory + fileName.Replace("%20", " "), FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    int bytesRead = 0;

                    byte[] byteBuffer = new byte[32768];

                    Int64 iRunningByteTotal = 0;

                    Int64 iSize = Convert.ToInt64(wClient.ResponseHeaders["Content-Length"]);

                    if (sw.ElapsedMilliseconds > 0) { sw.Reset(); }
                    sw.Start();

                    while ((bytesRead = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                    {
                        iRunningByteTotal += bytesRead;

                        streamLocal.Write(byteBuffer, 0, bytesRead);

                        bytesReceived = Math.Round((decimal)iRunningByteTotal / 1024);
                        totalBytes = Math.Round((decimal)iSize / 1024);
                        decimal dProgressPercentage;
                        try
                        {
                            dProgressPercentage = (bytesReceived / totalBytes);
                        }
                        catch
                        {
                            dProgressPercentage = 1;
                        }

                        int progressPerc = (int)(dProgressPercentage * 100);

                        backgroundWorker.ReportProgress(progressPerc);
                    }
                    sw.Stop();
                }
            }
            catch (Exception ex)
            {
                StackFrame[] stFrames = new StackTrace(ex).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", ex.Message), String.Format("Details: {0}", ex.ToString()) };
                log.WriteLog(write);
                MessageBox.Show(String.Format("Unknow Error: {0}", ex.Message), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DownloadAndReloadLauncher(string directory)
        {
            try
            {
                string fileName = doNotDownload[4];
                string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                HttpWebRequest requestFile = (HttpWebRequest)WebRequest.Create(directory + fileName);
                HttpWebResponse responseFile = (HttpWebResponse)requestFile.GetResponse();
                responseFile.Close();

                DownloadFile(BaseDirectory, directory, fileName);

                File.Move(BaseDirectory + fileName, BaseDirectory + fileName.Replace(launcherNAME, oldLauncherNAME));
                File.Move(BaseDirectory + fileName.Replace(launcherNAME, newLauncherNAME), BaseDirectory + fileName);
                WriteVersion();

                openNewLauncher = true;
                openingClient = true;

                //DeleteNonExistentFiles(AppDomain.CurrentDomain.BaseDirectory);
                Application.Exit();
            }
            catch (DirectoryNotFoundException dnfEx)
            {
                StackFrame[] stFrames = new StackTrace(dnfEx).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", dnfEx.Message), String.Format("Details: {0}", dnfEx.ToString()) };
                log.WriteLog(write);
                MessageBox.Show(String.Format("Directory Error: {0}", dnfEx.Message), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FileNotFoundException fnfEx)
            {
                StackFrame[] stFrames = new StackTrace(fnfEx).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", fnfEx.Message), String.Format("Details: {0}", fnfEx.ToString()) };
                log.WriteLog(write);
                MessageBox.Show(String.Format("File Error: {0}", fnfEx.Message), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex)
            {
                StackFrame[] stFrames = new StackTrace(ex).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", ex.Message), String.Format("Details: {0}", ex.ToString()) };
                log.WriteLog(write);
                MessageBox.Show(String.Format("Unknow Error: {0}", ex.Message), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeleteIFExists(string dir, string fName)
        {
            if (File.Exists(dir + fName.Replace("%20", " ")))
            {
                File.Delete(dir + fName.Replace("%20", " "));
            }
        }

        public void DeleteNonExistentFiles(string dir)
        {
            if (!Directory.Exists(dir)) { return; }
            string[] files = Directory.GetFiles(dir);
            foreach (string file in files)
            {
                foreach (string uFile in usedFiles)
                {
                    if (usedFiles.Contains(file.Replace(dir, "")) || file.Contains("log.txt") || 
                        file.Contains(String.Format("{0}.exe", oldLauncherNAME)) ||
                        file.Contains(String.Format("{0}.exe", newLauncherNAME)) ||
                        file.Contains(String.Format("{0}.exe", launcherNAME)) ||
                        file.Contains("en.lang") ||
                        file.Contains("pt.lang") ||
                        file.Contains("Config.ini")) { break; }
                    else { File.Delete(file); }
                }
            }

            string[] dirs = Directory.GetDirectories(dir);
            foreach (string d in dirs)
            {
                DeleteNonExistentFiles(d);
            }
        }

        public void DownloadAllFiles(string directory)
        {     
            string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;   
            string more = directory.Remove(0, WEB_DIRECTORY.Length);
            BaseDirectory = BaseDirectory + more;

            G_THREAD_RUNNING = backgroundWorker.IsBusy;

            if (!Directory.Exists(BaseDirectory))
            {
                Directory.CreateDirectory(BaseDirectory);
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(directory);
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string html = reader.ReadToEnd();
                        Regex regex = new Regex(rfunc.GetRegexHTML(directory));

                        MatchCollection matches = regex.Matches(html);

                        if (matches.Count > 0)
                        {
                            foreach (Match match in matches)
                            {
                                string fileName = match.Groups["name"].ToString();
                                string fileType = match.Groups["type"].ToString();

                                if (match.Success)
                                {
                                    bool fileForbidden = false;
                                    foreach (string str in doNotDownload)
                                    {
                                        if (fileName.Contains(str) || (fileType.Contains(doNotDownload[doNotDownload.Length - 1])))
                                        {
                                            fileForbidden = true;
                                            break;
                                        }
                                    }
                                    if (!fileForbidden && !fileType.Contains(doNotDownload[doNotDownload.Length - 2]))
                                    {
                                        if (fileName == String.Format("{0}.exe", newLauncherNAME))
                                        { usedFiles.Add(String.Format("{0}.exe", launcherNAME)); }
                                        else { usedFiles.Add(fileName); }
                                        try
                                        {
                                            if (string.Compare(G_VERSION, SERVER_MIN_VER) != -1)
                                            {
                                                if (File.Exists(BaseDirectory + fileName.Replace("%20", " ")))
                                                {
                                                    if (filesDownloaded < fileCount) { filesDownloaded += 1; }
                                                    try
                                                    {
                                                        if (CheckIntegrity(BaseDirectory, fileName.Replace("%20", " ")))
                                                        {
                                                            status = 0;
                                                            backgroundWorker.ReportProgress(0);
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            DeleteIFExists(BaseDirectory, fileName);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        StackFrame[] stFrames = new StackTrace(ex).GetFrames();
                                                        string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", ex.Message), String.Format("Details: {0}", ex.ToString()) };
                                                        log.WriteLog(write);
                                                        MessageBox.Show(String.Format("Unknow Error: {0}", ex.Message), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }
                                                }
                                            }
                                            DownloadFile(BaseDirectory, directory, fileName);
                                            try
                                            {
                                                if (CheckIntegrity(BaseDirectory, fileName.Replace("%20", " ")))
                                                {
                                                    continue;
                                                }
                                                else
                                                {
                                                    DeleteIFExists(BaseDirectory, fileName);
                                                    DownloadFile(BaseDirectory, directory, fileName);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                StackFrame[] stFrames = new StackTrace(ex).GetFrames();
                                                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", ex.Message), String.Format("Details: {0}", ex.ToString()) };
                                                log.WriteLog(write);
                                                MessageBox.Show(String.Format("Unknow Error: {0}", ex.Message), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        catch
                                        {
                                            //DO NOTHING
                                        }
                                    }
                                    else if (fileType.Contains(doNotDownload[doNotDownload.Length - 1]))
                                    {
                                        DownloadAllFiles(response.ResponseUri.ToString() + fileName);
                                    }                                    
                                }                                
                            }
                        }
                    }
                }
                if (filesDownloaded == fileCount-1)
                {
                    if (string.Compare(G_VERSION, SERVER_MIN_VER) != -1)
                    {
                        string iLauncher = h.CalculateMD5(BaseDirectory + launcherNAME.Insert(launcherNAME.Length, ".exe"));
                        if (!hset.Contains(iLauncher))
                        {
                            DownloadAndReloadLauncher(WEB_DIRECTORY);
                        }
                    }
                    else
                    {
                        DownloadAndReloadLauncher(WEB_DIRECTORY);
                    }
                    return;
                }
                if (filesDownloaded > 580) { MessageBox.Show("Count: " + fileCount + "\nDownloaded: " + filesDownloaded); }
            }
            catch (WebException wEx)
            {
                StackFrame[] stFrames = new StackTrace(wEx).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", wEx.Message), String.Format("Details: {0}", wEx.ToString()) };
                log.WriteLog(write);
                if (eWarned) { return; }
                MessageBox.Show(String.Format("Connection Error: {0}", wEx.Message), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                eWarned = true;
            }
            catch(Exception ex)
            {
                StackFrame[] stFrames = new StackTrace(ex).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", ex.Message), String.Format("Details: {0}", ex.ToString()) };
                log.WriteLog(write);
                MessageBox.Show(String.Format("Unknow Error: {0}", ex.Message), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private bool CheckIntegrity(string BaseDirectory, string fileName)
        {
            string rootFile = h.CalculateMD5(BaseDirectory + fileName);
            if (hset.Contains(rootFile))
            {
                return true;
            }
            return false;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            filesDownloaded = 1;
            CountTotalFiles(WEB_DIRECTORY + serverHashList);
            DownloadAllFiles(WEB_DIRECTORY);
            if (!repairing)
            {
                G_VERSION = SERVER_CURRENT_VER;
                status = 0;
                filesDownloaded = 1;
                DownloadAllFiles(WEB_DIRECTORY);
            }
            repairing = false;
        }             

        public static void RepairGame()
        {
            repairing = true;
            G_THREAD_RUNNING = staticBckWorker.IsBusy;
            if (G_THREAD_RUNNING) { MessageBox.Show(LANG["LAUNCHER_IN_DOWN"].ToString()); return; }
            status = 0;
            staticProgressFile.Width = fullWidthPCT;
            staticProgressTotal.Width = 0;
            staticLblInfo.Text = LANG["CHECK_FILES"].ToString();
            staticBckWorker.RunWorkerAsync();
            G_THREAD_RUNNING = staticBckWorker.IsBusy;
            staticBtnMain.Enabled = false;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblCount.Text = String.Format("{0} / {1}", filesDownloaded, fileCount);
            int percent = (int)Math.Round(((filesDownloaded / fileCount) * 100));
            if (percent > 100) { percent = 100; }
            int fProgressWidth = (int)Math.Round((fullWidthPCT * 0.01) * e.ProgressPercentage);
            int tProgressWidth = (int)Math.Round((fullWidthPCT * 0.01) * percent);
            if (fProgressWidth > fullWidthPCT) { fProgressWidth = fullWidthPCT; }
            if (tProgressWidth > fullWidthPCT) { tProgressWidth = fullWidthPCT; }                        
            pctTotalProgress.Width = tProgressWidth;
            if (status == 1)
            {
                string byteType = "KB";
                string sTotalBytes = totalBytes.ToString();
                string sBytesReceived = bytesReceived.ToString();
                if (sTotalBytes.Length > 3)
                {
                    sTotalBytes = sTotalBytes.Insert((sTotalBytes.Length - 3), ",");                        
                }
                if (sBytesReceived.Length > 3)
                {
                    sBytesReceived = sBytesReceived.Insert((sBytesReceived.Length - 3), ",");
                }
                pctFileProgress.Width = fProgressWidth;
                lblSize.Text = String.Format("{0:0,0}/{1} {2}", sBytesReceived, sTotalBytes, byteType);
                if (fileDownloading.Length-1 >= 20) { fileDownloading = fileDownloading.Remove(20) + "..."; }
                lblInfo.Text = String.Format(LANG["DOWNLOADING"] + "{0}", fileDownloading);
                //lblKBs.Text = String.Format("{0} KB/s", bytesPerSecond);
            }        
            else if (status == 0)
            {
                pctFileProgress.Width = fullWidthPCT;
                lblSize.Text = "";                
                lblInfo.Text = LANG["CHECK_UPDATES"].ToString();                
            }            
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            status = 2;
            WriteVersion();
            lblClientVer.Text = String.Format("Client Ver: {0}", G_VERSION);
            btnMain.Text = LANG["PLAY"].ToString();
            lblInfo.Text = LANG["UPDATED"].ToString();
            lblCount.Text = "";
            lblSize.Text = "";
            lblKBs.Text = "";
            pctFileProgress.Width = fullWidthPCT;
            pctTotalProgress.Width = fullWidthPCT;
            btnMain.Enabled = true;
            G_THREAD_RUNNING = backgroundWorker.IsBusy;
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            //NOTHING
        }

        private void LblNews_Click(object sender, EventArgs e)
        {
            //NOTHING
        }

        private void RedirectLabels_Click(object sender, EventArgs e)
        {
            if (lblSite.Equals(sender) && !(SITE_PWONDER is null || SITE_PWONDER == ""))
            {
                Process.Start(SITE_PWONDER);
            }
            else if (lblRegister.Equals(sender) && !(CREATE_ACCOUNT is null || CREATE_ACCOUNT == ""))
            {
                Process.Start(CREATE_ACCOUNT);
            }
            else if (lblDonate.Equals(sender) && !(DONATE is null || DONATE == ""))
            {
                Process.Start(DONATE);
            }
            else if (lblFacebook.Equals(sender) && !(FACEBOOK is null || FACEBOOK == ""))
            {
                Process.Start(FACEBOOK);
            }
            else
            {
                return;
            }
            if (!warned) { warned = true; MessageBox.Show(LANG["OPEN_BROWSER"].ToString()); }
        }

        private void BtnRepair_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(btnRepair, LANG["REPAIR_GAME"].ToString());
        }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Launcher_Updater.Properties.Resources.close;
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Launcher_Updater.Properties.Resources.close_white;
        }

        private void BtnMinimize_MouseEnter(object sender, EventArgs e)
        {
            btnMinimize.BackgroundImage = Launcher_Updater.Properties.Resources.minimize_golden;
        }

        private void BtnMinimize_MouseLeave(object sender, EventArgs e)
        {
            btnMinimize.BackgroundImage = Launcher_Updater.Properties.Resources.minimize_white;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            lblCurrent.Focus();
            Application.Exit();
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            lblCurrent.Focus();
            this.WindowState = FormWindowState.Minimized;
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            if (sender == lblSite)
            {
                lblSite.Font = new System.Drawing.Font(lblSite.Font.Name, lblSite.Font.Size, System.Drawing.FontStyle.Underline | System.Drawing.FontStyle.Bold);
            }
            else if (sender == lblRegister)
            {
                lblRegister.Font = new System.Drawing.Font(lblRegister.Font.Name, lblRegister.Font.Size, System.Drawing.FontStyle.Underline | System.Drawing.FontStyle.Bold);
            }
            else if (sender == lblDonate)
            {
                lblDonate.Font = new System.Drawing.Font(lblDonate.Font.Name, lblDonate.Font.Size, System.Drawing.FontStyle.Underline | System.Drawing.FontStyle.Bold);
            }
            else if (sender == lblFacebook)
            {
                lblFacebook.Font = new System.Drawing.Font(lblFacebook.Font.Name, lblFacebook.Font.Size, System.Drawing.FontStyle.Underline | System.Drawing.FontStyle.Bold);
            }
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            if (sender == lblSite)
            {
                lblSite.Font = new System.Drawing.Font(lblSite.Font.Name, lblSite.Font.Size, System.Drawing.FontStyle.Bold);
            }
            else if (sender == lblRegister)
            {
                lblRegister.Font = new System.Drawing.Font(lblRegister.Font.Name, lblRegister.Font.Size, System.Drawing.FontStyle.Bold);
            }
            else if (sender == lblDonate)
            {
                lblDonate.Font = new System.Drawing.Font(lblDonate.Font.Name, lblDonate.Font.Size, System.Drawing.FontStyle.Bold);
            }
            else if (sender == lblFacebook)
            {
                lblFacebook.Font = new System.Drawing.Font(lblFacebook.Font.Name, lblFacebook.Font.Size, System.Drawing.FontStyle.Bold);
            }
        }

        private void BtnRepair_Click(object sender, EventArgs e)
        {
            RepairGame();
        }

        private void CboEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            G_ENGINE = (cboEngine.SelectedItem as ComboboxItem).Value.ToString();
        }

        private void TestConnection()
        {
            HttpWebResponse response = null;
            try
            {
                WebRequest request = WebRequest.Create(WEB_DIRECTORY);
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException wEx)
            {
                StackFrame[] stFrames = new StackTrace(wEx).GetFrames();
                if (eWarned) { return; }
                MessageBox.Show(String.Format("Connection Error: {0}", wEx.Message), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", wEx.Message), String.Format("Details: {0}", wEx.ToString()) };
                log.WriteLog(write);
            }
            catch (Exception ex)
            {
                StackFrame[] stFrames = new StackTrace(ex).GetFrames();
                if (eWarned) { return; }
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", ex.Message), String.Format("Details: {0}", ex.ToString()) };
                log.WriteLog(write);
            }

            if (response != null)
            {
                GetServerVersion();
                GetLinks();
                FillHashSet(WEB_DIRECTORY + serverHashList);
            }
        }

        private void Launcher_Shown(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + String.Format("{0}.exe", oldLauncherNAME)))
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + String.Format("{0}.exe", oldLauncherNAME));
                }
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + IniFile.EXE))
                {
                    ini.CreateIni(AppDomain.CurrentDomain.BaseDirectory + IniFile.EXE);
                    ini.NewWrite();
                    ini.NewRead();
                }
                else
                {
                    ini.CreateIni(AppDomain.CurrentDomain.BaseDirectory + IniFile.EXE);
                    ini.NewRead();
                }

                if (G_ENGINE == "Dx9")
                {
                    cboEngine.SelectedIndex = 0;
                }
                else
                {
                    cboEngine.SelectedIndex = 1;
                }
            }
            catch
            {
                //DO NOTHING
            }

            TestConnection();

            try
            {
                LANG = (Hashtable)LANGUAGES[G_LANGUAGE];
                ComboboxItem item = new ComboboxItem();
                UpdateComboboxLanguages();
                lblClientVer.Text = String.Format("Client Ver: {0}", G_VERSION);
                lblLauncherVer.Text = String.Format("Launcher Ver: {0}", Assembly.GetEntryAssembly().GetName().Version.ToString());
                lblCurrent.Text = LANG["FILE"].ToString();
                lblRegister.Text = LANG["REGISTER"].ToString();
                lblDonate.Text = LANG["DONATE"].ToString();
                btnRepair.Text = LANG["REPAIR_GAME"].ToString();
            }
            catch (Exception ex)
            {
                StackFrame[] stFrames = new StackTrace(ex).GetFrames();
                string[] write = { String.Format("Function: {0}", stFrames[stFrames.Length - 1].GetMethod().Name), String.Format("Message: {0}", ex.Message), String.Format("Details: {0}", ex.ToString()) };
                log.WriteLog(write);
            }
            CheckForUpdates();
        }
    }
}
