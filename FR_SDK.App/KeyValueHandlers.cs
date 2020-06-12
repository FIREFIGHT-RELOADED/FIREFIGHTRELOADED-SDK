using KVLib;

namespace FR_SDK.App
{
    //These helper methods are used to quickly add child keys to parent keys.
    class KeyValueHelpers
    {
        public static KeyValue addKey(string KeyName, int keyValue)
        {
            KeyValue key = new KeyValue(KeyName);
            key.Set(keyValue);
            return key;
        }

        public static KeyValue addKey(string KeyName, float keyValue)
        {
            KeyValue key = new KeyValue(KeyName);
            key.Set(keyValue);
            return key;
        }

        public static KeyValue addKey(string KeyName, string keyValue)
        {
            KeyValue key = new KeyValue(KeyName);
            key.Set(keyValue);
            return key;
        }
    }

    //These are generator functions used for creating new KeyValues.
    class KeyValueCreators
    {
        public static KeyValue GenerateGameConfig()
        {
            KeyValue kv = new KeyValue("Configs");

            KeyValue games = new KeyValue("Games");

            KeyValue currgame = new KeyValue("FIREFIGHT RELOADED");
            currgame.AddChild(KeyValueHelpers.addKey("GameDir", GlobalVars.moddir));

            KeyValue hammer = new KeyValue("Hammer");
            hammer.AddChild(KeyValueHelpers.addKey("GameData0", GlobalVars.fgd));
            hammer.AddChild(KeyValueHelpers.addKey("DefaultTextureScale", GlobalVars.DefaultTextureScale));
            hammer.AddChild(KeyValueHelpers.addKey("DefaultLightmapScale", GlobalVars.DefaultLightmapScale));
            hammer.AddChild(KeyValueHelpers.addKey("GameExe", GlobalVars.gameexe));
            hammer.AddChild(KeyValueHelpers.addKey("DefaultSolidEntity", GlobalVars.DefaultSolidEntity));
            hammer.AddChild(KeyValueHelpers.addKey("DefaultPointEntity", GlobalVars.DefaultPointEntity));
            hammer.AddChild(KeyValueHelpers.addKey("BSP", GlobalVars.vbsp));
            hammer.AddChild(KeyValueHelpers.addKey("Vis", GlobalVars.vvis));
            hammer.AddChild(KeyValueHelpers.addKey("Light", GlobalVars.vrad));
            hammer.AddChild(KeyValueHelpers.addKey("GameExeDir", GlobalVars.gamedir));
            hammer.AddChild(KeyValueHelpers.addKey("MapDir", GlobalVars.mapsrcdir));
            hammer.AddChild(KeyValueHelpers.addKey("BSPDir", GlobalVars.mapdir));
            hammer.AddChild(KeyValueHelpers.addKey("CordonTexture", GlobalVars.CordonTexture));
            hammer.AddChild(KeyValueHelpers.addKey("MaterialExcludeCount", GlobalVars.MaterialExcludeCount));

            currgame.AddChild(hammer);

            games.AddChild(currgame);

            kv.AddChild(games);

            kv.AddChild(KeyValueHelpers.addKey("SDKVersion", GlobalVars.SDKVersion));

            return kv;
        }
    }
}
