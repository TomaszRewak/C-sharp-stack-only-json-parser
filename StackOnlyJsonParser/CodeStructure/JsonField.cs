using System.Collections.Generic;

namespace StackOnlyJsonParser.CodeStructure
{
	internal sealed class JsonField
	{
		public string Name { get; }
		public JsonFieldType Type { get; }
		public IReadOnlyCollection<string> SerializedNames { get; }

		public JsonField(string name, JsonFieldType type, IEnumerable<string> serializedNames)
		{
			Name = name;
			Type = type;
			SerializedNames = new List<string>(serializedNames);
		}
	}
}
