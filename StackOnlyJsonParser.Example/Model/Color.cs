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
		public readonly int R { get; }
		public readonly int G { get; }
		public readonly int B { get; }

		public int Brightness => (R + G + B) / 3;
	}

	[StackOnlyJsonArray(typeof(Color))]
	public readonly ref partial struct ColorArray
	{ }
}
