using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Broth.Engine.IO
{
    public class ScriptDocument
    {
        public string JString { get; private set; } = "";

        public ScriptDocument(string absoluteFilepath)
        {
            using StreamReader reader = new StreamReader(absoluteFilepath);
            JString = reader.ReadToEnd();
        }
    }
}
