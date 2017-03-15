﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.ALMRangers.BreakpointGenerator.Analyzer;
using Microsoft.ALMRangers.BreakpointGenerator.Common;
using EnvDTE;
using EnvDTE80;
using Microsoft.ALMRangers.BreakpointGenerator.ViewModels.Base;

namespace Microsoft.ALMRangers.BreakpointGenerator.ViewModels
{
    public class TreeViewModel : ViewModelBase
    {
        private readonly DTE dte;
        bool? isChecked = false;
        public TreeViewModel Parent { get; set; }
        public List<TreeViewModel> Children { get; set; }

        public bool IsInitiallySelected { get; private set; }
        RelayCommand onInsertBreakpoint;
        private bool isExpanded;

        public TreeNode Node { get; set; }
        public TreeViewModel(DTE dte, TreeNode node)
        {
            this.dte = dte;
            Node = node;
            this.Children = new List<TreeViewModel>();


        }

        public bool CanBreakpointBeInserted
        {
            get { return true; }
        }

        public ICommand OnInsertBreakpointCommand
        {
            get
            {
                if (onInsertBreakpoint == null)
                {
                    onInsertBreakpoint = new RelayCommand(param => BreakpointInserted(),
                        param => CanBreakpointBeInserted);
                }
                return onInsertBreakpoint;
            }
        }

        private void BreakpointInserted()
        {

        }


        public TreeViewModel AddChild(TreeNode node)
        {
            TreeViewModel childNode = new TreeViewModel(dte, node) { Parent = this };
            this.Children.Add(childNode);
            return childNode;
        }

        public BitmapSource Icon { get; set; }

        public TreeViewModel()
        {
            Children = new List<TreeViewModel>();
        }

        /// <summary>
        /// Gets/sets the state of the associated UI toggle (ex. CheckBox).
        /// The return value is calculated based on the check state of all
        /// child FooViewModels.  Setting this property to true or false
        /// will set all children to the same check state, and setting it 
        /// to any value will cause the parent to verify its check state.
        /// </summary>
        public bool? IsChecked
        {
            get { return isChecked; }
            set { SetIsChecked(value, true, true); }
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged();
            }
        }

        private void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == isChecked)
                return;

            isChecked = value;

            if (Node.ItemType == ItemType.Method)
            {
                var method = Node as PublicMethodNode;
                Debugger debugger = dte.Debugger;

                if(method.Breakpoint == null)
                {
                    var nrBreakpoints = debugger.Breakpoints.Count;
                    debugger.Breakpoints.Add("", method.FilePath, method.LineNo, 1, "",
                        dbgBreakpointConditionType.dbgBreakpointConditionTypeWhenTrue,
                        "", "", 0);
                    var newBreakPoint = (Breakpoint2) (debugger.Breakpoints.Item(nrBreakpoints + 1));
                    newBreakPoint.Message = UserSettings.TracepointExpression;
                    newBreakPoint.BreakWhenHit = !UserSettings.ContinueExecution;
                    method.Breakpoint = newBreakPoint;
                }
                else
                {
                    try
                    {
                        method.Breakpoint.Delete();
                    }
                    catch (Exception)
                    {
                    }
                    method.Breakpoint = null;
                }   
            }

            if (updateChildren && isChecked.HasValue)
                Children.ForEach(c => c.SetIsChecked(isChecked, true, false));

            if (updateParent && Parent != null)
                Parent.VerifyCheckState();



            OnPropertyChanged("IsChecked");
        }

        void VerifyCheckState()
        {
            bool? state = null;
            for (int i = 0; i < Children.Count; ++i)
            {
                bool? current = Children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }
            SetIsChecked(state, false, true);
        }
    }
}
