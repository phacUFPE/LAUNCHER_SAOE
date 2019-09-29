using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Launcher_Updater
{
    class Errors
    {
        Exception ex;
        UriFormatException uriEx;
        WebException wEx;

        public DialogResult ShowError()
        {
            string errorMessage = "";
            if (ex.Equals(wEx))
            {
                errorMessage = "Connection Error: {0}";
            }
            else if (ex.Equals(uriEx))
            {
                errorMessage = "Uri Error: {0}";
            }
            else
            {
                errorMessage = "Unknow Error: {0}";
            }

            return MessageBox.Show(String.Format(errorMessage, ex.Message), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}