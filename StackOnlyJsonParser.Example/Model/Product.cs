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
		public readonly int Id1;
		public readonly int Id2;
		public readonly string Name;
		public readonly StackOnlyJsonString Surname;

		//[StackOnlyJsonField]
		//public readonly double? Price;
	}
}
