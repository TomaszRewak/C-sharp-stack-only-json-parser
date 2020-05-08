using System.Collections.Generic;

namespace StackOnlyJsonParser.CodeStructure
{
	internal sealed class JsonType
	{
		public string Namespace { get; set; }
		public string TypeName { get; }
		public IReadOnlyCollection<JsonField> Fields { get; }

		public JsonType(string fullTypeName, IEnumerable<JsonField> fields)
		{
			var lastDot = fullTypeName.LastIndexOf('.');

			Namespace = fullTypeName[..lastDot];
			TypeName = fullTypeName[lastDot..];
			Fields = new List<JsonField>(fields);
		}
	}
}
