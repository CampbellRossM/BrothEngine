using System;

using SFML.System;
using SFML.Graphics;

using Broth.Util;

namespace Broth.GUI.Widgets
{
    public class BarWidget
    {
        private FloatRect _bounds = new FloatRect();

        private readonly RectangleShape backBar = new RectangleShape(new Vector2f(100, 10));
        private readonly RectangleShape fillBar = new RectangleShape(new Vector2f(0, 10));

        /// <summary>
        /// If true, read bounds as a ratio of the render target (.
        /// If false, read bounds as world coordinates.
        /// </summary>
        public bool BoundsAsRatio = false;

        public Color BackgroundColor { set { EmptyBackgroundColor = value; FullBackgroundColor = value; } }
        public Color EmptyBackgroundColor { get; set; } = Color.Black;
        public Color FullBackgroundColor { get; set; } = Color.Black;

        public Color FillColor { set { EmptyFillColor = value; FullFillColor = value; } }
        public Color EmptyFillColor { get; set; } = Color.White;
        public Color FullFillColor { get; set; } = Color.White;

        public Color BorderColor { get { return backBar.OutlineColor; } set { backBar.OutlineColor = value; } }
        public float BorderSize { get { return backBar.OutlineThickness; } set { backBar.OutlineThickness = value; } }


        public FloatRect Bounds {
            get
            {
                return _bounds;
            }
            set
            {
                _bounds = value;
                if (BoundsAsRatio == false)
                {
                    backBar.Position = new Vector2f(Bounds.Left, Bounds.Top);
                    backBar.Size = new Vector2f(Bounds.Width, Bounds.Height);
                    fillBar.Position = backBar.Position;
                }
            }
        }

        public void Draw(RenderTarget target, float percentFilled)
        {
            percentFilled = Math.Clamp(percentFilled, 0, 1);

            backBar.FillColor = BrothMath.Lerp(percentFilled, EmptyBackgroundColor, FullBackgroundColor);
            fillBar.FillColor = BrothMath.Lerp(percentFilled, EmptyFillColor, FullFillColor);

            if (BoundsAsRatio == true)
            {
                backBar.Position = new Vector2f
                    (
                        Bounds.Left * target.Size.X,
                        Bounds.Top * target.Size.Y
                    );
                backBar.Size = new Vector2f
                    (
                        Bounds.Width * target.Size.X,
                        Bounds.Height * target.Size.Y
                    );
                fillBar.Position = backBar.Position;
            }

            fillBar.Size = new Vector2f
                (
                    backBar.Size.X * percentFilled,
                    backBar.Size.Y
                );

            target.Draw(backBar);
            target.Draw(fillBar);
        }
    }
}
