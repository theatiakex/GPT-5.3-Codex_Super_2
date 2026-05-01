using System.Collections.Generic;
using SubtitleQc.Core.Models;

namespace SubtitleQc.Core.Qc;

public sealed class RuleContext
{
    public RuleContext(IReadOnlyList<Cue> cues)
    {
        Cues = cues;
    }

    public IReadOnlyList<Cue> Cues { get; }
}
