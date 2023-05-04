namespace DscTrReconTool.Importer.Common.Exceptions
{
    internal class ImporterApplicationNotFoundException : Exception
    {
        internal ImporterApplicationNotFoundException(Type optionsType)
            : base($"Importer Application for given options type {optionsType.Name} not found.") { }
    }
}
