using System;
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

        public CollectionEnumerator(ref Utf8JsonReader jsonReader)
        {
            _jsonReader = jsonReader;
            Current = default;

            if (_jsonReader.TokenType != JsonTokenType.StartArray) throw new JsonException("Expected '['");

            jsonReader.Skip();
        }

        public double Current { get; private set; }

        public bool MoveNext()
        {
            if (!_jsonReader.Read()) throw new JsonException("Expected ']'");
            if (_jsonReader.TokenType == JsonTokenType.EndArray) return false;

            Current = _jsonReader.GetDouble();

            return true;
        }
    }

    ref struct CollectionEnumerable
    {
        Utf8JsonReader _jsonReader;

        public IEnumerator<int> GetEnumerator()
        {
            if (_jsonReader.TokenType != JsonTokenType.StartArray) throw new JsonException("Expected '['");

            if (!_jsonReader.Read()) throw new JsonException("Expected ']'");



            _jsonReader.Read();
            if (_jsonReader.TokenType != JsonTokenType.EndArray) throw new JsonException("Expected ']'");

            throw new NotImplementedException();
        }
    }

    class Playground
    {
    }
}
