using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace FR_SDK.Core
{
    public class ProcessController
    {
        public static List<Process> ProcessList;

        public ProcessController()
        {
            ProcessList = new List<Process>();
        }

        public Process LaunchApp(string exePath, string exeArgs, string formAppName = "")
        {
            try
            {
                Process pr = new Process();
                pr.StartInfo.FileName = exePath;
                pr.StartInfo.Arguments = exeArgs;
                pr.Exited += ProcessExited;
                ProcessList.Add(pr);
                return pr;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occurred when launching the application: " + ex.Message);
            }

            return null;
        }

        public virtual void ProcessExitedExt() { }

        private void ProcessExited(object sender, EventArgs e)
        {
            try
            {
                Process proc = (Process)sender;

                if (proc != null)
                {
                    ProcessList.Remove(proc);
                }
            }
            catch (Exception)
            {

            }

            ProcessExitedExt();
        }

        [SupportedOSPlatform("windows")]
        //https://stackoverflow.com/questions/30249873/process-kill-doesnt-seem-to-kill-the-process
        private static void KillProcessAndChildrens(int pid)
        {
            ManagementObjectSearcher processSearcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection processCollection = processSearcher.Get();

            // We must kill child processes first!
            if (processCollection != null)
            {
                foreach (ManagementObject mo in processCollection)
                {
                    KillProcessAndChildrens(Convert.ToInt32(mo["ProcessID"])); //kill child processes(also kills childrens of childrens etc.)
                }
            }

            // Then kill parents.
            try
            {
                Process proc = Process.GetProcessById(pid);
                if (!proc.HasExited) proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }

        [SupportedOSPlatform("windows")]
        public virtual void KillAllActiveProcesses()
        {
            foreach (Process proc in ProcessList)
            {
                KillProcessAndChildrens(proc.Id);

                if (proc != null)
                {
                    ProcessList.Remove(proc);
                }
            }
        }
    }
}
