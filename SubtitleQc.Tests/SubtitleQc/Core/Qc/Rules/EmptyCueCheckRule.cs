using System.Linq;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class EmptyCueCheckRule : IQcRule
{
    public string Name => "EmptyCueCheck";

    public QcViolation? Evaluate(Cue cue, RuleContext context)
    {
        bool hasText = cue.Lines.Any(line => !string.IsNullOrWhiteSpace(line));
        return hasText ? null : new QcViolation(Name, "Cue has no text content.");
    }
}
