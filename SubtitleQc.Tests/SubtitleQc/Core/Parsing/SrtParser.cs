using System;
using System.Collections.Generic;
using System.Linq;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Parsing.Abstractions;

namespace SubtitleQc.Core.Parsing;

public sealed class SrtParser : ISubtitleParser
{
    public SubtitleFormat Format => SubtitleFormat.Srt;

    public SubtitleDocument Parse(string rawSubtitleContent)
    {
        string normalized = rawSubtitleContent.Replace("\r\n", "\n");
        var blocks = normalized.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
        List<Cue> cues = blocks.Select(ParseCueBlock).Where(cue => cue != null).Cast<Cue>().ToList();
        return new SubtitleDocument(cues);
    }

    private Cue? ParseCueBlock(string block)
    {
        string[] lines = block.Split('\n', StringSplitOptions.None);
        if (lines.Length < 3)
        {
            return null;
        }

        string timecodeLine = lines[1];
        (TimeSpan start, TimeSpan end) = ParseRange(timecodeLine);
        IReadOnlyList<string> cueLines = lines.Skip(2).ToArray();
        return new Cue(Guid.NewGuid().ToString("N"), start, end, cueLines);
    }

    private (TimeSpan start, TimeSpan end) ParseRange(string timecodeLine)
    {
        string[] parts = timecodeLine.Split("-->", StringSplitOptions.TrimEntries);
        return (TimecodeParser.Parse(parts[0]), TimecodeParser.Parse(parts[1]));
    }
}
