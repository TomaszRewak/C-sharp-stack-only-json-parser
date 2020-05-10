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
				ShortName = "Some short name",
				Values = new[] { 4, 5, 6 },
				Price = 1.2,
				Id1 = 1,
				Id2 = 2,
				Sizes = new int[] { 100, 200, 300 },
				MainColor = new
				{
					R = 200,
					G = 100,
					B = 30
				},
				PackageColor = (object)null
			});

			var product = new Product(bytes);

			Console.WriteLine(product.Id1);
			Console.WriteLine(product.Id2);
			Console.WriteLine(product.Name);
			Console.WriteLine(product.ShortName.ToString());
			Console.WriteLine(product.MainColor.HasValue);
			Console.WriteLine($"{product.MainColor.R} {product.MainColor.G} {product.MainColor.B}");
			Console.WriteLine(product.PackageColor.HasValue);
			Console.WriteLine(product.LogoColor.HasValue);
			Console.WriteLine(product.Sizes.Any());
			foreach (var size in product.Sizes)
				Console.WriteLine($"size {size}");
		}
	}
}
