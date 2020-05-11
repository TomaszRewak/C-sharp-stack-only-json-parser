using Microsoft.CodeAnalysis;
using StackOnlyJsonParser.CodeAnalysis;
using System.Collections.Generic;

namespace StackOnlyJsonParser.CodeStructure
{
	internal sealed class JsonField
	{
		public string Name { get; }
		public string Type { get; }
		public IReadOnlyCollection<string> SerializedNames { get; }

		public JsonField(IPropertySymbol property)
		{
			Name = property.Name;
			Type = property.Type.GetFullName();
			SerializedNames = new[] { property.Name };
		}
	}
}
