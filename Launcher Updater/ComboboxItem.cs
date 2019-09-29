using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher_Updater
{
    class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public string TranslationName { get; set; }

        public override string ToString()
        {
            return Text;
        }

    }
}
