using Zhomfr.ReadmeGeneratorTool;
using Zhomfr.ReadmeGeneratorTool.Helpers;
using Zhomfr.ReadmeGeneratorTool.Validation;

AbstractTask[] tasks = [new ScalarType(), new Rules(), new Database()];

Task.WaitAll([.. tasks.Select(task => task.Run())]);
