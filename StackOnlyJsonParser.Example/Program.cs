using System;
using System.Text.Json;
using StackOnlyJsonParser.Example.Model;

namespace StackOnlyJsonParser.Example
{
	class Program
	{
		static void Main(string[] args)
		{
			var bytes = JsonSerializer.SerializeToUtf8Bytes(new {
				Name = "Some name",
				Price = 1.2
			});

			var product = new Product(bytes);

			Console.WriteLine(product.Name);
		}
	}
}
