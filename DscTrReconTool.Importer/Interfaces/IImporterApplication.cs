namespace DscTrReconTool.Importer.Interfaces
{
    internal interface IImporterApplication
    {
        Task<int> ImportAsync(IArgsOptions options);
    }
}
