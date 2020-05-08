using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser.Structure
{
    internal sealed class JsonType
    {
        public string TypeName { get; }
        public IReadOnlyCollection<JsonField> Fields { get; }

        public JsonType(string typeName, IEnumerable<JsonField> fields)
        {
            TypeName = typeName;
            Fields = new List<JsonField>(fields);
        }
    }
}
