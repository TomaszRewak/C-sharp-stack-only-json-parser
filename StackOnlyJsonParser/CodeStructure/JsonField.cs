using Microsoft.CodeAnalysis;
using StackOnlyJsonParser.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace StackOnlyJsonParser.CodeStructure
{
	internal sealed class JsonField
	{
		public string Name { get; }
		public JsonDataType Type { get; }
		public IReadOnlyCollection<string> SerializedNames { get; }

		public JsonField(IPropertySymbol property)
		{
			Name = property.Name;
			Type = new JsonDataType(property.Type);
			SerializedNames = property
					.GetAttributes(typeof(StackOnlyJsonFieldAttribute).FullName)
					.SelectMany(attribute => attribute.ConstructorArguments)
					.SelectMany(parameter => parameter.Values)
					.Select(value => value.Value as string)
					.Where(name => name != null)
					.Distinct()
					.ToList();

			if (!SerializedNames.Any())
				SerializedNames = new[] { property.Name };
		}
	}
}
