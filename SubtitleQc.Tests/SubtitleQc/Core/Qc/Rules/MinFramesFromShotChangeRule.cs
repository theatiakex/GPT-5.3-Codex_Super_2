using System.Linq;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public sealed class MinFramesFromShotChangeRule : IQcRule
{
    private readonly IShotChangeProvider _shotChangeProvider;
    private readonly int _thresholdFrames;

    public MinFramesFromShotChangeRule(IShotChangeProvider shotChangeProvider, int thresholdFrames)
    {
        _shotChangeProvider = shotChangeProvider;
        _thresholdFrames = thresholdFrames;
    }

    public string Name => "MinFramesFromShotChange";

    public QcViolation? Evaluate(Cue cue, RuleContext context)
    {
        if (!cue.StartFrame.HasValue)
        {
            return null;
        }

        bool tooClose = _shotChangeProvider.GetShotChangeFrames().Any(frame => IsTooClose(frame, cue.StartFrame.Value));
        return tooClose ? new QcViolation(Name, "Cue starts too close to a shot change frame.") : null;
    }

    private bool IsTooClose(int cutFrame, int cueStartFrame)
    {
        int distance = cueStartFrame - cutFrame;
        return distance >= 0 && distance < _thresholdFrames;
    }
}
