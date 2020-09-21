﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spotify.ObjectModel.Serialization
{
    using CountryCodeArray = IReadOnlyList<CountryCode>;
    using ExternalUrls = IReadOnlyDictionary<String, Uri>;
    using SimplifiedArtistArray = IReadOnlyList<SimplifiedArtist>;

    public sealed class SimplifiedTrackConverter : JsonConverter<SimplifiedTrack>
    {
        public override SimplifiedTrack? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var countryCodeArrayConverter = options.GetConverter<CountryCodeArray>();
            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var simplifiedArtistArrayConverter = options.GetConverter<SimplifiedArtistArray>();
            var uriConverter = options.GetConverter<Uri>();

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
            SimplifiedArtistArray artists = Array.Empty<SimplifiedArtist>();
            Int32 duration = default;
            Int32 trackNumber = default;
            Int32 discNumber = default;
            Boolean isExplicit = default;
            Boolean isLocal = default;
            CountryCodeArray availableMarkets = Array.Empty<CountryCode>();
            String previewUrl = String.Empty;
            ExternalUrls externalUrls = null!;

            while (reader.Read())
            {
                if (reader.TokenType is JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType is not JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                var propertyName = reader.GetString();

                reader.Read(); // Read to next token.

                switch (propertyName)
                {
                    case "id":
                        id = reader.GetString()!;
                        break;
                    case "uri":
                        uri = uriConverter.Read(ref reader, typeof(Uri), options)!;
                        break;
                    case "href":
                        href = uriConverter.Read(ref reader, typeof(Uri), options)!;
                        break;
                    case "name":
                        name = reader.GetString()!;
                        break;
                    case "artists":
                        artists = simplifiedArtistArrayConverter.Read(ref reader, typeof(SimplifiedArtistArray), options)!;
                        break;
                    case "duration":
                        duration = reader.GetInt32();
                        break;
                    case "track_number":
                        trackNumber = reader.GetInt32();
                        break;
                    case "disc_number":
                        discNumber = reader.GetInt32();
                        break;
                    case "explicit":
                        isExplicit = reader.GetBoolean();
                        break;
                    case "is_local":
                        isLocal = reader.GetBoolean();
                        break;
                    case "available_markets":
                        availableMarkets = countryCodeArrayConverter.Read(ref reader, typeof(CountryCodeArray), options)!;
                        break;
                    case "preview_url":
                        previewUrl = reader.GetString()!;
                        break;
                    case "external_urls":
                        externalUrls = externalUrlsConverter.Read(ref reader, typeof(ExternalUrls), options)!;
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return new(
                id,
                uri,
                href,
                name,
                artists,
                duration,
                discNumber,
                trackNumber,
                isExplicit,
                isLocal,
                availableMarkets,
                previewUrl,
                externalUrls);
        }

        public override void Write(Utf8JsonWriter writer, SimplifiedTrack value, JsonSerializerOptions options) => throw new NotSupportedException();
    }
}