using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MaxLinesRule : IQcRule
{
    private readonly int _threshold;

    public MaxLinesRule(int threshold)
    {
        _threshold = threshold;
    }

    public string Name => "MaxLines";

    public QcViolation? Evaluate(Cue cue, RuleContext context)
    {
        if (cue.Lines.Count <= _threshold)
        {
            return null;
        }

        return new QcViolation(Name, $"Cue has {cue.Lines.Count} lines, max is {_threshold}.");
    }
}
