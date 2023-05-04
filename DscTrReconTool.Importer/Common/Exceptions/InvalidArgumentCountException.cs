namespace DscTrReconTool.Importer.Common.Exceptions
{
    internal class InvalidArgumentCountException : Exception
    {
        internal InvalidArgumentCountException(int argsCount, int expected)
            : base($"Arguments count should be equal {expected} but {argsCount} provided.")
        {

        }
    }
}
