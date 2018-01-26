using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.Input
{
    public static class Extensions
    {
        internal static bool IsCpsProject(this IVsHierarchy hierarchy)
        {
            Requires.NotNull(hierarchy, "hierarchy");
            return hierarchy.IsCapabilityMatch("CPS");
        }
    }
}
