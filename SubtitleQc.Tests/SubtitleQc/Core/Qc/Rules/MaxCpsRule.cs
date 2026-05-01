using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MaxCpsRule : IQcRule
{
    private readonly double _threshold;

    public MaxCpsRule(double threshold)
    {
        _threshold = threshold;
    }

    public string Name => "MaxCps";

    public QcViolation? Evaluate(Cue cue, RuleContext context)
    {
        double durationSeconds = cue.Duration.TotalSeconds;
        if (durationSeconds <= 0)
        {
            return new QcViolation(Name, "Cue duration must be greater than zero.");
        }

        double cps = cue.CharacterCount / durationSeconds;
        return cps > _threshold ? new QcViolation(Name, $"CPS {cps:F2} exceeds {_threshold:F2}.") : null;
    }
}
