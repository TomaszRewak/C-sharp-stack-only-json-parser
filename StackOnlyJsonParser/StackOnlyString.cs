using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace StackOnlyJsonParser
{
    public ref struct StackOnlyString
    {
        private readonly Utf8JsonReader _jsonReader;

        public bool HasValueSequence => _jsonReader.HasValueSequence;
        public ReadOnlySpan<byte> ValueSpan => _jsonReader.ValueSpan;
        public ReadOnlySequence<byte> ValueSequence => _jsonReader.ValueSequence;

        public string Value => _jsonReader.GetString() ?? throw new InvalidOperationException("String value not available");

        internal StackOnlyString(Utf8JsonReader jsonReader)
        {
            _jsonReader = jsonReader;
        }
    }
}
