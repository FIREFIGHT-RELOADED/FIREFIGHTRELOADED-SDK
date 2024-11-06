using System.Globalization;
using ValveKeyValue;

namespace Fabricator
{
    public class Playlist : FileCreatorBase
    {
        public class PlaylistNode : BaseNode
        {
            public string path { get; set; } = "";
            public string title { get; set; } = "";
            public string artist { get; set; } = "";
            public string album { get; set; } = "";
        }

        public override bool SetLabelToFileName { get; set; } = true;
        public override bool FileUsesSettings { get; set; } = true;

        public Playlist(string filePath) : base(filePath)
        {
        }

        public override KVObject NodeToKVObject(BaseNode node, int index = -1)
        {
            PlaylistNode classNode = node as PlaylistNode;

            if (classNode != null)
            {
                AddKVObjectEntryStat("path", classNode.path);
                AddKVObjectEntryStat("title", classNode.title);
                AddKVObjectEntryStat("artist", classNode.artist);
                AddKVObjectEntryStat("album", classNode.album);
            }

            return base.NodeToKVObject(node, index);
        }

        public override PlaylistNode EntryToNode(int index)
        {
            int actualIndex = index - 1;

            PlaylistNode classNode = new PlaylistNode();

            if (entries[actualIndex] != null)
            {
                KVObject obj = entries[actualIndex];

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
