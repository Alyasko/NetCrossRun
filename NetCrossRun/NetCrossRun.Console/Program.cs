using System;

namespace NetCrossRun.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            var p = "dotnet --version".ExecuteCommand();
            Console.WriteLine(p.StandardOutput.ReadToEnd());
        }
    }
}