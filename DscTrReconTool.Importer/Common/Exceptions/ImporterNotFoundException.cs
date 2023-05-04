namespace DscTrReconTool.Importer.Common.Exceptions
{
    internal class ImporterNotFoundException : Exception
    {
        public ImporterNotFoundException(string reportType)
            :base($"Importer for given report type {reportType} not found.") { }
    }
}
