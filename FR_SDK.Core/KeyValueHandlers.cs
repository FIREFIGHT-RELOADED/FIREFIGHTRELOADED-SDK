using ValveKeyValue;

namespace FR_SDK.Core
{
    //These are generator functions used for creating new KeyValues.
    public class KeyValueCreators
    {
        public static KVObject GenerateGameConfig()
        {
            KVObject kv = new KVObject("Configs", 
            [
                new KVObject("Games", 
                [
                        new KVObject("FIREFIGHT RELOADED", 
                        [
                                new KVObject("GameDir", GlobalVars.moddir),
                                new KVObject("Hammer", 
                                [
                                        new KVObject("GameData0", GlobalVars.fgd),
                                        new KVObject("DefaultTextureScale", GlobalVars.DefaultTextureScale),
                                        new KVObject("DefaultLightmapScale", GlobalVars.DefaultLightmapScale),
                                        new KVObject("GameExe", GlobalVars.gameexe),
                                        new KVObject("DefaultSolidEntity", GlobalVars.DefaultSolidEntity),
                                        new KVObject("DefaultPointEntity", GlobalVars.DefaultPointEntity),
                                        new KVObject("BSP", GlobalVars.vbsp),
                                        new KVObject("Vis", GlobalVars.vvis),
                                        new KVObject("Light", GlobalVars.vrad),
                                        new KVObject("GameExeDir", GlobalVars.gamedir),
                                        new KVObject("MapDir", GlobalVars.mapsrcdir),
                                        new KVObject("BSPDir", GlobalVars.mapdir),
                                        new KVObject("CordonTexture", GlobalVars.CordonTexture),
                                        new KVObject("MaterialExcludeCount", GlobalVars.MaterialExcludeCount)
                                ])
                        ])
                ]),
                new KVObject("SDKVersion", GlobalVars.SDKVersion)
            ]);

            return kv;
        }
    }
}
