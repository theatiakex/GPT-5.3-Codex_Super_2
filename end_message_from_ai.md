### Superspec

Implemented a working .NET 10 prototype from agents_super.md + superspec.md with a unified internal model, decoupled parsers, and an extensible QC rule engine.

What I built
Added a full SubtitleQc.Core prototype under SubtitleQc.Tests/SubtitleQc/Core/ with strict separation of concerns:
Models: Cue, SubtitleDocument, ShotChangeData
QC abstractions: IQcRule, IShotChangeProvider
QC engine/reporting: RuleEngine, RuleContext, QcReport, QcResult, QcViolation, QcStatus
Rules: MaxLinesRule, MaxCplRule, MaxCpsRule, MinDurationRule, OverlapCheckRule, EmptyCueCheckRule, CrossShotBoundaryCheckRule, MinFramesFromShotChangeRule
Parsing: ISubtitleParser, SubtitleFormat, TimecodeParser, SrtParser, WebVttParser, TtmlParser
Architecture is format-agnostic at engine level (parsing and validation are fully decoupled), so adding rules or formats does not require changing core engine logic.
Kept one class per file and all internal structures JSON-serializable by design (plain immutable DTO-style classes).
Build/Test + telemetry requirements
dotnet test initially failed due missing central package versions in test dependencies.
Added Directory.Packages.props to provide package versions (without changing test logic).
Re-ran tests successfully:
Total: 19
Passed: 19
Failed: 0
Logged runs to test_metrics.jsonl per required schema:
Attempt 1: build failed
Attempt 2: 100% pass
Generated test_performance_report.md with Mermaid line chart after reaching 100%.
Files added
Directory.Packages.props
test_metrics.jsonl
test_performance_report.md
SubtitleQc.Tests/SubtitleQc/Core/... (all prototype source files)
If you want, I can next add a tiny console entrypoint demonstrating parsing + QC execution end-to-end on sample SRT/WebVTT/TTML inputs.