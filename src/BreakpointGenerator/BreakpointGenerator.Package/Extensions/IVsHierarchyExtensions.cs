using System;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ALMRangers.BreakpointGenerator.Extensions
{
    public static class IVsHierarchyExtensions
    {
        /// <summary>
        /// Converts a IVsHierarchy to a Project.
        /// </summary>
        /// <param name="hierarchy">The hierarchy.</param>
        /// <returns></returns>
        public static Project ToProject(this IVsHierarchy hierarchy)
        {
            object project = null;

            if (hierarchy.GetProperty(VSConstants.VSITEMID_ROOT, (int)__VSHPROPID.VSHPROPID_ExtObject, out project) < 0)
            {
                throw new ArgumentException("IVsHierarchy is not a project.");
            }

            return (Project)project;
        }
    }
}
