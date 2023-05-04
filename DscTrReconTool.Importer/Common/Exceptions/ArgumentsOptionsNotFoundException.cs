namespace DscTrReconTool.Importer.Common.Exceptions
{
    internal class ArgumentsOptionsNotFoundException : Exception
    {
        internal ArgumentsOptionsNotFoundException(string[] args)
            : base($"Options not found for arguments: {args}")
        {

        }
    }
}
