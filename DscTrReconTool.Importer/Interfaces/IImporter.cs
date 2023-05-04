namespace DscTrReconTool.Importer.Interfaces
{
    internal interface IImporter
    {
        string _reportType { get; }
        Task<int> ImportAsync(string path);
    }
}
