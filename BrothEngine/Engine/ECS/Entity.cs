using System.Collections.Generic;

using SFML.Graphics;

using Broth.Engine.Util;

namespace Broth.Engine.ECS
{
    public class Entity
    {
        private readonly List<Component> components = new List<Component>();

        public Transform2D Transform { get; private set; }
        public BaseRenderer Renderer { get; set; }

        public Entity()
        {
            Transform = new Transform2D(this);
        }

        public void Update(Game game, GameTime gameTime)
        {
            foreach (Component component in components)
            {
                component.Update(game, gameTime);
            }
        }

        public void Draw(RenderTarget target, Game game, GameTime gameTime)
        {
            if (Renderer != null)
                Renderer.Draw(target, game, gameTime);
        }
    }
}
