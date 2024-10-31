using System.Threading.Tasks;

namespace FR_SDK.Core
{
    public class ProcessControllerSteam : ProcessController
    {
        public bool AppOverridesSteam = false;
        public bool ShuttingDown = false;

        public ProcessControllerSteam() : base()
        {
        }

        public override void ProcessExitedExt()
        {
            FixSteam();
        }

        public override void KillAllActiveProcesses()
        {
            base.KillAllActiveProcesses();
            FixSteam();
        }

        private void FixSteam()
        {
            if (AppOverridesSteam && !ShuttingDown)
            {
                Task.Delay(SteamworksIntegration.SteamRelaunchDelayMiliseconds);
                SteamworksIntegration.InitSteam(SteamworksIntegration.sdkAppID);
                AppOverridesSteam = false;
            }
        }
    }
}
