using DscTrReconTool.Importer.Common.Exceptions;
using DscTrReconTool.Importer.Interfaces;

namespace DscTrReconTool.Importer.Importers
{
    internal class ImporterFactory : IImporterFactory
    {
        private readonly Dictionary<string, IImporter> _importers = new();

        public ImporterFactory(IEnumerable<IImporter> importers)
        {
            _importers = importers.ToDictionary(_ => _._reportType, _ => _);
        }

        public IImporter Get(string reportType)
        {
            _importers.TryGetValue(reportType, out var importer);

            if (importer is null)
                throw new ImporterNotFoundException(reportType);

            return importer;
        }
    }

}
