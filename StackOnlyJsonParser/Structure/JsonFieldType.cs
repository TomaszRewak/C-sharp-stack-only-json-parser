using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser.Structure
{
    internal sealed class JsonFieldType
    {
        public string Name { get; set; }
        public List<JsonCollectionKind> ConnectionKind { get; set; } = new List<JsonCollectionKind>();

        public JsonFieldType() { }
        public JsonFieldType(string type)
        {
            while (true)
            {
                if (type.EndsWith("[]"))
                {
                    ConnectionKind.Insert(0, JsonCollectionKind.Array);
                    type = type[0..^2];
                    continue;
                }

                if (type.EndsWith("{}"))
                {
                    ConnectionKind.Insert(0, JsonCollectionKind.Dictionary);
                    type = type[0..^2];
                    continue;
                }

                break;
            }
        }
    }
}
