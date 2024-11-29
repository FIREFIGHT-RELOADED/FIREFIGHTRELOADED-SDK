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

        public static object? DataTypeForString(string data)
        {
            object? res = null;

            switch (data)
            {
                case string a when a.Contains("Boolean", StringComparison.CurrentCultureIgnoreCase):
                case string c when c.Contains("Integer", StringComparison.CurrentCultureIgnoreCase):
                case string j when j.Contains("Bool", StringComparison.CurrentCultureIgnoreCase):
                case string k when k.Contains("Int", StringComparison.CurrentCultureIgnoreCase):
                    res = 0;
                    break;
                case string d when d.Contains("Collection", StringComparison.CurrentCultureIgnoreCase):
                case string m when m.Contains("List", StringComparison.CurrentCultureIgnoreCase):
                case string n when n.Contains("Array", StringComparison.CurrentCultureIgnoreCase):
                    res = "[Collection]";
                    break;
                case string f when f.Contains("Double", StringComparison.CurrentCultureIgnoreCase):
                    res = 0.00d;
                    break;
                case string g when g.Contains("Float", StringComparison.CurrentCultureIgnoreCase):
                    res = 0.0f;
                    break;
                case string b when b.Contains("String", StringComparison.CurrentCultureIgnoreCase):
                    res = "Hello World!";
                    break;
                case string h when h.Contains("Color", StringComparison.CurrentCultureIgnoreCase):
                    res = "255 255 255 255";
                    break;
                case string i when i.Contains("Vector", StringComparison.CurrentCultureIgnoreCase):
                    res = "0 0 0";
                    break;
                case string l when l.Contains("FormattedStr", StringComparison.CurrentCultureIgnoreCase):
                    {
                        string format = data.Split()[1];
                        res = format;
                    }
                    break;
                default:
                    break;
            }

            return res;
        }
    }
}
