using SFML.Graphics;

using Broth.Engine.ECS;
using Broth.Engine.GameData.Resources.GraphicsResources;
using Broth.Engine.Util;

namespace Broth.Engine.Rendering
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
