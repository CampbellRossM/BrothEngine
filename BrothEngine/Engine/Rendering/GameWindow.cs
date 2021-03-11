using System;
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;
using SFML.Window;

using Broth.Engine.GameData.Settings;

namespace Broth.Engine.Rendering
{
    /// <summary>
    /// GameWindow is a RenderWindow that performs its own setup, and includes some additional functions.
    /// Construction of a GameWindow should only be performed through the static method GameWindow.CreateGameWindow
    /// </summary>
    public class GameWindow : RenderWindow
    {
        private GameWindow(VideoMode videoMode, string title, Styles style) : base(videoMode, title, style) { }

        /// <summary>
        /// Tell the window how certain events should be responded to.
        /// This should only be called during the creation of the window.
        /// </summary>
        private void AttachEventDelegates(Game game)
        {
            Closed += (sender, args) =>
            {
                RenderWindow window = (RenderWindow)sender;
                window.Close();
            };
            Resized += (sender, args) =>
            {
                if (game.Settings.GetSetting("ENGINE.Fullscreen").GetBool()) return;

                RenderWindow window = (RenderWindow)sender;
                game.Settings.GetSetting("ENGINE.WindowWidth").Set((int)args.Width);
                game.Settings.GetSetting("ENGINE.WindowHeight").Set((int)args.Height);
                window.SetView(new View(new FloatRect(0, 0, args.Width, args.Height)));
            };

            // KeyPressed += (sender, args) => { };
            // The KeyPressed EventHandler will continue to fire several times while the key is held.
            // It's fine for navigating menus, but not for gameplay.

        }

        /// <summary>
        /// Prepares a new GameWindow, and performs all necessary setup.
        /// </summary>
        /// <returns> A GameWindow ready to be used. </returns>
        public static GameWindow CreateGameWindow(Game game)
        {
            VideoMode videoMode;
            Styles style;

            if (game.Settings.GetSetting("ENGINE.Fullscreen").GetBool())
            {
                videoMode = VideoMode.FullscreenModes[0];
                style = Styles.Fullscreen;
            }
            else
            {
                videoMode = new VideoMode(
                    (uint)game.Settings.GetSetting("ENGINE.WindowWidth").GetInt(),
                    (uint)game.Settings.GetSetting("ENGINE.WindowHeight").GetInt()
                    );
                style = Styles.Default;
            }

            GameWindow window = new GameWindow(videoMode, game.Title, style);

            // WARNING: If the internet finds out you are using a Sleep() based method to set framerate, you will get yelled at
            window.SetFramerateLimit((uint)game.Settings.GetSetting("ENGINE.TargetFPS").GetInt());
            window.AttachEventDelegates(game);
            window.SetActive();

            return window;
        }

        /// <summary>
        /// Queues the window to switch between fullscreen and windowed modes after event polling is finished.
        /// The window is saved as the main game window, and state is stored in config settings.
        /// Only one active window is supported at this time.
        /// </summary>
        public void ToggleFullscreen(Game game)
        {
            game.QueueAction(
                    () =>
                    {
                        game.Settings.GetSetting("ENGINE.Fullscreen").Set(
                            !game.Settings.GetSetting("ENGINE.Fullscreen").GetBool()
                            );
                        game.Window = CreateGameWindow(game);

                        Destroy(false);
                    }
                );
        }
    }
}
