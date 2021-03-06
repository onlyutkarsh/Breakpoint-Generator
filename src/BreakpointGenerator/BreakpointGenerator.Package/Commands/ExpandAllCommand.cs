﻿// //———————————————————————
// // <copyright file="ExpandAllCommand.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  The menu command for "Expand All" action.
// // </summary>
// //———————————————————————

using System;
using System.ComponentModel.Design;
using Microsoft.ALMRangers.BreakpointGenerator.Commands.Base;
using Microsoft.ALMRangers.BreakpointGenerator.ViewModels;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.ALMRangers.BreakpointGenerator.Commands
{
    public class ExpandAllCommand : DynamicCommand
    {
        public ExpandAllCommand(IServiceProvider provider)
            : base(provider, OnExecute, new CommandID(PackageGuids.guidToolbar, (int) PackageIds.expandAllCommand))
        {
        }

        private static void OnExecute(object sender, EventArgs e)
        {
            BreakpointGeneratorToolWindowViewModel.Instance.ExpandExecute();
        }

        protected override bool CanExecute(OleMenuCommand command)
        {
            return true;
        }
    }
}