using Broth.Engine.Util;
using SFML.Window;

namespace Broth.Engine.Input
{
    /// <summary>
    /// A container class for an array of booleans that represent the state of each key on the keyboard at a moment in time.
    /// </summary>
    public class KeyboardState
    {
        private readonly bool[] _keys = new bool[(int)Keyboard.Key.KeyCount];

        /// <summary>
        /// Create a new frame-independent KeyboardState for this exact moment.
        /// Any game logic should use InputHandler.GetKeyboardState instead,
        /// for consistency within the frame and to use less resources.
        /// </summary>
        public static KeyboardState GetCurrentKeyboardState()
        {
            KeyboardState currentState = new KeyboardState();
            for (int i = 0; i < currentState._keys.Length; i++)
            {
                currentState._keys[i] = Keyboard.IsKeyPressed((Keyboard.Key)i);
            }
            return currentState;
        }

        /// <summary>
        /// Get the state a key was in when the KeyboardState was created.
        /// </summary>
        /// <returns> True if the key is down </returns>
        public bool GetKey(Keyboard.Key key)
        {
            if (!IsKeyValid(key))
                return false;
            return _keys[(int)key];
        }

        /// <summary>
        /// Check if a key is one of the keys recorded by the KeyboardState
        /// </summary>
        /// <returns> True if the key is recorded. </returns>
        private bool IsKeyValid(Keyboard.Key key)
        {
            return (int)key >= 0 && (int)key < _keys.Length;
        }
    }
}
