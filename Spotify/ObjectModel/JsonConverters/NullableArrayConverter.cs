﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.JsonConverters
{
    internal sealed class NullableArrayConverter<TElement> : JsonConverter<IReadOnlyList<TElement?>> where TElement : class
    {
        internal static readonly NullableArrayConverter<TElement> Instance = new();

        private NullableArrayConverter() : base() { }

        public override IReadOnlyList<TElement?> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.AssertTokenType(JsonTokenType.StartArray);

            var elements = new List<TElement?>();
            var elementType = typeof(TElement);
            var elementConverter = (JsonConverter<TElement>) options.GetConverter(elementType);

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.EndArray:
                        return elements.AsReadOnly();
                    case JsonTokenType.Null:
                        elements.Add(null);
                        break;
                    default:
                        elements.Add(elementConverter.Read(ref reader, elementType, options));
                        break;
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IReadOnlyList<TElement?> value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}