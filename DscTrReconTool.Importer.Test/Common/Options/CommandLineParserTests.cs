using DscTrReconTool.Importer.Common.Exceptions;
using DscTrReconTool.Importer.Common.Options;
using DscTrReconTool.Importer.Interfaces;
using FluentAssertions;
using static DscTrReconTool.Importer.Common.Options.CommandLineOptions;

namespace DscTrReconTool.Importer.Test.Common.Options
{
    public class CommandLineParserTests
    {
        [Theory]
        [InlineData(typeof(FileArgsOptions), new string[] { "file", "test_path", "test_report_type" })]
        [InlineData(typeof(JsonArgsOptions), new string[] { "json", "test_path", "test_report_type" })]
        public void Parse_ShouldReturnCorrectOptionsObject_WhenCorrectCommandLineArgumentsAreProvided(Type expectedType, string[] args)
        {
            // Arrange
            var sut = new CommandLineParser();

            // Act
            var result = sut.Parse(args);

            // Assert
            result.Should().NotBeNull();
            Assert.IsAssignableFrom<IArgsOptions>(result);
            result.Should().BeAssignableTo(expectedType);
            result.Path.Should().Be(args[1]);
            result.ReportType.Should().Be(args[2]);
        }

        [Theory]
        [InlineData("json")]
        [InlineData("json", "test_arg")]
        [InlineData("json", "test_arg", "test_arg", "test_arg")]
        public void Parse_ShouldThrowInvalidArgumentCountException_WhenArgumentCountIsNotEqualExpected(params string[] args)
        {
            // Arrange
            var expectedArgsCount = 3;
            var sut = new CommandLineParser();

            // Act
            Action queryAction = () => sut.Parse(args);

            // Assert
            queryAction.Should().Throw<InvalidArgumentCountException>()
                .WithMessage($"Arguments count should be equal {expectedArgsCount} but {args.Length} provided.");
        }

        [Fact]
        public void Parse_ShouldThrowArgumentsOptionsNotFoundException_WhenOptionsAreNotFoundForGivenArguments()
        {
            // Arrange
            var args = new string[] { "non_existing", "test_path", "test_variant" };
            var sut = new CommandLineParser();

            // Act
            Action queryAction = () => sut.Parse(args);

            // Assert
            queryAction.Should().Throw<ArgumentsOptionsNotFoundException>()
                .WithMessage($"Options not found for arguments: {args}");
        }
    }
}
