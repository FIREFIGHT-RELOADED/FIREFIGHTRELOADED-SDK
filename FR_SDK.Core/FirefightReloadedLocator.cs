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
            string log = "";

            var appFolders = LibraryFolders().Select(x => x + "\\SteamApps\\common");
            foreach (var folder in appFolders)
            {
                try
                {
                    log += $"[LOG] Searching \"{folder}\".\n";
                    var matches = Directory.GetDirectories(folder, "FIREFIGHT RELOADED");
                    if (matches.Length >= 1)
                    {
                        result = matches[0];
                        log += $"[LOG] Found directory \"{result}\"!\n";
                        break;
                    }
                    else
                    {
                        log += $"[LOG] Directory \"{folder}\" is invalid. Continuing.\n";
                        continue;
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    log += $"[LOG] Directory \"{folder}\" is invalid. Continuing.\n";
                    continue;
                }
            }

            string BasePath = AppContext.BaseDirectory;

            // Couldn't find folder, attempt another method
            if (string.IsNullOrWhiteSpace(result))
            {
                log += $"[LOG] Couldn't find folder through registry/steam config. Trying to find it using current directory.\n";

                string dir = BasePath.Replace("\\sdk\\", "");
                if (dir.Contains("FIREFIGHT RELOADED"))
                {
                    result = dir;
                    log += $"[LOG] Launching directly from \"{result}\"!\n";
                }
            }
            else
            {
                log += $"[LOG] Found the game folder through the registry/steam config.\n";
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                log += $"[LOG] Unable to find FIREFIGHT RELOADED directory.\n";
            }

            string logfile = BasePath + "\\log.txt";

            if (File.Exists(logfile))
            {
                File.Delete(logfile);
            }

            File.WriteAllText(logfile, log);

            return result;
        }
    }
}
