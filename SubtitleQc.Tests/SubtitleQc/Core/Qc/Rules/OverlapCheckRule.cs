using System.Linq;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class OverlapCheckRule : IQcRule
{
    public string Name => "OverlapCheck";

    public QcViolation? Evaluate(Cue cue, RuleContext context)
    {
        bool overlapsPrevious = context.Cues
            .Where(other => other.Start <= cue.Start && other.Id != cue.Id)
            .Any(other => other.End > cue.Start);

        if (!overlapsPrevious)
        {
            return null;
        }

        return new QcViolation(Name, "Cue overlaps with a previous cue.");
    }
}
