using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace StackOnlyJsonParser.CodeStructure
{
	internal sealed class JsonType
	{
		public Accessibility Accesibility { get; }
		public string Namespace { get; }
		public string TypeName { get; }
		public IReadOnlyCollection<JsonField> Fields { get; }

		public JsonType(Accessibility accessibility, string @namespace, string typeName, IEnumerable<JsonField> fields)
		{
			Namespace = @namespace;
			TypeName = typeName;
			Fields = new List<JsonField>(fields);
		}
	}
}
