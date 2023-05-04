using DscTrReconTool.Importer.Interfaces;

namespace DscTrReconTool.Importer
{
    internal class ApplicationRunner : IApplicationRunner
    {
        private readonly IImporterApplicationFactory _importerApplicationFactory;
        private readonly IConsole _console;
        private readonly ICommandLineParser _commandLineParser;

        public ApplicationRunner(IImporterApplicationFactory importerApplicationFactory, IConsole console, ICommandLineParser commandLineParser)
        {
            _console = console ?? throw new ArgumentNullException(nameof(console));
            _commandLineParser = commandLineParser ?? throw new ArgumentNullException(nameof(commandLineParser));
            _importerApplicationFactory = importerApplicationFactory;
        }

        public async Task<int> RunAsync(string[] args)
        {
            try
            {
                var options = _commandLineParser.Parse(args);

                var importerApplication = _importerApplicationFactory.Get(options);

                await importerApplication.ImportAsync(options);
            } catch(Exception ex)
            {
                _console.WriteLine(ex.Message);
                return -1;
            }

            return 0;
        }
    }
}
