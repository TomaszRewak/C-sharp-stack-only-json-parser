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
				PackageColor = (object)null,
				AdditinalColors = new[] {
					new { R = 1, G = 2, B = 3 },
					new { R = 4, G = 5, B = 6 }
				},
				Prices = new {
					PL = new
					{
						Currency = "PLN",
						value = 100
					},
					NL = new
					{
						Currency = "EUR",
						val = 25
					}
				},
				StackPrices = new
				{
					US = new
					{
						Currency = "DOL",
						value = 100
					},
					NL = new
					{
						Currency = "EUR",
						val = 25
					}
				}
			});

			var product = new Product(bytes);

			Console.WriteLine(product.Id1);
			Console.WriteLine(product.Id2);
			Console.WriteLine(product.Name);
			Console.WriteLine(product.ShortName.ToString());
			Console.WriteLine(product.MainColor.HasValue);
			Console.WriteLine($"{product.MainColor.R} {product.MainColor.G} {product.MainColor.B}");
			Console.WriteLine(product.MainColor.Brightness);
			Console.WriteLine(product.PackageColor.HasValue);
			Console.WriteLine(product.LogoColor.HasValue);
			Console.WriteLine(product.Sizes.Any());
			foreach (var size in product.Sizes)
				Console.WriteLine($"size {size}");
			foreach (var color in product.AdditinalColors)
				Console.WriteLine($"color {color.R}, {color.G}, {color.B}");
			foreach (var price in product.Prices)
				Console.WriteLine($"price {price.Key}: {price.Value.Value} {price.Value.Currency}");
			foreach (var price in product.StackPrices)
				Console.WriteLine($"price {price.Key.ToString()}: {price.Value.Value} {price.Value.Currency}");
		}
	}
}
