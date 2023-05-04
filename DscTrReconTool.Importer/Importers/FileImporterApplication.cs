using DscTrReconTool.Importer.Interfaces;

namespace DscTrReconTool.Importer.Importers
{
    internal class FileImporterApplication<FileArgsOptions> : IImporterApplication
    {
        private readonly IImporterFactory _importerFactory;

        public FileImporterApplication(IImporterFactory importerFactory)
        {
            _importerFactory = importerFactory ?? throw new ArgumentNullException(nameof(importerFactory));
        }

        public async Task<int> ImportAsync(IArgsOptions options)
        {
            if(options is null)
                throw new ArgumentNullException(nameof(options));

            if(options is not FileArgsOptions)
                throw new InvalidCastException($"Options should be of type {nameof(FileArgsOptions)}");

            var importer = _importerFactory.Get(options.ReportType);

            var result = await importer.ImportAsync(options.Path);

            return result;
        }
    }
}
