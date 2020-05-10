using System.Collections.Generic;
using System.Linq;

namespace StackOnlyJsonParser.CodeStructure
{
	internal sealed class JsonFieldType
	{
		public string Name { get; }
		public IReadOnlyCollection<JsonCollectionKind> CollectionKind { get; }

		public JsonFieldType(string name, IEnumerable<JsonCollectionKind> collectionKind)
		{
			Name = name;
			CollectionKind = CollectionKind.Concat(collectionKind).ToList();
		}
	}
}
