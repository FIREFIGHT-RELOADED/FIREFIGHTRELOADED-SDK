using System.Diagnostics;

namespace Fabricator
{
    // The various types of scripts to load.
    public enum FabType
    {
        Other,
        Spawnlist,
        ShopCatalog,
        RewardList,
        Playlist,
        MapAdd,
        Loadout
    }

    public class LocalVars
    {
        // The selected script type.
        public static FabType SelectedType { get; set; }
        // The path to the data folder relative to the application.
        public static string DataPath = Path.Combine(AppContext.BaseDirectory, "data");
    }
}
