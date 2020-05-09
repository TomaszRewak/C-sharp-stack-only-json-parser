using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace StackOnlyJsonParser
{
	public readonly ref struct StackOnlyJsonString
	{
		private readonly Utf8JsonReader _jsonReader;

		public readonly bool HasValue;

		public bool HasValueSequence => _jsonReader.HasValueSequence;
		public ReadOnlySpan<byte> ValueSpan => _jsonReader.ValueSpan;
		public ReadOnlySequence<byte> ValueSequence => _jsonReader.ValueSequence;

		public string Value => _jsonReader.GetString() ?? throw new InvalidOperationException("String value not available");

		internal StackOnlyJsonString(Utf8JsonReader jsonReader)
		{
			HasValue = true;
			_jsonReader = jsonReader;
		}
	}
}
