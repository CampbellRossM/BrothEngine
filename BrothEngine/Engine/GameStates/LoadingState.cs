using System.Collections.Generic;

using SFML.Graphics;
using SFML.System;

using Broth.Engine.ECS;
using Broth.Engine.IO;
using Broth.Engine.GameData.Resources.GraphicsResources;
using Broth.Engine.GUI.Widgets;
using Broth.Engine.Rendering;
using Broth.Engine.Util;

namespace Broth.Engine.GameStates
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

            game.Resources.RegisterResource(
                game.Resources.ResourceDeserializer.Deserialize(
                    new ScriptDocument(FileFinder.ResourcePath("BrothEngine/resource.json")).JString
                    ));

            ImageRenderer renderer = new ImageRenderer(entity)
            {
                Resource = game.Resources.TryGetResource<ImageResource>("ENGINE::logo")
            };
            entity.Renderer = renderer;
            entity.Transform.Position = new Vector2f(game.Window.Size.X / 2, game.Window.Size.Y / 2);
        }

        public override void Update(Game game, GameTime gameTime)
        {
            progress += gameTime.DeltaTime.AsSeconds() * 10;
            entity.Transform.Position = new Vector2f
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
