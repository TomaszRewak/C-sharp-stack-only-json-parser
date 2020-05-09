using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOnlyJsonParser.Example.Model
{
	[StackOnlyJsonType()]
	[StackOnlyJsonCollection(JsonCollectionKind.ArrayOf, JsonCollectionKind.DictionaryOf)]
	[StackOnlyJsonCollection(JsonCollectionKind.ArrayOf, JsonCollectionKind.ArrayOf)]
	public readonly ref partial struct Product
	{
		[StackOnlyJsonProperty]
		public readonly int Id;

		[StackOnlyJsonProperty]
		public readonly string Name;

		[StackOnlyJsonProperty]
		public readonly double? Price;

		[StackOnlyJsonProperty]
		public readonly StackOnlyJsonString Surname;
	}
}
