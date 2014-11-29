using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace beakn.desktop.mqtt
{
    [RunInstaller(true)]
    public class Installer: System.Configuration.Install.Installer
    {
        public override void Commit(System.Collections.IDictionary savedState)
        {
            base.Commit(savedState);
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("beakn.desktop.mqtt", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\beakn.exe");
            }
        }

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue("beakn.desktop.mqtt", false);
            }
        }
    }
}
