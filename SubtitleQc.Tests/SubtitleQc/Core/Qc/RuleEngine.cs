using System.Collections.Generic;
using System.Linq;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc;

public sealed class RuleEngine
{
    private readonly IReadOnlyList<IQcRule> _rules;

    public RuleEngine(IReadOnlyList<IQcRule> rules)
    {
        _rules = rules;
    }

    public QcReport Evaluate(IReadOnlyList<Cue> cues)
    {
        var context = new RuleContext(cues);
        var results = cues.Select(cue => EvaluateCue(cue, context)).ToList();
        return new QcReport(results);
    }

    private QcResult EvaluateCue(Cue cue, RuleContext context)
    {
        List<QcViolation> violations = _rules
            .Select(rule => rule.Evaluate(cue, context))
            .OfType<QcViolation>()
            .ToList();
        QcStatus status = violations.Count == 0 ? QcStatus.Passed : QcStatus.Failed;
        return new QcResult(cue.Id, status, violations);
    }
}
