using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOnlyJsonParser.Example.Model
{
	[StackOnlyJsonType]
	public readonly ref partial struct Price
	{
		public string Currency { get; }
		public double Value { get; }
	}

	[StackOnlyJsonDictionary(typeof(string), typeof(Price))]
	public readonly ref partial struct PriceDictionary
	{ }
}
