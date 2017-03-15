﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.ALMRangers.BreakpointGenerator.Analyzer;
using EnvDTE;
using Microsoft.ALMRangers.BreakpointGenerator.Common;
using Microsoft.ALMRangers.BreakpointGenerator.ViewModels.Base;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ALMRangers.BreakpointGenerator.ViewModels
{
    public class BreakpointGeneratorToolWindowViewModel : ViewModelBase
    {
        private static readonly BreakpointGeneratorToolWindowViewModel _instance = new BreakpointGeneratorToolWindowViewModel();


        private ObservableCollection<TreeViewModel> tree = new ObservableCollection<TreeViewModel>();
        private Visibility isLoading;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static BreakpointGeneratorToolWindowViewModel()
        {
        }
        private BreakpointGeneratorToolWindowViewModel()
        {
        }

        public static BreakpointGeneratorToolWindowViewModel Instance
        {
            get { return _instance; }
        }

        public void ExpandExecute()
        {
            foreach (var node in Tree)
            {
                Expand(node);
            }
        }

        private void Expand(TreeViewModel node)
        {
            foreach (var child in node.Children)
            {
                Expand(child);
            }
            node.IsExpanded = true;

        }

        private bool CanExecute
        {
            get { return true; }
        }


        public void CollapseExecute()
        {
            foreach (var node in Tree)
            {
                Collapse(node);
            }
        }

        private void Collapse(TreeViewModel node)
        {
            foreach (var child in node.Children)
            {
                Collapse(child);
            }
            node.IsExpanded = false;

        }

        public ObservableCollection<TreeViewModel> Tree
        {
            get { return tree; }
            set
            {
                tree = value;
                OnPropertyChanged();
            }
        }

        public Visibility IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        public void Initialize()
        {
            var solution = PackageContext.Instance.ServiceProvider.GetService<SVsSolution, IVsSolution>();
            PackageContext.Instance.Solution = solution;
        }

        public void AnalyzeProject(DTE dte, string projectPath)
        {
            IsLoading = Visibility.Visible;
            Tree = null;
            Task.Run(() =>
            {
                var tree = CodeParser.GetPublicMethodsFromProject(projectPath).Result;
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    var treeviewModel = new TreeViewModel(dte, tree.Node);

                    CastToTreeViewModel(tree, treeviewModel);
                    if (tree.Children.Count > 0)
                    {
                        Tree = new ObservableCollection<TreeViewModel> {treeviewModel};
                    }

                    IsLoading = Visibility.Collapsed;
                }));
            });
        }

        public async void AnalyzeFile(DTE dte, string projectPath, string filePath)
        {
            IsLoading = Visibility.Visible;
            Tree = null;
            var publicMethods = await CodeParser.GetPublicMethodsFromFile(projectPath, filePath);
            var treeviewModel = new TreeViewModel(dte, publicMethods.Node);

            CastToTreeViewModel(publicMethods, treeviewModel);
            if (publicMethods.Children.Count > 0)
            {
                Tree = new ObservableCollection<TreeViewModel> { treeviewModel };
            }
            
            IsLoading = Visibility.Collapsed;

        }

        public void AnalyzeSolution(DTE dte, string solutionPath)
        {
            IsLoading = Visibility.Visible;
            Tree = null;

            Task.Run(() =>
            {
                var publicMethods = CodeParser.GetPublicMethodsFromSolution(solutionPath).Result;
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    var treeviewModel = new TreeViewModel(dte, publicMethods.Node);

                    CastToTreeViewModel(publicMethods, treeviewModel);
                    treeviewModel.IsExpanded = true;
                    if (publicMethods.Children.Count > 0)
                    {
                        Tree = new ObservableCollection<TreeViewModel> { treeviewModel };
                    }
                    IsLoading = Visibility.Collapsed;
                }));
            });
        }

        private void CastToTreeViewModel(Tree<TreeNode> modes, TreeViewModel root)
        {
            root.IsExpanded = true;
            root.Icon = VsShellHelper.GetIcon(modes.ItemType);
            foreach (var treeItem in modes)
            {
                foreach (var child in treeItem.Children)
                {
                    var childNode = root.AddChild(child.Node);
                    childNode.Icon = VsShellHelper.GetIcon(child.ItemType);
                    CastToTreeViewModel(child, childNode);
                    if (child.ItemType == ItemType.File)
                    {
                        childNode.IsExpanded = false;
                    }
                }
            }
        }
    }
}
