using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace beakn
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        private string keyPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private string keyName = "beakn";
        private string exeName = "beakn.exe";

        public override void Commit(System.Collections.IDictionary savedState)
        {
            base.Commit(savedState);
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath, true))
            {
                key.SetValue(keyName, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + exeName);
            }
        }

        public override void Rollback(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath, true))
            {
                key.DeleteValue(keyName, false);
            }
        }

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath, true))
            {
                key.DeleteValue(keyName, false);
            }
        }
    }
}
