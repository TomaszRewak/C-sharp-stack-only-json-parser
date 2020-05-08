using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser.Structure
{
    internal sealed class JsonField
    {
        public string Name { get; }
        public string Type { get; }

        public JsonField(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}
