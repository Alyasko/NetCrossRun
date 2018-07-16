using System;
using System.Diagnostics;
using NetCrossRun.Core;
using Xunit;

namespace NetCrossRun.Tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void ExecuteCommand_NormalCommand_CommandRun()
        {
            var p = "dotnet --version".ExecuteCommand();
            
            p.WaitForExit();

            var so = p.StandardOutput.ReadToEnd();
            Assert.True(char.IsDigit(so[0]));
        }
        
        [Fact]
        public void ExecuteCommand_NormalCommandWithoutOutputRedirect_ThrowsInvalidOpEx()
        {
            var p = "dotnet --version".ExecuteCommand(new ProcessStartInfo()
            {
                FileName = "Trying to override",
                Arguments = "Command and arguments"
            });
            p.WaitForExit();

            Assert.Throws<InvalidOperationException>(() => p.StandardOutput.ReadToEnd());
        }
    }
}