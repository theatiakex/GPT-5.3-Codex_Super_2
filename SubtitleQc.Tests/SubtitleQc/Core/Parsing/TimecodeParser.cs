using System;
using System.Globalization;

namespace SubtitleQc.Core.Parsing;

public static class TimecodeParser
{
    public static TimeSpan Parse(string value)
    {
        string normalized = Normalize(value);
        if (TimeSpan.TryParseExact(normalized, @"hh\:mm\:ss\.fff", CultureInfo.InvariantCulture, out TimeSpan parsed))
        {
            return parsed;
        }

        throw new FormatException($"Unsupported timecode value: '{value}'.");
    }

    private static string Normalize(string value)
    {
        return value.Trim().Replace(',', '.');
    }
}
