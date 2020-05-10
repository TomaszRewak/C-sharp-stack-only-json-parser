using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace StackOnlyJsonParser.CodeStructure
{
	internal sealed class JsonType
	{
		public string Accesibility { get; }
		public string Namespace { get; }
		public string TypeName { get; }
		public IReadOnlyCollection<JsonField> Fields { get; }

		public JsonType(string accessibility, string @namespace, string typeName, IEnumerable<JsonField> fields)
		{
			Accesibility = accessibility;
			Namespace = @namespace;
			TypeName = typeName;
			Fields = new List<JsonField>(fields);
		}
	}
}
