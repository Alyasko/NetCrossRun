using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace NetCrossRun.Core
{
    public class CrossRun
    {
        private readonly CommandProcessor _commandProcessor = new CommandProcessor();
        private readonly string _rawCommand;

        public CrossRun(string command)
        {
            if (string.IsNullOrEmpty(command))
                throw new ArgumentException(nameof(command));

            var separated = _commandProcessor.SeparateCommandParams(command);
            Command = separated.Command;
            Arguments = separated.Arguments;
            _rawCommand = command;
        }

        public Process Execute()
        {
            var psi = CreateStartupInfo();
            var p = Process.Start(psi);

            return p;
        }

        private ProcessStartInfo CreateStartupInfo()
        {
            var si = ProcessStartInfoInitializer ?? new ProcessStartInfo()
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardOutput = EnableAllRedirects,
                RedirectStandardError = EnableAllRedirects,
                RedirectStandardInput = EnableAllRedirects,
                WorkingDirectory = WorkingDirectory ?? ""
            };

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                    si.FileName = "/bin/bash";
                    si.Arguments = $"-c \"{_rawCommand}\"";
                    break;
                case PlatformID.Win32NT:
                    si.FileName = "cmd";
                    si.Arguments = "/C " + _rawCommand;
                    break;
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return si;
        }

        public ProcessStartInfo ProcessStartInfoInitializer { get; set; }
        public bool EnableAllRedirects { get; set; }
        public string WorkingDirectory { get; set; }

        public string Command { get; private set; }
        public string Arguments { get; private set; }

        public bool HasArguments => !string.IsNullOrWhiteSpace(Arguments);
    }
}