using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOnlyJsonParser.Example.Model
{
	[StackOnlyJsonType]
	public readonly ref partial struct Product
	{
		public readonly int Id1;
		public readonly int Id2;
		public readonly string Name;
		public readonly StackOnlyJsonString Surname;
		public readonly Color MainColor;
		public readonly Color PackageColor;
		public readonly Color LogoColor;
		//public readonly ColorArray AdditinalColors;
	}
}
