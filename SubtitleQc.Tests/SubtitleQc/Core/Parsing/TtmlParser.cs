using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Parsing.Abstractions;

namespace SubtitleQc.Core.Parsing;

public sealed class TtmlParser : ISubtitleParser
{
    public SubtitleFormat Format => SubtitleFormat.Ttml;

    public SubtitleDocument Parse(string rawSubtitleContent)
    {
        XDocument document = XDocument.Parse(rawSubtitleContent);
        XNamespace ns = document.Root!.Name.Namespace;
        List<Cue> cues = document
            .Descendants(ns + "p")
            .Select(ParseCueElement)
            .ToList();
        return new SubtitleDocument(cues);
    }

    private Cue ParseCueElement(XElement paragraph)
    {
        string begin = paragraph.Attribute("begin")?.Value ?? "00:00:00.000";
        string end = paragraph.Attribute("end")?.Value ?? "00:00:00.000";
        IReadOnlyList<string> lines = paragraph.Value
            .Split('\n', StringSplitOptions.None)
            .Select(line => line.Trim())
            .ToArray();
        return new Cue(Guid.NewGuid().ToString("N"), TimecodeParser.Parse(begin), TimecodeParser.Parse(end), lines);
    }
}
