using System;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MinDurationRule : IQcRule
{
    private readonly TimeSpan _threshold;

    public MinDurationRule(TimeSpan threshold)
    {
        _threshold = threshold;
    }

    public string Name => "MinDuration";

    public QcViolation? Evaluate(Cue cue, RuleContext context)
    {
        if (cue.Duration >= _threshold)
        {
            return null;
        }

        return new QcViolation(Name, $"Duration {cue.Duration} is below minimum {_threshold}.");
    }
}
