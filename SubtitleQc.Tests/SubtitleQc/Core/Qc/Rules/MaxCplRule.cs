using System.Linq;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MaxCplRule : IQcRule
{
    private readonly int _threshold;

    public MaxCplRule(int threshold)
    {
        _threshold = threshold;
    }

    public string Name => "MaxCpl";

    public QcViolation? Evaluate(Cue cue, RuleContext context)
    {
        int maxLength = cue.Lines.Count == 0 ? 0 : cue.Lines.Max(line => line.Length);
        if (maxLength <= _threshold)
        {
            return null;
        }

        return new QcViolation(Name, $"Line length {maxLength} exceeds {_threshold}.");
    }
}
