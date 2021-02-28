using SFML.Graphics;

using Broth.ECS;
using Broth.Util;
using Broth.GameData.Resources.GraphicsResources;

namespace Broth.Rendering
{
    class ImageRenderer : BaseRenderer
    {
        public Color Color { get; set; } = Color.White;

        public ImageResource Resource { get; set; }

        public ImageRenderer(Entity parent) : base(parent)
        {
            Color = new Color(
                (byte)BrothMath.Random.Next(0, 256),
                (byte)BrothMath.Random.Next(0, 256),
                (byte)BrothMath.Random.Next(0, 256)
                );
        }

        public override void Draw(RenderTarget target, Game game, GameTime gameTime)
        {
            if (Resource != null)
                Resource.Draw(target, Parent.Transform, Color);
        }
    }
}
