using DscTrReconTool.Importer.Interfaces;
using FluentAssertions;
using Moq;

namespace DscTrReconTool.Importer.Test
{
    public class ApplicationRunnerTests
    {
        private readonly Mock<IImporterApplicationFactory> _mockImporterApplicationFactory = new();
        private readonly Mock<IConsole> _mockConsole = new();
        private readonly Mock<ICommandLineParser> _mockCommandLineParser = new();
        private readonly Mock<IImporterApplication> _mockImporterApplication = new();

        [Fact]
        public async Task RunAsync_ShouldInvokeImporterApplicationFactoryGetMethodOnce()
        {
            // Arrange
            var sut = new ApplicationRunner(_mockImporterApplicationFactory.Object, _mockConsole.Object, _mockCommandLineParser.Object);

            // Act
            var result = await sut.RunAsync(It.IsAny<string[]>());

            // Assert
            _mockImporterApplicationFactory.Verify(m => m.Get(It.IsAny<IArgsOptions>()), Times.Once);
        }

        [Fact]
        public async Task RunAsync_ShouldReturnZero_WhenImporterApplicationFactoryRunWithSuccess()
        {
            // Arrange
            _mockImporterApplicationFactory.Setup(_ => _.Get(It.IsAny<IArgsOptions>())).Returns(_mockImporterApplication.Object);
            var sut = new ApplicationRunner(_mockImporterApplicationFactory.Object, _mockConsole.Object, _mockCommandLineParser.Object);

            // Act
            var result = await sut.RunAsync(It.IsAny<string[]>());

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public async Task RunAsync_ShouldInvokeIConsoleWriteLineMethodOnceWithExceptionMessageOnce_WhenIImporterApplicationFactoryThrowsException()
        {
            // Arrange
            var message = "Importer Exception";
            _mockImporterApplicationFactory.Setup(_ => _.Get(It.IsAny<IArgsOptions>())).Throws(new Exception(message));
            var sut = new ApplicationRunner(_mockImporterApplicationFactory.Object, _mockConsole.Object, _mockCommandLineParser.Object);

            // Act
            var result = await sut.RunAsync(It.IsAny<string[]>());

            // Assert
            _mockConsole.Verify(m => m.WriteLine(message), Times.Once);
        }

        [Fact]
        public async Task RunAsync_ShouldReturnNegativeOne_WhenIImporterApplicationFactoryThrowsException()
        {
            // Arrange
            var message = "Importer Exception";
            _mockImporterApplicationFactory.Setup(_ => _.Get(It.IsAny<IArgsOptions>())).Throws(new Exception(message));
            var sut = new ApplicationRunner(_mockImporterApplicationFactory.Object, _mockConsole.Object, _mockCommandLineParser.Object);

            // Act
            var result = await sut.RunAsync(It.IsAny<string[]>());

            // Assert
            result.Should().Be(-1);
        }

        [Fact]
        public async Task RunAsync_ShouldInvokeICommandLineParserParseMethodOnce()
        {
            // Arrange
            var sut = new ApplicationRunner(_mockImporterApplicationFactory.Object, _mockConsole.Object, _mockCommandLineParser.Object);

            // Act
            var result = await sut.RunAsync(It.IsAny<string[]>());

            // Assert
            _mockCommandLineParser.Verify(m => m.Parse(It.IsAny<string[]>()), Times.Once);
        }

        [Fact]
        public async Task RunAsync_ShouldInvokeIConsoleWriteLineMethodOnceWithExceptionMessageOnce_WhenICommandLineParserThrowsException()
        {
            // Arrange
            var message = "ICommandLineParser Exception";
            _mockCommandLineParser.Setup(_ => _.Parse(It.IsAny<string[]>())).Throws(new Exception(message));
            var sut = new ApplicationRunner(_mockImporterApplicationFactory.Object, _mockConsole.Object, _mockCommandLineParser.Object);

            // Act
            var result = await sut.RunAsync(It.IsAny<string[]>());

            // Assert
            _mockConsole.Verify(m => m.WriteLine(message), Times.Once);
        }

        [Fact]
        public async Task RunAsync_ShouldReturnNegativeOne_WhenICommandLineParserThrowsException()
        {
            // Arrange
            var message = "ICommandLineParser Exception";
            _mockCommandLineParser.Setup(_ => _.Parse(It.IsAny<string[]>())).Throws(new Exception(message));
            var sut = new ApplicationRunner(_mockImporterApplicationFactory.Object, _mockConsole.Object, _mockCommandLineParser.Object);

            // Act
            var result = await sut.RunAsync(It.IsAny<string[]>());

            // Assert
            result.Should().Be(-1);
        }

        [Theory]
        [InlineData(0, new string[] { "file", "test_path", "test_reportType" })]
        [InlineData(0, new string[] { "json", "test_path", "test_reportType" })]

        public async Task RunAsync_ShouldRunICommandLineParserParseMethod_WithArgsPassedToRunAsyncMethod(int ignoreParameter, string[] args)
        {
            // Arrange
            var sut = new ApplicationRunner(_mockImporterApplicationFactory.Object, _mockConsole.Object, _mockCommandLineParser.Object);

            // Act
            var result = await sut.RunAsync(args);

            // Assert
            _mockCommandLineParser.Verify(m => m.Parse(args), Times.Once);
        }

        [Fact]
        public async Task RunAsync_ShouldInvokeIConsoleWriteLineMethodOnceWithExceptionMessageOnce_WhenIImporterApplicationThrowsException()
        {
            // Arrange
            var message = "IImporterApplication Exception";
            _mockImporterApplication.Setup(_ => _.ImportAsync(It.IsAny<IArgsOptions>())).ThrowsAsync(new Exception(message));
            _mockImporterApplicationFactory.Setup(_ => _.Get(It.IsAny<IArgsOptions>())).Returns(_mockImporterApplication.Object);
            var sut = new ApplicationRunner(_mockImporterApplicationFactory.Object, _mockConsole.Object, _mockCommandLineParser.Object);

            // Act
            var result = await sut.RunAsync(It.IsAny<string[]>());

            // Assert
            _mockConsole.Verify(m => m.WriteLine(message), Times.Once);
        }

        [Fact]
        public async Task RunAsync_ShouldReturnNegativeOne_WhenIImporterApplicationThrowsException()
        {
            // Arrange
            var message = "IImporterApplication Exception";
            _mockImporterApplication.Setup(_ => _.ImportAsync(It.IsAny<IArgsOptions>())).ThrowsAsync(new Exception(message));
            _mockImporterApplicationFactory.Setup(_ => _.Get(It.IsAny<IArgsOptions>())).Returns(_mockImporterApplication.Object);
            var sut = new ApplicationRunner(_mockImporterApplicationFactory.Object, _mockConsole.Object, _mockCommandLineParser.Object);

            // Act
            var result = await sut.RunAsync(It.IsAny<string[]>());

            // Assert
            result.Should().Be(-1);
        }
    }
}
