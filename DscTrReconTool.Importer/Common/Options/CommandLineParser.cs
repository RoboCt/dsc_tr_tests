using CommandLine;
using DscTrReconTool.Importer.Common.Exceptions;
using DscTrReconTool.Importer.Interfaces;
using static DscTrReconTool.Importer.Common.Options.CommandLineOptions;

namespace DscTrReconTool.Importer.Common.Options
{
    internal class CommandLineParser : ICommandLineParser
    {
        private const int _argsCountRequired = 3;
        public IArgsOptions Parse(string[] args)
        {
            if (args.Length != _argsCountRequired)
                throw new InvalidArgumentCountException(args.Length, _argsCountRequired);

            var options = Parser.Default.ParseArguments<FileArgsOptions, JsonArgsOptions>(args).Value
                ?? throw new ArgumentsOptionsNotFoundException(args);

            return (options as IArgsOptions)!;
        }
    }
}
