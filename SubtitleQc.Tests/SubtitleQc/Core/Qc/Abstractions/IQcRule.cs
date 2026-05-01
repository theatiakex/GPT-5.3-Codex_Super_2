using SubtitleQc.Core.Models;

namespace SubtitleQc.Core.Qc.Abstractions;

public interface IQcRule
{
    string Name { get; }

    QcViolation? Evaluate(Cue cue, RuleContext context);
}
