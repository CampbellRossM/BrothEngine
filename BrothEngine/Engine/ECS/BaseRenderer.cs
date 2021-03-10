using SFML.Graphics;

using Broth.Engine.Util;

namespace Broth.Engine.ECS
{
    public abstract class BaseRenderer
    {
        public Entity Parent { get; private set; }

        public BaseRenderer(Entity parent) {
            Parent = parent;
        }

        public abstract void Draw(RenderTarget target, Game game, GameTime gameTime);
    }
}
