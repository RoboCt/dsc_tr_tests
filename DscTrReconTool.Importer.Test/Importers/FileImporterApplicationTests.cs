using DscTrReconTool.Importer.Importers;
using DscTrReconTool.Importer.Interfaces;
using FluentAssertions;
using Moq;
using static DscTrReconTool.Importer.Common.Options.CommandLineOptions;

namespace DscTrReconTool.Importer.Test.Importers
{
    public class FileImporterApplicationTests
    {
        private readonly Mock<IImporterFactory> _importerFactoryMock = new();
        private readonly Mock<IImporter> _importerMock = new();

        private class TestOptions : IArgsOptions
        {
            public string Path { get; set; }
            public string ReportType { get; set; }
        }
        
        [Fact]
        public async void ImportAsync_ShouldReturnZero_WhenFinishedWithSuccess()
        {
            // Arrange
            _importerFactoryMock.Setup(_ => _.Get(It.IsAny<string>())).Returns(_importerMock.Object);
            _importerMock.Setup(_ => _.ImportAsync(It.IsAny<string>())).ReturnsAsync(0);
            var sut = new FileImporterApplication<FileArgsOptions>(_importerFactoryMock.Object);

            // Act
            var result = await sut.ImportAsync(new FileArgsOptions());

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void ImportAsync_ShouldThrowArgumentNullException_WhenGivenArgsOptionsAreNull()
        {
            // Arrange
            var sut = new FileImporterApplication<FileArgsOptions>(_importerFactoryMock.Object);

            // Act
            var queryAction = async () => await sut.ImportAsync(null);

            // Assert
            queryAction.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public void ImportAsync_ShouldThrowInvalidCastException_WhenGivenArgsOptionsAreNotFileArgsOptionsType()
        {
            // Arrange
            var sut = new FileImporterApplication<FileArgsOptions>(_importerFactoryMock.Object);

            // Act
            var queryAction = async () => await sut.ImportAsync(new TestOptions());

            // Assert
            queryAction.Should().ThrowAsync<InvalidCastException>();
        }

        [Fact]
        public async Task ImportAsync_ShouldInvokeImporterFactoryGetMethodOnce()
        {
            // Arrange
            _importerFactoryMock.Setup(_ => _.Get(It.IsAny<string>())).Returns(_importerMock.Object);
            var sut = new FileImporterApplication<FileArgsOptions>(_importerFactoryMock.Object);

            // Act
            var result = await sut.ImportAsync(new FileArgsOptions());

            // Assert
            _importerFactoryMock.Verify(m => m.Get(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ImportAsync_ShouldInvokeImporterImportAsyncMethodOnce()
        {
            // Arrange
            _importerFactoryMock.Setup(_ => _.Get(It.IsAny<string>())).Returns(_importerMock.Object);
            var sut = new FileImporterApplication<FileArgsOptions>(_importerFactoryMock.Object);

            // Act
            var result = await sut.ImportAsync(new FileArgsOptions());

            // Assert
            _importerMock.Verify(m => m.ImportAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
