﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Spotify.ObjectModel.Serialization.EnumConverters;

namespace Spotify.ObjectModel.Serialization
{
    using ExternalUrls = IReadOnlyDictionary<String, Uri>;
    using ImageArray = IReadOnlyList<Image>;
    using StringArray = IReadOnlyList<String>;

    public sealed class EpisodeConverter : JsonConverter<Episode>
    {
        public override Episode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType is not JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var imageArrayConverter = options.GetConverter<ImageArray>();
            var resumePointConverter = options.GetConverter<ResumePoint>();
            var simplifiedShowConverter = options.GetConverter<SimplifiedShow>();
            var stringArrayConverter = options.GetConverter<StringArray>();
            var uriConverter = options.GetConverter<Uri>();

            String id = String.Empty;
            Uri uri = null!;
            Uri href = null!;
            String name = String.Empty;
            String description = String.Empty;
            ImageArray images = Array.Empty<Image>();
            SimplifiedShow show = null!;
            Int32 duration = default;
            DateTime releaseDate = default;
            ReleaseDatePrecision releaseDatePrecision = default;
            Boolean isExplicit = default;
            Boolean isPlayable = default;
            Boolean isExternallyHosted = default;
            StringArray languages = Array.Empty<String>();
            Uri? audioPreviewUrl = null;
            ExternalUrls externalUrls = null!;
            ResumePoint? resumePoint = null;

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

                reader.Read(); // Skip to next token.

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
                    case "description":
                        description = reader.GetString()!;
                        break;
                    case "images":
                        images = imageArrayConverter.Read(ref reader, typeof(ImageArray), options)!;
                        break;
                    case "show":
                        show = simplifiedShowConverter.Read(ref reader, typeof(SimplifiedShow), options)!;
                        break;
                    case "duration_ms":
                        duration = reader.GetInt32();
                        break;
                    case "release_date":
                        releaseDate = reader.GetReleaseDate();
                        break;
                    case "release_date_precision":
                        releaseDatePrecision = ReleaseDatePrecisionConverter.FromSpotifyString(reader.GetString()!);
                        break;
                    case "explicit":
                        isExplicit = reader.GetBoolean();
                        break;
                    case "is_playable":
                        isPlayable = reader.GetBoolean();
                        break;
                    case "is_externally_hosted":
                        isExternallyHosted = reader.GetBoolean();
                        break;
                    case "languages":
                        languages = stringArrayConverter.Read(ref reader, typeof(StringArray), options)!;
                        break;
                    case "audio_preview_url":
                        audioPreviewUrl = (reader.TokenType is JsonTokenType.Null) ? null : uriConverter.Read(ref reader, typeof(Uri), options!);
                        break;
                    case "external_urls":
                        externalUrls = externalUrlsConverter.Read(ref reader, typeof(ExternalUrls), options)!;
                        break;
                    case "resume_point":
                        resumePoint = resumePointConverter.Read(ref reader, typeof(ResumePoint), options);
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
                description,
                images,
                show,
                duration,
                releaseDate,
                releaseDatePrecision,
                isExplicit,
                isPlayable,
                isExternallyHosted,
                languages,
                audioPreviewUrl,
                externalUrls,
                resumePoint);
        }

        public override void Write(Utf8JsonWriter writer, Episode value, JsonSerializerOptions options)
        {
            var externalUrlsConverter = options.GetConverter<ExternalUrls>();
            var imageArrayConverter = options.GetConverter<ImageArray>();
            var resumePointConverter = options.GetConverter<ResumePoint>();
            var simplifiedShowConverter = options.GetConverter<SimplifiedShow>();
            var stringArrayConverter = options.GetConverter<StringArray>();
            var uriConverter = options.GetConverter<Uri>();

            writer.WriteStartObject();
            writer.WriteString("type", "episode");
            writer.WriteString("id", value.Id);
            writer.WritePropertyName("uri");
            uriConverter.Write(writer, value.Uri, options);
            writer.WritePropertyName("href");
            uriConverter.Write(writer, value.Href, options);
            writer.WriteString("name", value.Name);
            writer.WriteString("description", value.Description);
            writer.WritePropertyName("images");
            imageArrayConverter.Write(writer, value.Images, options);
            writer.WritePropertyName("show");
            simplifiedShowConverter.Write(writer, value.Show, options);
            writer.WriteNumber("duration_ms", value.Duration);
            writer.WriteReleaseDate(value.ReleaseDate, value.ReleaseDatePrecision);
            writer.WriteString("release_date_precision", value.ReleaseDatePrecision.ToSpotifyString());
            writer.WriteBoolean("explicit", value.IsExplicit);
            writer.WriteBoolean("is_playable", value.IsPlayable);
            writer.WriteBoolean("is_externally_hosted", value.IsExternallyHosted);
            writer.WritePropertyName("languages");
            stringArrayConverter.Write(writer, value.Languages, options);
            if (value.AudioPreviewUrl is Uri audioPreviewUrl)
            {
                writer.WritePropertyName("audio_preview_url");
                uriConverter.Write(writer, audioPreviewUrl, options);
            }
            else
            {
                writer.WriteNull("audio_preview_url");
            }
            writer.WritePropertyName("external_urls");
            externalUrlsConverter.Write(writer, value.ExternalUrls, options);
            if (value.ResumePoint is ResumePoint resumePoint)
            {
                writer.WritePropertyName("resume_point");
                resumePointConverter.Write(writer, resumePoint, options);
            }
            else
            {
                writer.WriteNull("resume_point");
            }
            writer.WriteEndObject();
        }
    }
}