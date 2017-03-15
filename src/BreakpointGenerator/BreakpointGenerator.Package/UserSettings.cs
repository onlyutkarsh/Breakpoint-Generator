using System;
using Microsoft.ALMRangers.BreakpointGenerator.Options;

namespace Microsoft.ALMRangers.BreakpointGenerator
{
    public static class UserSettings
    {

        private static void OnOptionsChanged(object sender, OptionsChangedEventArgs e)
        {
            
        }
        public static IServiceProvider ServiceProvider { get; set; }

        public static string TracepointExpression
        {
            get { return Options.LogMessaage; }
            set { Options.LogMessaage = value; }
        }

        public static bool ContinueExecution
        {
            get { return Options.ContinueExecution; }
            set { Options.ContinueExecution = value; }
        }

        public static BreakpointGeneratorOptions Options { get; set; }
    }
}