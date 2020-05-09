using System.Collections.Generic;
using System.Linq;

namespace StackOnlyJsonParser.CodeStructure
{
	internal sealed class JsonFieldType
	{
		public string Name { get; }
		public IReadOnlyCollection<JsonCollectionKind> CollectionKind { get; }

		public string FullName
		{
			get
			{
				var collectionType = string.Join("", CollectionKind);

				switch (Name)
				{
					case "System.Int32":
					case "System.Int64":
					case "System.String":
					case "StackOnlyJsonParser":
						return CollectionKind.Any()
							? $"StackOnlyJsonParser.Collections.{Name.Split('.')[1]}{collectionType}Reader"
							: Name.Split('.')[1];
					default:
						return CollectionKind.Any()
							? $"{Name}.JsonCollections.{collectionType}Reader"
							: $"{Name}";
				}
			}
		}

		public JsonFieldType(string name, IEnumerable<JsonCollectionKind> collectionKind) : this(name)
		{
			CollectionKind = CollectionKind.Concat(collectionKind).ToList();
		}

		public JsonFieldType(string type)
		{
			var collectionKind = new List<JsonCollectionKind>();

			while (true)
			{
				if (type.EndsWith("[]"))
				{
					collectionKind.Insert(0, JsonCollectionKind.Array);
					type = type[0..^2];
					continue;
				}

				if (type.EndsWith("{}"))
				{
					collectionKind.Insert(0, JsonCollectionKind.Dictionary);
					type = type[0..^2];
					continue;
				}

				break;
			}

			Name = type;
			CollectionKind = collectionKind;
		}
	}
}
