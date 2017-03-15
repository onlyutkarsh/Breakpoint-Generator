using System.Runtime.InteropServices;
using Microsoft.ALMRangers.BreakpointGenerator.Options;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.ALMRangers.BreakpointGenerator.Services
{
    [Guid("A12C2602-DF73-44AA-B8E8-B262810DB729")]
    [ComVisible(true)]
    internal interface ICommandManagerService
    {
        /// <summary>
        /// Registers the command.
        /// </summary>
        /// <param name="command">The command.</param>
        void RegisterCommand(OleMenuCommand command);
        /// <summary>
        /// Uns the register command.
        /// </summary>
        /// <param name="command">The command.</param>
        void UnRegisterCommand(OleMenuCommand command);

        BreakpointGeneratorOptions BreakpointGeneratorOptions { get; set; }
    }
}