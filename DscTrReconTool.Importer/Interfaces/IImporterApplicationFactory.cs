namespace DscTrReconTool.Importer.Interfaces
{
    internal interface IImporterApplicationFactory
    {
        IImporterApplication Get(IArgsOptions options);
    }
}
