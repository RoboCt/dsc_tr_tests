using DscTrReconTool.Importer.Common.Exceptions;
using DscTrReconTool.Importer.Importers;
using DscTrReconTool.Importer.Interfaces;
using FluentAssertions;

namespace DscTrReconTool.Importer.Test.Importers
{
    public class ImporterFactoryTests
    {
        private const string reportType = "test_report";
        private class TestImporter : IImporter
        {
            public string _reportType => reportType;

            public Task<int> ImportAsync(string path)
            {
                throw new NotImplementedException();
            }
        }


        [Fact]
        public void Get_ShouldThrowImporterNotFoundException_WhenImporterNotFoundForGivenReportType()
        {
            // Arrange
            var reportType = "test_report";
            var sut = new ImporterFactory(Enumerable.Empty<IImporter>());

            // Act
            Action queryAction = () => sut.Get(reportType);

            // Assert
            queryAction.Should().Throw<ImporterNotFoundException>()
                .WithMessage($"Importer for given report type {reportType} not found.");
        }

        [Fact]
        public void Get_ShouldReturnCorrectImporter_WhenImporterFoundForGivenReporType()
        {
            // Arrange
            var sut = new ImporterFactory(new List<IImporter>() { new TestImporter() });

            // Act
            IImporter result = sut.Get(reportType);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<TestImporter>();
        }
    }
}
