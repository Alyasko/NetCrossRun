using System;
using NetCrossRun.Core;
using Xunit;

namespace NetCrossRun.Tests
{
    public class CommandProcessorTests
    {
        [Theory]
        [InlineData("dotnet run", "dotnet", "run")]
        [InlineData("dotnet app.dll test 1 2 3", "dotnet", "app.dll test 1 2 3")]
        [InlineData("  dotnet  run  ", "dotnet", "run")]
        [InlineData("  dotnet   ", "dotnet", null)]
        [InlineData("1 2         ", "1", "2")]
        public void SeparateCommandParams_CorrectCommands_ReturnCommandAndParameters(string rawCommand, string expExec, string expParams)
        {
            // Arrange.
            var processor = new CommandProcessor();

            // Act.
            var result = processor.SeparateCommandParams(rawCommand);

            // Assert.
            Assert.Equal(expExec, result.Command);
            Assert.Equal(expParams, result.Arguments);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData(null)]
        public void SeparateCommandParams_IncorrectCommands_Throw(string rawCommand)
        {
            // Arrange.
            var processor = new CommandProcessor();

            // Act.
            var action = new Action(() =>
            {
                processor.SeparateCommandParams(rawCommand);
            });
           
            // Assert.
            Assert.ThrowsAny<Exception>(action);
        }
    }
}