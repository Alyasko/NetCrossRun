using System;
using System.Diagnostics;
using System.Linq;

namespace NetCrossRun.Core
{
    public class CrossRun
    {
        private readonly CommandProcessor _commandProcessor = new CommandProcessor();
        
        public CrossRun(string command)
        {
            if (string.IsNullOrEmpty(command))
                throw new ArgumentException(nameof(command));

            var separated = _commandProcessor.SeparateCommandParams(command);
            Command = separated.Command;
            Parameters = separated.Parameters;
        }



        public Process Execute()
        {
            
            throw new NotImplementedException();
        }

        public string Command { get; private set; }
        public string Parameters { get; private set; }

        public bool HasParameters => !string.IsNullOrWhiteSpace(Parameters);
    }
}