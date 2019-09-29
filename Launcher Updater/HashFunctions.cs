using System;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Launcher_Updater
{
    class HashFunctions
    {
        public string CalculateMD5(string filename)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filename))
                    {
                        var hash = md5.ComputeHash(stream);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "NONE";
            }
        }
    }
}
