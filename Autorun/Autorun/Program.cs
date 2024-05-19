
#region using
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
#endregion

namespace Autorun {
    internal class Program {

        [STAThread]
        static void Main(string[] args) {

            var commandLineArgs = Environment.GetCommandLineArgs();

            if (commandLineArgs.Contains("-added")) {
                string appPath = commandLineArgs[2];

                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(
                    "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                registryKey.SetValue(Path.GetFileNameWithoutExtension(appPath), appPath);
                registryKey.Close();
                return;
            }
            else if (commandLineArgs.Contains("-removeReg")) {
                RegistryKey _key = Registry.ClassesRoot.OpenSubKey("exefile\\Shell", true);
                _key.DeleteSubKeyTree("Add to Autorun");
                _key.Close();
                return;
            }
            else {
                RegistryKey _key = Registry.ClassesRoot.OpenSubKey("exefile\\Shell", true);
                RegistryKey newkey = _key.CreateSubKey("Add to Autorun");
                RegistryKey subNewkey = newkey.CreateSubKey("Command");
                subNewkey.SetValue("", Environment.CurrentDirectory + "\\Autorun.exe -added \"%1\"");
                subNewkey.Close();
                newkey.Close();
                _key.Close();
                return;
            }
        }
    }
}
