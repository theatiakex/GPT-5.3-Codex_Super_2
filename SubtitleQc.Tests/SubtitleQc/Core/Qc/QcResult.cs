using System.Collections.Generic;

namespace SubtitleQc.Core.Qc;

public sealed class QcResult
{
    public QcResult(string cueId, QcStatus status, IReadOnlyList<QcViolation> violations)
    {
        CueId = cueId;
        Status = status;
        Violations = violations;
    }

    public string CueId { get; }

    public QcStatus Status { get; }

    public IReadOnlyList<QcViolation> Violations { get; }
}
