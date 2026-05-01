using System;
using System.Collections.Generic;
using System.Linq;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Parsing.Abstractions;

namespace SubtitleQc.Core.Parsing;

public sealed class WebVttParser : ISubtitleParser
{
    public SubtitleFormat Format => SubtitleFormat.WebVtt;

    public SubtitleDocument Parse(string rawSubtitleContent)
    {
        string normalized = rawSubtitleContent.Replace("\r\n", "\n");
        string payload = normalized.StartsWith("WEBVTT", StringComparison.OrdinalIgnoreCase) ? normalized[6..].TrimStart() : normalized;
        var blocks = payload.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
        List<Cue> cues = blocks.Select(ParseCueBlock).Where(cue => cue != null).Cast<Cue>().ToList();
        return new SubtitleDocument(cues);
    }

    private Cue? ParseCueBlock(string block)
    {
        string[] lines = block.Split('\n', StringSplitOptions.None);
        int timeLineIndex = lines[0].Contains("-->", StringComparison.Ordinal) ? 0 : 1;
        if (lines.Length <= timeLineIndex + 1)
        {
            return null;
        }

        (TimeSpan start, TimeSpan end) = ParseRange(lines[timeLineIndex]);
        IReadOnlyList<string> cueLines = lines.Skip(timeLineIndex + 1).ToArray();
        return new Cue(Guid.NewGuid().ToString("N"), start, end, cueLines);
    }

    private (TimeSpan start, TimeSpan end) ParseRange(string timecodeLine)
    {
        string[] parts = timecodeLine.Split("-->", StringSplitOptions.TrimEntries);
        return (TimecodeParser.Parse(parts[0]), TimecodeParser.Parse(parts[1]));
    }
}
