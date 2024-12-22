using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FR_SDK.Core
{
    //https://github.com/Jamedjo/RSTabExplorer/blob/master/RockSmithTabExplorer/Services/RocksmithLocator.cs
    [SupportedOSPlatform("windows")]
    public static class FirefightReloadedLocator
    {
        public static string SteamFolder()
        {
            RegistryKey steamKey = Registry.LocalMachine.OpenSubKey("Software\\Valve\\Steam");

            if (Environment.Is64BitOperatingSystem)
            {
                steamKey = Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\Valve\\Steam");
            }

            return steamKey.GetValue("InstallPath").ToString();
        }

        public static List<string> LibraryFolders()
        {
            List<string> folders = new List<string>();

            string steamFolder = SteamFolder();
            folders.Add(steamFolder);

            string configFile = steamFolder + "\\config\\config.vdf";

            Regex regex = new Regex("BaseInstallFolder[^\"]*\"\\s*\"([^\"]*)\"");
            using (StreamReader reader = new StreamReader(configFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        folders.Add(Regex.Unescape(match.Groups[1].Value));
                    }
                }
            }

            return folders;
        }

        public static string FR_Folder()
        {
            string result = "";

            var appFolders = LibraryFolders().Select(x => x + "\\SteamApps\\common");
            foreach (var folder in appFolders)
            {
                try
                {
                    var matches = Directory.GetDirectories(folder, "FIREFIGHT RELOADED");
                    if (matches.Length >= 1)
                    {
                        result = matches[0];
                        break;
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    continue;
                }

            }

            // Couldn't find folder, attempt another method
            return result;
        }
    }
}
