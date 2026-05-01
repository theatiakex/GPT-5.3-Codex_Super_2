using System.Linq;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class CrossShotBoundaryCheckRule : IQcRule
{
    private readonly IShotChangeProvider _shotChangeProvider;

    public CrossShotBoundaryCheckRule(IShotChangeProvider shotChangeProvider)
    {
        _shotChangeProvider = shotChangeProvider;
    }

    public string Name => "CrossShotBoundaryCheck";

    public QcViolation? Evaluate(Cue cue, RuleContext context)
    {
        bool crossesCut = _shotChangeProvider
            .GetShotChangeTimestamps()
            .Any(cut => cue.Start < cut && cut < cue.End);

        return crossesCut ? new QcViolation(Name, "Cue spans across a shot change.") : null;
    }
}
