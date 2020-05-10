using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOnlyJsonParser.Example.Model
{
	[StackOnlyJsonType]
	public readonly ref partial struct Color
	{
		public readonly int R;
		public readonly int G;
		public readonly int B;
	}

	[StackOnlyJsonArray(typeof(Color))]
	public readonly ref partial struct ColorArray
	{ }
}
