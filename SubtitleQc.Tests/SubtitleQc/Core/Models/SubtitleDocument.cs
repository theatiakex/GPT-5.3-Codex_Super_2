using System.Collections.Generic;

namespace SubtitleQc.Core.Models;

public sealed class SubtitleDocument
{
    public SubtitleDocument(IReadOnlyList<Cue> cues, ShotChangeData? shotChanges = null)
    {
        Cues = cues;
        ShotChanges = shotChanges;
    }

    public IReadOnlyList<Cue> Cues { get; }

    public ShotChangeData? ShotChanges { get; }
}
