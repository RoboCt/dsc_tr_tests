using DscTrReconTool.Importer.Interfaces;

namespace DscTrReconTool.Importer.Common
{
    internal class ConsoleController : IConsole
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
