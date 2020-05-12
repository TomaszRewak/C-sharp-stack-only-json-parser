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

		public bool ValueTextEquals(string text) => _jsonReader.ValueTextEquals(text);
		public bool ValueTextEquals(ReadOnlySpan<char> text) => _jsonReader.ValueTextEquals(text);
		public bool ValueTextEquals(ReadOnlySpan<byte> utf8Text) => _jsonReader.ValueTextEquals(utf8Text);

		public override string ToString() => HasValue ? _jsonReader.GetString() : null;

		public StackOnlyJsonString(ref Utf8JsonReader jsonReader)
		{
			if (jsonReader.TokenType != JsonTokenType.PropertyName && jsonReader.TokenType != JsonTokenType.String && jsonReader.TokenType != JsonTokenType.Null) jsonReader.Read();
			if (jsonReader.TokenType != JsonTokenType.PropertyName && jsonReader.TokenType != JsonTokenType.String && jsonReader.TokenType != JsonTokenType.Null) throw new JsonException($"Expected string, but got { jsonReader.TokenType }");

			HasValue = jsonReader.TokenType != JsonTokenType.Null;
			_jsonReader = jsonReader;
		}
	}
}
