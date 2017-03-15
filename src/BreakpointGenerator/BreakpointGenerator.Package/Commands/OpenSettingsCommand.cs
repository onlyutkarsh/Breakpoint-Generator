using System;
using System.ComponentModel.Design;
using Microsoft.ALMRangers.BreakpointGenerator.Commands.Base;
using Microsoft.ALMRangers.BreakpointGenerator.Options;

namespace Microsoft.ALMRangers.BreakpointGenerator.Commands
{
    public class OpenSettingsCommand : DynamicCommand
    {
        public OpenSettingsCommand(IServiceProvider provider) 
            : base(provider, OnExecute, new CommandID(PackageGuids.guidToolbar, PackageIds.optionsCommand))
        {
        }

        private static void OnExecute(object sender, EventArgs e)
        {
            BreakpointGeneratorPackage.ShowOptionPage(typeof(BreakpointGeneratorOptions));
        }
    }
}
