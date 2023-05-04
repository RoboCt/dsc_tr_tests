namespace DscTrReconTool.Importer.Interfaces
{
    internal interface IApplicationRunner
    {
        Task<int> RunAsync(string[] args);
    }
}
