using System;
using System.Windows.Markup;

namespace Microsoft.ALMRangers.BreakpointGenerator.Common
{
    public abstract class BaseConverter : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
