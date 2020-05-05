using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace StackOnlyJsonParser
{
    interface Interface
    {

    }

    ref struct CollectionEnumerator
    {
        private Utf8JsonReader _jsonReader;

        public CollectionEnumerator(Utf8JsonReader jsonReader)
        {
            _jsonReader = jsonReader;
            Current = default;

            if (_jsonReader.TokenType == JsonTokenType.None) _jsonReader.Read();
            if (_jsonReader.TokenType != JsonTokenType.StartArray) throw new JsonException("Expected '['");
            if (!_jsonReader.Read()) throw new JsonException("Expected ']'");
        }

        public double Current { get; private set; }

        public bool MoveNext()
        {
            if (_jsonReader.TokenType == JsonTokenType.EndArray) return false;

            Current = _jsonReader.GetDouble();

            if (!_jsonReader.Read()) throw new JsonException("Expected ']'");

            return true;
        }
    }

    ref struct CollectionEnumerable
    {
        private Utf8JsonReader _jsonReader;

        public CollectionEnumerable(ref Utf8JsonReader jsonReader)
        {
            _jsonReader = jsonReader;
            jsonReader.Skip();
        }

        public CollectionEnumerator GetEnumerator() => new CollectionEnumerator(_jsonReader);
    }

    readonly ref struct ClassParser
    {
        public readonly double A;
        public readonly double B;

        public ClassParser(ReadOnlySpan<byte> jsonData) : this(new Utf8JsonReader(jsonData, new JsonReaderOptions { CommentHandling = JsonCommentHandling.Skip }))
        { }

        public ClassParser(ReadOnlySequence<byte> jsonData) : this(new Utf8JsonReader(jsonData, new JsonReaderOptions { CommentHandling = JsonCommentHandling.Skip }))
        { }

        private ClassParser(Utf8JsonReader jsonReader) : this(ref jsonReader) { }
        internal ClassParser(ref Utf8JsonReader jsonReader)
        {
            A = default;
            B = default;

            if (jsonReader.TokenType == JsonTokenType.None) jsonReader.Read();
            if (jsonReader.TokenType != JsonTokenType.StartObject) throw new JsonException("Expected '{'");

            while (jsonReader.TokenType != JsonTokenType.EndObject)
            {
                if (!jsonReader.Read()) throw new JsonException("Expected '}'");
                if (jsonReader.TokenType != JsonTokenType.PropertyName) throw new JsonException("Expected property name");

                if (jsonReader.ValueTextEquals("PropertyName"))
                {
                    jsonReader.Read();
                    A = jsonReader.GetDouble();
                }
                else if (jsonReader.ValueTextEquals("PropertyName"))
                {
                    jsonReader.Read();
                    B = jsonReader.GetDouble();
                }
                else
                {
                    jsonReader.Read();
                    jsonReader.Skip();
                }

                jsonReader.Read();
            }
        }
    }

    class Playground
    {
    }
}
