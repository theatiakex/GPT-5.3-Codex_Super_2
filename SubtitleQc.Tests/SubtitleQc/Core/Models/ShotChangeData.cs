using System;
using System.Collections.Generic;

namespace SubtitleQc.Core.Models;

public sealed class ShotChangeData
{
    public ShotChangeData(IReadOnlyList<TimeSpan> cutTimestamps, IReadOnlyList<int> cutFrames)
    {
        CutTimestamps = cutTimestamps;
        CutFrames = cutFrames;
    }

    public IReadOnlyList<TimeSpan> CutTimestamps { get; }

    public IReadOnlyList<int> CutFrames { get; }
}
