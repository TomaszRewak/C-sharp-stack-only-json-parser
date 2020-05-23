using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StackOnlyJsonParser.PerformanceTests
{
	internal class DataGenerator
	{
		private static readonly string[] Colors = new[] { "red", "green", "blue", "orange", "black" };
		private static readonly Dictionary<string, string> Regions = new Dictionary<string, string> { ["PL"] = "PLN", ["NL"] = "EUR", ["GER"] = "EUR", ["US"] = "USD", ["CZ"] = "CZK" };

		private readonly Random _random = new Random(0);

		public byte[] Generate(int elements)
		{
			var data = Enumerable
				.Repeat(0, elements)
				.Select(_ => new
				{
					Name = NextName(),
					BoxSize = new
					{
						Width = _random.Next(10000) * 0.01,
						Height = _random.Next(10000) * 0.01,
						Depth = _random.Next(10000) * 0.01,
					},
					AvailableItems = _random.Next(0, 10),
					Colors = Colors.Where(_ => _random.Next(2) > 0),
					Regions = Regions.Where(_ => _random.Next(3) > 0).ToDictionary(
						e => e.Key,
						e => new {
							Currency = e.Value,
							Value = _random.Next(100, 10000) / 100m
						})
				});

			return JsonSerializer.SerializeToUtf8Bytes(data);
		}

		private string NextName()
		{
			return new string(Enumerable
				.Repeat('A', _random.Next(5, 10))
				.Select(l => (char)(l + _random.Next(0, 23)))
				.ToArray());
		}
	}
}
