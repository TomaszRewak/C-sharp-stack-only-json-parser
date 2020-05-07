using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser.Structure
{
    internal sealed class JsonType
    {
        public string TypeName { get; set; }

        public List<JsonField> Fields { get; set; } = new List<JsonField>();
    }
}
