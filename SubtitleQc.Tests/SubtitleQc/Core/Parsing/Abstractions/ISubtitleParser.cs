using SubtitleQc.Core.Models;

namespace SubtitleQc.Core.Parsing.Abstractions;

public interface ISubtitleParser
{
    SubtitleFormat Format { get; }

    SubtitleDocument Parse(string rawSubtitleContent);
}
