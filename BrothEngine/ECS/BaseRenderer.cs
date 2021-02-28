using SFML.Graphics;

using Broth.Util;

namespace Broth.ECS
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
