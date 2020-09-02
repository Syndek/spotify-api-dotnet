﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System
{
    internal static class EnumExtensions : Object
    {
        [SuppressMessage("Usage", "CA2248")]
        internal static IEnumerable<TEnum> GetFlags<TEnum>(this TEnum flags) where TEnum : Enum
        {
            foreach (TEnum value in Enum.GetValues(flags.GetType()))
            {
                if (flags.HasFlag(value))
                {
                    yield return value;
                }
            }
        }

        internal static String GetName<TEnum>(this TEnum value) where TEnum : Enum =>
            Enum.GetName(typeof(TEnum), value) ?? throw new ArgumentException($"Invalid enum value: {value}", nameof(value));
    }
}