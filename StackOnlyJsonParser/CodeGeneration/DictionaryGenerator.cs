using StackOnlyJsonParser.CodeStructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser.CodeGeneration
{
	internal static class DictionaryGenerator
	{
		public static string Generate(JsonDictionary dictionary)
		{
			return $@"
using System;
using System.Buffers;
using System.Text.Json;

namespace {dictionary.Namespace}
{{
	{dictionary.Accesibility} readonly ref partial struct {dictionary.TypeName}
	{{
		private readonly Utf8JsonReader _jsonReader;

		public readonly bool HasValue {{ get; }}

		public {dictionary.TypeName}(ref Utf8JsonReader jsonReader)
		{{
			if (jsonReader.TokenType != JsonTokenType.StartObject && jsonReader.TokenType != JsonTokenType.Null) jsonReader.Read();

			switch (jsonReader.TokenType)
			{{
				case JsonTokenType.StartObject:
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
					throw new JsonException($""Expected '{{{{', but got {{jsonReader.TokenType}}"");
			}}
		}}

		public bool Any() => HasValue && _jsonReader.TokenType != JsonTokenType.EndObject;
		public Enumerator GetEnumerator() => new Enumerator(_jsonReader);

		public ref struct Enumerator
		{{
			private Utf8JsonReader _jsonReader;

			public Enumerator(in Utf8JsonReader jsonReader)
			{{
				_jsonReader = jsonReader;
				Current = default;
			}}

			public KeyValuePair Current {{ get; private set; }}

			public bool MoveNext()
			{{
				if (_jsonReader.TokenType == JsonTokenType.EndObject || _jsonReader.TokenType == JsonTokenType.None) return false;

				var key = {DeserializationGenerator.Generate("_jsonReader", dictionary.KeyType)};
				_jsonReader.Read();
				var value = {DeserializationGenerator.Generate("_jsonReader", dictionary.ValueType)};
				_jsonReader.Read();

				Current = new KeyValuePair(key, value);

				return true;
			}}

			public readonly ref struct KeyValuePair
			{{
				public {dictionary.KeyType} Key {{ get; }}
				public {dictionary.ValueType} Value {{ get; }}

				public KeyValuePair({dictionary.KeyType} key, {dictionary.ValueType} value)
				{{
					Key = key;
					Value = value;
				}}
			}}
		}}
	}}
}}
";
		}
	}
}
