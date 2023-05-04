using DscTrReconTool.Importer.Common;
using FluentAssertions;

namespace DscTrReconTool.Importer.Test.Common
{
    public class ConsoleControllerTests
    {
        [Theory]
        [InlineData("test message 1")]
        [InlineData("test message 2")]
        public void WriteLine_ShouldOutputGivenTextToConsole(string message)
        {
            // Arrange
            var writer = new StringWriter();
            Console.SetOut(writer);

            var sut = new ConsoleController();

            // Act
            sut.WriteLine(message);

            // Assert
            var lines = writer.ToString().Split(Environment.NewLine, StringSplitOptions.TrimEntries);
            lines.Should().HaveCountGreaterThan(1);
            lines.Should().Contain(message);
        }
    }
}
