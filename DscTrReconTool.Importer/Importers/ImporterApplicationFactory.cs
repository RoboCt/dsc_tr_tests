using DscTrReconTool.Importer.Common.Exceptions;
using DscTrReconTool.Importer.Interfaces;

namespace DscTrReconTool.Importer.Importers
{
    internal class ImporterApplicationFactory : IImporterApplicationFactory
    {
        private readonly Dictionary<Type, IImporterApplication> _applications = new Dictionary<Type, IImporterApplication>();

        public ImporterApplicationFactory(IEnumerable<IImporterApplication> applications)
        {
            _applications = applications.Where(_ => _.GetType().GenericTypeArguments.Any(g => g.IsAssignableTo(typeof(IArgsOptions))))
                .ToDictionary(_ => _.GetType().GenericTypeArguments.First(g => g.IsAssignableTo(typeof(IArgsOptions))), _ => _);
        }

        public IImporterApplication Get(IArgsOptions options)
        {
            _applications.TryGetValue(options.GetType(), out var application);

            if (application is null)
                throw new ImporterApplicationNotFoundException(options.GetType());

            return application;
        }
    }
}
