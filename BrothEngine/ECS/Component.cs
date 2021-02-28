using Broth.Util;

namespace Broth.ECS
{
    abstract class Component
    {
        public Entity Parent { get; private set; }

        public Component(Entity parent)
        {
            Parent = parent;
        }

        public abstract void Update(Game game, GameTime gameTime);
    }
}
