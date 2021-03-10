using System;
using System.Collections.Generic;
using System.Text;

namespace Broth.Engine.Util
{
    class Debug
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public static void Warning(string message)
        {
            Console.WriteLine("WARNING: " + message);
        }
    }
}
