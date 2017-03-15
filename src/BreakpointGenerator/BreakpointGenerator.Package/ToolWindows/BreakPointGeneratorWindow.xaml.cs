using System;
using System.Windows.Controls;
using Microsoft.ALMRangers.BreakpointGenerator.ViewModels;

namespace Microsoft.ALMRangers.BreakpointGenerator.ToolWindows
{
    /// <summary>
    /// Interaction logic for MyControl.xaml
    /// </summary>
    public partial class BreakPointGeneratorWindow : UserControl
    {
        private IServiceProvider _serviceProvider;


        public BreakPointGeneratorWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            DataContext = BreakpointGeneratorToolWindowViewModel.Instance;
        }

    }

}