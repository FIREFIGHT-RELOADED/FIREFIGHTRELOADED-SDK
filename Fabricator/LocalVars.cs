using System.Diagnostics;

namespace Fabricator
{
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
        public static FabType SelectedType { get; set; }
        public static string DataPath = Path.Combine(AppContext.BaseDirectory, "data");
    }
}
