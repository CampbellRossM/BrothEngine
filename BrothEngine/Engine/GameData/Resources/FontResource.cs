using SFML.Graphics;
using Broth.Engine.IO;

namespace Broth.Engine.GameData.Resources
{
    class FontResource : Resource
    {
        // ---JSON Properties---
        public string Filepath { get; set; }
        // ---------------------

        public Font Font { get; private set; }

        public override void Dispose()
        {
            if (Font == null) return;

            Font.Dispose();
            Font = null;
        }

        public override bool IsLoaded()
        {
            return Font != null;
        }

        public override void Load()
        {
            Font = new Font(FileFinder.ResourcePath(Filepath));
        }
    }
}
