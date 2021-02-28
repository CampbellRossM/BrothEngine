using SFML.Window;

namespace Broth.Input
{
    /// <summary>
    /// Key bindings allow controls to be easily remapped to different keys.
    /// </summary>
    public class KeyBinding
    {
        public string Name { get; private set; } = "Unnamed";
        public Keyboard.Key BoundKey { get; set; } = Keyboard.Key.Unknown;

        public KeyBinding(string name, Keyboard.Key key)
        {
            Name = name;
            BoundKey = key;
        }

        public bool IsKeyBound(Keyboard.Key key)
        {
            return key == BoundKey;
        }
    }
}
