﻿using Spotify.ObjectModel.Collections;
using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    public class PagingConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) =>
            typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Paging<>);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (!CanConvert(typeToConvert))
            {
                throw new ArgumentException($"{typeToConvert} must be a generic Paging type.", nameof(typeToConvert));
            }

            var elementType = typeToConvert.GetGenericArguments()[0];

            return (JsonConverter)Activator.CreateInstance(
                typeof(PagingConverter<>).MakeGenericType(elementType),
                BindingFlags.Instance | BindingFlags.Public,
                null,
                null,
                null)!;
        }
    }
}
