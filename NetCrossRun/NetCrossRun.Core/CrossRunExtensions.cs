using System;
using System.Diagnostics;

namespace NetCrossRun.Core
{
    public static class CrossRunExtensions
    {
        public static Process ExecuteCommand(this string command)
        {
            var cr = new CrossRun(command);
            return cr.Execute();
        } 
        
        public static Process ExecuteCommand(this string command, bool enableAllRedirects)
        {
            var cr = new CrossRun(command);
            cr.EnableAllRedirects = enableAllRedirects;
            return cr.Execute();
        } 
        
        public static Process ExecuteCommand(this string command, bool enableAllRedirects, string workingDirectory)
        {
            if(string.IsNullOrWhiteSpace(workingDirectory))
                throw new ArgumentNullException(nameof(workingDirectory));
            
            var cr = new CrossRun(command);
            cr.EnableAllRedirects = enableAllRedirects;
            cr.WorkingDirectory = workingDirectory;
            return cr.Execute();
        } 
        
        public static Process ExecuteCommand(this string command, ProcessStartInfo psi)
        {
            if (psi == null)
                throw new ArgumentNullException(nameof(psi));
            
            var cr = new CrossRun(command)
            {
                ProcessStartInfoInitializer = psi
            };
            return cr.Execute();
        }
    }
}