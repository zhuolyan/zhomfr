using Zhomfr.ReadmeGeneratorTool;
using Zhomfr.ReadmeGeneratorTool.Helpers;

AbstractTask[] tasks = [new ScalarType()];

Task.WaitAll([.. tasks.Select(task => task.Run())]);
