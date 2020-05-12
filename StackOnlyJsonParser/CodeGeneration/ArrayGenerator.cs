using StackOnlyJsonParser.CodeStructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser.CodeGeneration
{
	internal static class ArrayGenerator
	{
		public static string Generate(JsonArray array)
		{
			return $@"
using System;
using System.Buffers;
using System.Text.Json;

namespace {array.Namespace}
{{
	{array.Accesibility} readonly ref partial struct {array.TypeName}
	{{
		private readonly Utf8JsonReader _jsonReader;

		public readonly bool HasValue {{ get; }}

		public {array.TypeName}(ReadOnlySpan<byte> jsonData) : this(new Utf8JsonReader(jsonData, new JsonReaderOptions {{ CommentHandling = JsonCommentHandling.Skip }}))
		{{}}
		public {array.TypeName}(ReadOnlySequence<byte> jsonData) : this(new Utf8JsonReader(jsonData, new JsonReaderOptions {{ CommentHandling = JsonCommentHandling.Skip }}))
		{{}}
		private {array.TypeName}(Utf8JsonReader jsonReader) : this(ref jsonReader)
		{{}}
		public {array.TypeName}(ref Utf8JsonReader jsonReader)
		{{
			if (jsonReader.TokenType != JsonTokenType.StartArray && jsonReader.TokenType != JsonTokenType.Null) jsonReader.Read();

			switch (jsonReader.TokenType)
			{{
				case JsonTokenType.StartArray:
					HasValue = true;
					_jsonReader = jsonReader;
					_jsonReader.Read();
					jsonReader.Skip();
					break;

				case JsonTokenType.Null:
					HasValue = false;
					_jsonReader = default;
					break;

				default:
					throw new JsonException($""Expected '[', but got {{jsonReader.TokenType}}"");
			}}
		}}

		public bool Any() => HasValue && _jsonReader.TokenType != JsonTokenType.EndArray;
		public Enumerator GetEnumerator() => new Enumerator(_jsonReader);

		public ref struct Enumerator
		{{
			private Utf8JsonReader _jsonReader;

			public Enumerator(in Utf8JsonReader jsonReader)
			{{
				_jsonReader = jsonReader;
				Current = default;
			}}

			public {array.ElementType} Current {{ get; private set; }}

			public bool MoveNext()
			{{
				if (_jsonReader.TokenType == JsonTokenType.EndArray || _jsonReader.TokenType == JsonTokenType.None) return false;

				Current = {DeserializationGenerator.Generate("_jsonReader", array.ElementType)};
				_jsonReader.Read();

				return true;
			}}
		}}
	}}
}}
";
		}
	}
}
