using System.Text.Json.Serialization;

using SFML.System;
using SFML.Graphics;

using Broth.ECS;
using Broth.IO;

namespace Broth.GameData.Resources.GraphicsResources
{
    /// <summary>
    /// An image that will be displayed as a whole. Not cropped or subdivided.
    /// </summary>
    public class ImageResource : GraphicsResource
    {
        // ---------
        // Properties set with JSON
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OriginPosition Origin { get; set; } = OriginPosition.CUSTOM;
        public Vector2f CustomOrigin { get; set; } = new Vector2f(0, 0);
        public string Filepath { get; set; } = "";
        // ---------

        public Sprite Image { get; set; } = null;

        private Texture texture = null;

        public override void Dispose()
        {
            if (texture == null) return;

            texture.Dispose();
            texture = null;
        }

        public override bool IsLoaded()
        {
            return Image != null && Image.Texture != null;
        }

        public override void Load()
        {
            texture = LoadTexture(FileFinder.ResourcePath(Filepath));
            Image = new Sprite(texture);
            RefreshOrigin(Image, Origin, CustomOrigin);
        }

        public void Draw(RenderTarget renderTarget, Transform2D transform, Color color)
        {
            LazyLoad();
            if (IsLoaded() == false)
                return;

            Image.Position = transform.Position;
            Image.Rotation = transform.Rotation;
            Image.Scale = transform.Scale;
            Image.Color = color;

            renderTarget.Draw(Image);
        }
    }
}
