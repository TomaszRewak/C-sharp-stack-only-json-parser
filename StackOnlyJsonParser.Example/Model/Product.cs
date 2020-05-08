using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOnlyJsonParser.Example.Model
{
	[StackOnlyJsonType("StackOnlyProduct")]
	[StackOnlyJsonCollection(JsonCollectionKind.Array, JsonCollectionKind.Dictionary)]
	[StackOnlyJsonCollection(JsonCollectionKind.Array, JsonCollectionKind.Array)]
	public class Product
	{
		[StackOnlyJsonProperty]
		public int Id { get; set; }

		[StackOnlyJsonProperty, StrictlyStackOnly]
		public string Name { get; set; }

		[StackOnlyJsonProperty]
		public double Price { get; set; }
	}
}
