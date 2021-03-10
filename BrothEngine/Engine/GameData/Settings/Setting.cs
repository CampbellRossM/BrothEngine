using System;
using System.Collections.Generic;
using System.Text;

namespace Broth.Engine.GameData.Settings
{
    public abstract class Setting
    {
        public string Tab { get; set; } = "Hidden";
        public string ID { get; set; }
        public string Name { get; set; }

        public abstract bool IsDefault { get; }

        public virtual int GetInt() { throw new InvalidCastException("Setting " + ID + " is not an integer."); }
        public virtual void Set(int value) { throw new InvalidCastException("Setting " + ID + " is not an integer."); }

        public virtual float GetFloat() { throw new InvalidCastException("Setting " + ID + " is not a float."); }
        public virtual void Set(float value) { throw new InvalidCastException("Setting " + ID + " is not a float."); }

        public virtual string GetString() { throw new InvalidCastException("Setting " + ID + " is not a string."); }
        public virtual void Set(string value) { throw new InvalidCastException("Setting " + ID + " is not a string."); }

        public virtual bool GetBool() { throw new InvalidCastException("Setting " + ID + " is not a bool."); }
        public virtual void Set(bool value) { throw new InvalidCastException("Setting " + ID + " is not a bool."); }
    }

    public class IntSetting : Setting
    {
        private int _default = 0;

        public int Default { get { return _default; } set { _default = value; Value = value; } }
        public int Value { get; set; } = 0;

        public override bool IsDefault => Value == Default;

        public override void Set(int value)
        {
            Value = value;
        }

        public override int GetInt()
        {
            return Value;
        }
    }

    public class BoolSetting : Setting
    {
        private bool _default = false;

        public bool Default { get { return _default; } set { _default = value; Value = value; } }
        public bool Value { get; set; } = true;

        public override bool IsDefault => Value == Default;

        public override void Set(bool value)
        {
            Value = value;
        }

        public override bool GetBool()
        {
            return Value;
        }
    }


}
