using System;
using System.ComponentModel.Design;
using Microsoft.ALMRangers.BreakpointGenerator.Commands;
using Microsoft.VisualStudio.Shell;
using Microsoft.ALMRangers.BreakpointGenerator.Extensions;
using Microsoft.ALMRangers.BreakpointGenerator.Services;

namespace Microsoft.ALMRangers.BreakpointGenerator
{
    internal class CommandSet
    {
        private readonly IServiceProvider serviceProvider;
        readonly OleMenuCommandService menuCommandService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSet"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public CommandSet(IServiceProvider provider)
        {
            this.serviceProvider = provider;
            menuCommandService = serviceProvider.GetService<IMenuCommandService, OleMenuCommandService>();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            RegisterMenuCommands();
        }

        private void RegisterMenuCommands()
        {
            ICommandManagerService commandManager =
                this.serviceProvider.GetService<SCommandManagerService, ICommandManagerService>();

            OleMenuCommand breakpointGeneratorCommand = new ShowBreakpointGeneratorToolWindowCommand(serviceProvider);
            menuCommandService.AddCommand(breakpointGeneratorCommand);
            commandManager.RegisterCommand(breakpointGeneratorCommand);

            OleMenuCommand breakpointGeneratorViaDebugCommand = new ShowBreakpointGeneratorViaDebugCommand(serviceProvider);
            menuCommandService.AddCommand(breakpointGeneratorViaDebugCommand);
            commandManager.RegisterCommand(breakpointGeneratorViaDebugCommand);

            OleMenuCommand expandTreeCommand = new ExpandAllCommand(serviceProvider);
            menuCommandService.AddCommand(expandTreeCommand);
            commandManager.RegisterCommand(expandTreeCommand);

            OleMenuCommand collapseAllCommand = new CollapseAllCommand(serviceProvider);
            menuCommandService.AddCommand(collapseAllCommand);
            commandManager.RegisterCommand(collapseAllCommand);

            OleMenuCommand openSettingsCommand = new OpenSettingsCommand(serviceProvider);
            menuCommandService.AddCommand(openSettingsCommand);
            commandManager.RegisterCommand(openSettingsCommand);
        }
    }
}
