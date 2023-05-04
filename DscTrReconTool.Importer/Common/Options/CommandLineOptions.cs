using CommandLine;
using DscTrReconTool.Importer.Interfaces;

namespace DscTrReconTool.Importer.Common.Options
{
    internal class CommandLineOptions
    {
        [Verb("file", isDefault: false, HelpText = "Parse single file.")]
        public class FileArgsOptions : IArgsOptions
        {
            //[Value(0)]
            //public string ImporterType { get; set; } = string.Empty;

            [Value(0)]
            public string Path { get; set; } = string.Empty;
            
            [Value(1)]
            public string ReportType { get; set; } = string.Empty;
        }

        [Verb("json", isDefault: false, HelpText = "Parse json file.")]
        public class JsonArgsOptions : IArgsOptions
        {
            [Value(0)]
            public string Path { get; set; } = string.Empty;

            [Value(1)]
            public string ReportType { get; set; } = string.Empty;
        }
    }
}
