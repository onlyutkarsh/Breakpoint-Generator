using System.Collections.Generic;
using System.Linq;
using Microsoft.ALMRangers.BreakpointGenerator.Options;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.ALMRangers.BreakpointGenerator.Services
{
    internal class CommandManagerService : ICommandManagerService, SCommandManagerService
    {
        private IList<OleMenuCommand> _registeredCommands;
        public CommandManagerService()
        {
            _registeredCommands = new List<OleMenuCommand>();
        }
        public void RegisterCommand(OleMenuCommand command)
        {
            if (_registeredCommands.SingleOrDefault(
                cmd => cmd.CommandID.Guid.Equals(command.CommandID.Guid) &&
                    cmd.CommandID.ID.Equals(command.CommandID.ID)) == null)
            {
                _registeredCommands.Add(command);
            }
        }

        public void UnRegisterCommand(OleMenuCommand command)
        {
            if (_registeredCommands.SingleOrDefault(
               cmd => cmd.CommandID.Guid.Equals(command.CommandID.Guid) &&
                   cmd.CommandID.ID.Equals(command.CommandID.ID)) != null)
            {
                _registeredCommands.Remove(command);
            }
        }

        public BreakpointGeneratorOptions BreakpointGeneratorOptions { get; set; }
    }
}
