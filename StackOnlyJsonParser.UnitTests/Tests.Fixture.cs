using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StackOnlyJsonParser.UnitTests
{
	public sealed partial class Tests
	{
		private byte[] Encode(string data) => Encoding.UTF8.GetBytes(data);
		private Utf8JsonReader Read(string data) => new Utf8JsonReader(Encode(data));
	}
}
