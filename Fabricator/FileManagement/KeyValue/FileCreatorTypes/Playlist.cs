using System.Globalization;
using ValveKeyValue;

namespace Fabricator
{
    public class Playlist : FileCreatorBase
    {
        public class PlaylistNode : BaseNode
        {
            public string path { get; set; } = "hl1/music/Half-Life13.mp3";
            public string title { get; set; } = "Hazardous Environments";
            public string artist { get; set; } = "Kelly Bailey";
            public string album { get; set; } = "HalfLife2";
        }

        public override bool SetLabelToFileName { get; set; } = true;
        public override bool FileUsesSettings { get; set; } = true;

        public Playlist() : base()
        {
        }

        public Playlist(string filePath) : base(filePath)
        {
        }

        public override KVObject NodeToKVObject(BaseNode node, int index = -1)
        {
            PlaylistNode classNode = node as PlaylistNode;

            if (classNode != null)
            {
                //load every entry into the entrystats list and convert them to KVObjects.
                AddKVObjectEntryStat("path", classNode.path);
                AddKVObjectEntryStat("title", classNode.title);
                AddKVObjectEntryStat("artist", classNode.artist);
                AddKVObjectEntryStat("album", classNode.album);
            }

            return base.NodeToKVObject(node, index);
        }

        public override PlaylistNode KVObjectToNode(int index)
        {
            int actualIndex = index - 1;

            PlaylistNode classNode = new PlaylistNode();

            if (entries[actualIndex] != null)
            {
                KVObject obj = entries[actualIndex];

                //go through the KVObject's children and fill in the entries of each node.
                foreach (KVObject child in obj.Children)
                {
                    switch (child.Name)
                    {
                        case "path":
                            classNode.path = child.Value.ToString(CultureInfo.CurrentCulture);
                            break;
                        case "title":
                            classNode.title = child.Value.ToString(CultureInfo.CurrentCulture);
                            break;
                        case "artist":
                            classNode.artist = child.Value.ToString(CultureInfo.CurrentCulture);
                            break;
                        case "album":
                            classNode.album = child.Value.ToString(CultureInfo.CurrentCulture);
                            break;
                        default:
                            break;
                    }
                }
            }

            return classNode;
        }
    }
}
