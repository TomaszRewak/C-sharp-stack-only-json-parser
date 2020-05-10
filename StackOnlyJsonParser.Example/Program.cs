using System;
using System.Text.Json;
using StackOnlyJsonParser.Example.Model;

namespace StackOnlyJsonParser.Example
{
	class Program
	{
		static void Main(string[] args)
		{
			var bytes = JsonSerializer.SerializeToUtf8Bytes(new
			{
				Name = "Some name",
				Surname = "Some surname",
				Values = new[] { 4, 5, 6 },
				Price = 1.2,
				Id1 = 1,
				Id2 = 2,
				MainColor = new
				{
					R = 200,
					G = 100,
					B = 30
				}
			});

			var product = new Product(bytes);

			Console.WriteLine(product.Id1);
			Console.WriteLine(product.Id2);
			Console.WriteLine(product.Name);
			Console.WriteLine(product.Surname.ToString());
			Console.WriteLine(product.MainColor.HasValue);
			Console.WriteLine($"{product.MainColor.R} {product.MainColor.G} {product.MainColor.B}");
		}
	}
}
