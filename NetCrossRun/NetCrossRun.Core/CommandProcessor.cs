using System;

namespace NetCrossRun.Core
{
    public class CommandProcessor
    {
        public (string Command, string Arguments) SeparateCommandParams(string sourceCommand)
        {
            var command = sourceCommand.Trim();

            if (string.IsNullOrWhiteSpace(command))
                throw new ArgumentException($"Argument '{nameof(sourceCommand)}' should be valid command.");

            var spaceIndex = command.IndexOf(' ');
            if (spaceIndex == -1)
                return (command, null);

            var executable = command.Substring(0, spaceIndex).Trim();
            var args = command.Replace(executable, "").Trim();
            return (executable, args);
        }
    }
}