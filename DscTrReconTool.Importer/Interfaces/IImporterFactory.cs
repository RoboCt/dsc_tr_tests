namespace DscTrReconTool.Importer.Interfaces
{
    internal interface IImporterFactory
    {
        IImporter Get(string reportType);
    }
}
