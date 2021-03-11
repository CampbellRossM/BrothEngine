using System;
using System.Collections.Generic;

using Broth.Engine.GameData.Resources;
using Broth.Engine.GameData.Settings;
using Broth.Engine.GameStates;
using Broth.Engine.Input;
using Broth.Engine.IO;
using Broth.Engine.Rendering;
using Broth.Engine.Util;

namespace Broth.Engine
{
    public class Game
    {
        private bool isRunning = false;
        private readonly List<Action> actions = new List<Action>();
        private GameState state;

        public GameWindow Window { get; set; }
        public SettingsManager Settings { get; } = new SettingsManager();
        public ResourceManager Resources { get; } = new ResourceManager();

        public GameState InitialGameState { get; set; } = new LoadingState();
        public string Title { get; set; } = "BrothEngine Game";

        public Game(string resourcesFolder = "Resources")
        {
            FileFinder.LocateResources(resourcesFolder);
        }

        //===============================
        // Game Logic
        //===============================

        private void Initialize()
        {
            Settings.RegisterCommonSettings();
            InputHandler.RegisterCommonBindings();

            Window = GameWindow.CreateGameWindow(this);

            state = InitialGameState;
            state.Initialize(this);
        }

        private void PerformActions()
        {
            // Since we can add to the actions list while iterating through it, we can't use a foreach iteration
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i]();
            }
            actions.Clear();
        }

        private void Update(GameTime gameTime)
        {
            state.Update(this, gameTime);
        }

        private void Draw(GameTime gameTime)
        {
            Window.Clear();

            state.Draw(this, gameTime);

            Window.Display();
        }

        private void End()
        {
            Window.Dispose();
            // TODO: Save settings
        }

        private void GameLoop()
        {
            // TODO: TargetFPS and fixed time step are currently used interchangeably.

            GameTime gameTime = new GameTime((uint)Settings.GetSetting("ENGINE.TargetFPS").GetInt());

            while(Window.IsOpen)
            {
                gameTime.NextFrame();
                Window.DispatchEvents();
                InputHandler.Update(this);
                InputHandler.PerformCommonBindingActions(this);

                PerformActions();
                if (!Window.IsOpen)
                    return;

                Update(gameTime);
                Draw(gameTime);
                // Draw method is managing framerate when it calls window.Display(), as set in Initialize()
            }
        }

        /// <summary> Called to begin running the game. Subsequent calls do nothing. </summary>
        public void Run()
        {
            if (isRunning)
                return;
            isRunning = true;

            Initialize();

            GameLoop();

            End();
        }

        //======================================
        //  Actions
        //======================================

        /// <summary> Delegate a function to be performed before the next Update in the game loop </summary>
        public void QueueAction(Action action) { actions.Add(action); }

        /// <summary> Queues an action to change the game state </summary>
        public void ChangeState(GameState state)
        {
            void action()
            {
                this.state = state;

                // TODO: Change resource context

                state.Initialize(this);
            }

            QueueAction(action);
        }

        /// <summary> Queues an action to forward the escape call to the active state to decide how it should be handled. </summary>
        public void Escape()
        {
            QueueAction(() => { state.OnEscape(this); });
        }

        /// <summary> Queues an action to begin closing procedures and terminate the game. </summary>
        public void Exit()
        {
            // TODO: This can cause a crash if any other actions try to interact with the window after it's executed.
            QueueAction(Window.Close);
        }
    }
}
