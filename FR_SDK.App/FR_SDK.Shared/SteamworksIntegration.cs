#if STEAM
using Steamworks;
using System;
public static class SteamworksIntegration
{
    public static int SteamRelaunchDelayMiliseconds = 500;
    public static AppId sdkAppID = 494770;
    public static AppId gameAppID = 397680;

    #region Functions
    public static void InitSteam(AppId id)
    {
        try
        {
            if (!SteamClient.IsValid)
            {
                SteamClient.Init(id, true);
            }
        }
        catch (Exception)
        {
        }
    }

    public static void ShutdownSteam()
    {
        try
        {
            if (SteamClient.IsValid)
            {
                SteamClient.Shutdown();
            }
        }
        catch (Exception)
        {
        }
    }
    #endregion
}
#endif