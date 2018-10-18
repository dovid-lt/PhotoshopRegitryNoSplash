using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhotoshopRegitryNoSplash
{
    class RegistryReplace
    {

        public void Replace(bool clean)
        {
            var keysFixed = new[] {
                @"Applications\Photoshop.exe\shell\edit\command",
                @"Applications\Photoshop.exe\shell\open\command"
            };

            var keysScan = Registry.ClassesRoot.GetSubKeyNames().Where(a => a.StartsWith("Photoshop.")).Select(a => a + @"\shell\open\command");

            if (clean)
                foreach (var element in keysFixed.Concat(keysScan))
                    CleanKey(element);
            else
                foreach (var element in keysFixed.Concat(keysScan))
                    ReplaceInKey(element);
        }


        public List<Tuple<string, string, string>> effecteds = new List<Tuple<string, string, string>>();

        Regex reg = new Regex("Photoshop.exe\"\\s+\"%1\"\\s*$");

        void ReplaceInKey(string subkey)
        {
            using (var key = Registry.ClassesRoot.OpenSubKey(subkey, true))
            {
                if (key == null) return;
                string val = key.GetValue(null).ToString();
                if (reg.IsMatch(val))
                {
                    key.SetValue(null, val + " -NoSplash");
                    effecteds.Add(Tuple.Create(key.Name, val, val + " -NoSplash"));
                }
            }
        }

        void CleanKey(string subkey)
        {
            using (var key = Registry.ClassesRoot.OpenSubKey(subkey, true))
            {
                if (key == null) return;
                string val = key.GetValue(null).ToString();
                if (val.Contains(" -NoSplash"))
                {
                    var rep = val.Replace(" -NoSplash", "");
                    key.SetValue(null, rep);
                    effecteds.Add(Tuple.Create(key.Name, val, rep));
                }
            }
        }


    }
}
