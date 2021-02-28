using SFML.Graphics;
using SFML.System;

namespace Broth.ECS
{
    public class Transform2D
    {
        // Fields
        private Vector2f _position = new Vector2f(0, 0);
        private Vector2f _scale = new Vector2f(1, 1);

        // Properties
        public Entity Parent { get; private set; }

        public Vector2f Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (value == null)
                    value = new Vector2f(0, 0);
                _position = value;
            }
        }

        public float Rotation { get; set; } = 0;

        public Vector2f Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                if (value == null)
                    value = new Vector2f(1, 1);
                _scale = value;
            }
        }

        public Transform2D(Entity parent)
        {
            Parent = parent;
        }
    }
}
