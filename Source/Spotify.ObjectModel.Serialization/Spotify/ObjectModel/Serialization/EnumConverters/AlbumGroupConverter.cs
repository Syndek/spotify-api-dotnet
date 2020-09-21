﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Spotify.ObjectModel.Serialization.EnumConverters
{
    public static class AlbumGroupConverter : Object
    {
        public static AlbumGroups FromSpotifyString(String albumGroup) => albumGroup switch
        {
            "album" => AlbumGroups.Album,
            "single" => AlbumGroups.Single,
            "compilation" => AlbumGroups.Compilation,
            "appears_on" => AlbumGroups.AppearsOn,
            _ => throw new ArgumentException($"Invalid {nameof(AlbumGroups)} string value: {albumGroup}", nameof(albumGroup))
        };

        public static AlbumGroups FromSpotifyStrings(IEnumerable<String> albumGroups) =>
            albumGroups.Aggregate(
                new AlbumGroups(),
                (current, albumGroups) => current | AlbumGroupConverter.FromSpotifyString(albumGroups));

        public static String ToSpotifyString(this AlbumGroups albumGroup) => albumGroup switch
        {
            AlbumGroups.Album => "album",
            AlbumGroups.Single => "single",
            AlbumGroups.Compilation => "compilation",
            AlbumGroups.AppearsOn => "appears_on",
            _ => throw new ArgumentException($"Invalid {nameof(AlbumGroups)} value: {albumGroup}", nameof(albumGroup))
        };

        public static IEnumerable<String> ToSpotifyStrings(this AlbumGroups albumGroups) =>
            albumGroups.GetFlags().Select(AlbumGroupConverter.ToSpotifyString);
    }
}