using System.Collections.Generic;

using SFML.Graphics;
using SFML.System;

using Broth.ECS;
using Broth.GUI.Widgets;
using Broth.Rendering;
using Broth.Util;
using Broth.IO;
using Broth.GameData.Resources.GraphicsResources;

namespace Broth.GameStates
{
    class LoadingState : GameState
    {
        private float progress = 0;

        private BarWidget progressBar;
        private Entity entity;

        public override void OnEscape(Game game)
        {
            game.Exit();
        }

        public override void Initialize(Game game)
        {
            progressBar = new BarWidget()
            {
                BoundsAsRatio = true,
                Bounds = new FloatRect(.25f, .45f, .5f, .1f),
                BorderColor = Color.White,
                BorderSize = 3,
                EmptyBackgroundColor = Color.Red,
                FullBackgroundColor = Color.Black,
                EmptyFillColor = Color.Black,
                FullFillColor = Color.Green
            };

            entity = new Entity();

            ResourceFactory.RegisterResourceFromFile(@"BrothEngine\resource.json", game.Resources);

            ImageRenderer renderer = new ImageRenderer(entity)
            {
                Resource = game.Resources.TryGetResource<ImageResource>("ENGINE::logo")
            };
            entity.Renderer = renderer;
            entity.Transform.Position = new Vector2f(game.Window.Size.X / 2, game.Window.Size.Y / 2);
            /*
            ImageRenderer renderer = new ImageRenderer(entity);
            ImageResource resource = new ImageResource("noID", null);
            resource.Filepath = @"BrothEngine\logo.png";
            resource.Load();
            resource.Image.Origin = new Vector2f(resource.Image.TextureRect.Width / 2, resource.Image.TextureRect.Height / 2);
            renderer.Resource = resource;
            entity.Renderer = renderer;
            */
        }

        public override void Update(Game game, GameTime gameTime)
        {
            progress += gameTime.DeltaTime.AsSeconds() * 10;
            entity.Transform.Position = new SFML.System.Vector2f
                (
                    game.Window.Size.X / 2,
                    game.Window.Size.Y / 4
                );
        }

        public override void Draw(Game game, GameTime gameTime)
        {
            game.Window.Clear(Color.White);
            progressBar.Draw(game.Window, progress / 100);
            entity.Draw(game.Window, game, gameTime);
        }
    }
}
