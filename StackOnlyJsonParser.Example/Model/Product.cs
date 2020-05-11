using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOnlyJsonParser.Example.Model
{
	[StackOnlyJsonArray(typeof(int))]
	public readonly ref partial struct SizeArray
	{ }

	[StackOnlyJsonType]
	public readonly ref partial struct Product
	{
		public readonly int Id1 { get; }
		public readonly int Id2 { get; }
		public readonly string Name { get; }
		public readonly StackOnlyJsonString ShortName { get; }
		public readonly SizeArray Sizes { get; }
		public readonly Color MainColor { get; }
		public readonly Color PackageColor { get; }
		public readonly Color LogoColor { get; }
		public readonly ColorArray AdditinalColors { get; }
		public readonly PriceDictionary Prices { get; }
	}
}
