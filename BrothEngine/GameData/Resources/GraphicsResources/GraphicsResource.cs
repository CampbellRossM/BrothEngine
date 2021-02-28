using System;
using System.Collections.Generic;
using System.Text.Json;

using SFML.Graphics;
using SFML.System;

using Broth.Util;

namespace Broth.GameData.Resources.GraphicsResources
{
    /// <summary>
    /// Base class for all graphics-specific resources. Contains relevant functions.
    /// </summary>
    public abstract class GraphicsResource : Resource
    {
        public enum OriginPosition
        {
            TOP_LEFT = 0,
            TOP_CENTER = 1,
            TOP_RIGHT = 2,
            CENTER_LEFT = 3,
            CENTER = 4,
            CENTER_RIGHT = 5,
            BOTTOM_LEFT = 6,
            BOTTOM_CENTER = 7,
            BOTTOM_RIGHT = 8,
            CUSTOM = -1
        }

        protected Texture LoadTexture(string filepath)
        {
            Texture texture = null;
            try
            {
                if (filepath.Length < 1)
                {
                    Debug.Log("Image " + ID + " attempted to load with no filepath.");
                    throw new Exception("No filepath to load");
                }
                texture = new Texture(filepath);
            }
            catch (Exception)
            {
                Debug.Warning("Image " + ID + " failed to load " + filepath);
                // TODO: Return a "Texture missing" graphic
            }

            return texture;
        }

        public OriginPosition ParseOrigin(string originString)
        {
            if (Enum.TryParse(originString, true, out OriginPosition origin))
                return origin;

            return OriginPosition.CUSTOM;
        }

        public void RefreshOrigin(Sprite sprite, OriginPosition origin, Vector2f customOrigin)
        {
            if (origin < 0)
            {
                sprite.Origin = customOrigin;
                return;
            }

            float x = (int)origin % 3 / 2f;
            float y = (int)origin / 3 / 2f;

            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Width * x, sprite.GetLocalBounds().Height * y);
        }
    }
}
