using System;
using System.Collections.Generic;

using SFML.Window;

namespace Broth.Input
{
    public static class InputHandler
    {
        private const bool REALEASE_KEYS_WHEN_LOST_FOCUS = true;

        private static readonly List<KeyBinding> _keyBindings = new List<KeyBinding>();

        private static KeyboardState
            _oldKeyboardState = KeyboardState.GetCurrentKeyboardState(),
            _currentKeyboardState = KeyboardState.GetCurrentKeyboardState();

        /// <summary> Get the state the keyboard was in at the beginning of the frame. </summary>
        public static KeyboardState GetKeyboardState { get { return _currentKeyboardState; } }

        public static void AddKeyBinding(KeyBinding binding)
        {
            _keyBindings.Add(binding);
        }

        /// <summary> Called by the engine once per frame. </summary>
        public static void Update(Game game)
        {
            _oldKeyboardState = _currentKeyboardState;
            if (REALEASE_KEYS_WHEN_LOST_FOCUS && !game.Window.HasFocus())
                _currentKeyboardState = new KeyboardState();
            else
                _currentKeyboardState = KeyboardState.GetCurrentKeyboardState();
        }

        /// <summary> Check if a key is currently down. </summary>
        public static bool IsDown(Keyboard.Key key)
        {
            return _currentKeyboardState.GetKey(key);
        }

        /// <summary> Check if a key was pressed on this frame. </summary>
        public static bool IsPressed(Keyboard.Key key)
        {
            return _currentKeyboardState.GetKey(key) && ! _oldKeyboardState.GetKey(key);
        }

        /// <summary> Check if a key was released on this frame. </summary>
        public static bool IsReleased(Keyboard.Key key)
        {
            return _oldKeyboardState.GetKey(key) && !_currentKeyboardState.GetKey(key);
        }

        /// <summary> Get the name of the first binding associated with a key. </summary>
        public static string GetBindingFromKey(Keyboard.Key key)
        {
            foreach (KeyBinding keyBinding in _keyBindings)
            {
                if (keyBinding.IsKeyBound(key))
                    return keyBinding.Name;
            }
            return "";
        }

        /// <summary>
        /// Check if any of the keys associated with the name of a binding return true for a given function.
        /// </summary>
        /// <param name="func"> A function that takes a Key as a parameter and returns a boolean. Example: InputHandler.IsDown </param>
        public static bool CheckBinding(string name, Func<Keyboard.Key, bool> func)
        {
            foreach (KeyBinding keyBinding in _keyBindings)
            {
                if (keyBinding.Name == name && func(keyBinding.BoundKey))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Register the bindings that are required by the game engine.
        /// </summary>
        internal static void RegisterCommonBindings()
        {
            AddKeyBinding(new KeyBinding("Exit", Keyboard.Key.Escape));
            AddKeyBinding(new KeyBinding("Debug", Keyboard.Key.F3));
            AddKeyBinding(new KeyBinding("Fullscreen", Keyboard.Key.F11));
        }

        /// <summary>
        /// Check if any of the common bindings have been pressed and perform them accordingly
        /// </summary>
        internal static void PerformCommonBindingActions(Game game)
        {
            if (CheckBinding("Fullscreen", IsPressed))
            {
                game.Window.ToggleFullscreen(game);
            }

            if (CheckBinding("Exit", IsPressed))
            {
                game.Escape();
            }
        }
    }
}
