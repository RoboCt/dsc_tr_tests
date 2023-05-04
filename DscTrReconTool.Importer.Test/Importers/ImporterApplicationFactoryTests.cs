using DscTrReconTool.Importer.Common.Exceptions;
using DscTrReconTool.Importer.Importers;
using DscTrReconTool.Importer.Interfaces;
using FluentAssertions;

namespace DscTrReconTool.Importer.Test.Importers
{
    public class ImporterApplicationFactoryTests
    {
        private class TestOptions : IArgsOptions
        {
            public string Path { get; set; }
            public string ReportType { get; set; }
        }

        private class TestImporterApplication<TestOptions> : IImporterApplication
        {
            public Task<int> ImportAsync(IArgsOptions options)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void Get_ShouldThrowImporterApplicationNotFoundException_WhenImporterApplicationNotFoundForGivenArgsOptions()
        {
            // Arrange
            var sut = new ImporterApplicationFactory(new List<IImporterApplication>());

            // Act
            Action queryAction = () => sut.Get(new TestOptions());

            // Assert
            queryAction.Should().Throw<ImporterApplicationNotFoundException>()
                .WithMessage($"Importer Application for given options type {typeof(TestOptions).Name} not found.");
        }

        [Fact]
        public void Get_ShouldReturnCorrectImporterApplication_WhenImporterApplicationFoundForGivenArgsOptions()
        {
            // Arrange
            var sut = new ImporterApplicationFactory(new List<IImporterApplication>() { new TestImporterApplication<TestOptions>() }); ;

            // Act
            IImporterApplication result = sut.Get(new TestOptions());

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TestImporterApplication<TestOptions>>();
        }
    }
}
