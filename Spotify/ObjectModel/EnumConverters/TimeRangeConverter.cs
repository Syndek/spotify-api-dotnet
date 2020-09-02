﻿using System;

namespace Spotify.ObjectModel.EnumConverters
{
    internal static class TimeRangeConverter : Object
    {
        internal static TimeRange FromSpotifyString(String timeRange) => timeRange switch
        {
            "short_term" => TimeRange.ShortTerm,
            "medium_term" => TimeRange.MediumTerm,
            "long_term" => TimeRange.LongTerm,
            _ => throw new ArgumentException($"Invalid {nameof(TimeRange)} string value: {timeRange}", nameof(timeRange))
        };

        internal static String ToSpotifyString(this TimeRange timeRange) => timeRange switch
        {
            TimeRange.ShortTerm => "short_term",
            TimeRange.MediumTerm => "medium_term",
            TimeRange.LongTerm => "long_term",
            _ => throw new ArgumentException($"Invalid {nameof(TimeRange)} value: {timeRange}", nameof(timeRange))
        };
    }
}