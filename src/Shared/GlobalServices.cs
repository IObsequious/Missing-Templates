using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EnvDTE80;

using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio
{
    internal static class GlobalServices
    {
        public static TServiceInterface GetService<TService, TServiceInterface>()
        {
            return (TServiceInterface)Package.GetGlobalService(typeof(TService));
        }

        public static TServiceInterface GetComponentModelService<TServiceInterface>() where TServiceInterface : class
        {
            return ComponentModel.GetService<TServiceInterface>();
        }

        public static IComponentModel ComponentModel
        {
            get
            {
                return GetService<SComponentModel, IComponentModel>();
            }
        }

        public static DTE2 DTE2
        {
            get
            {
                return GetService<SDTE, DTE2>();
            }
        }


        public static EnvDTE.SelectedItem SelectedItem
        {
            get
            {
                return DTE2.SelectedItems.Item(1);
            }
        }

        public static IVsSolution Solution
        {
            get
            {
                return GetService<SVsSolution, IVsSolution>();
            }
        }

        public static IVsSolution2 Solution2
        {
            get
            {
                return GetService<SVsSolution, IVsSolution2>();
            }
        }

        public static IVsSolution3 Solution3
        {
            get
            {
                return GetService<SVsSolution, IVsSolution3>();
            }
        }

        public static IVsSolution4 Solution4
        {
            get
            {
                return GetService<SVsSolution, IVsSolution4>();
            }
        }

        public static IVsSolution5 Solution5
        {
            get
            {
                return GetService<SVsSolution, IVsSolution5>();
            }
        }
    }
}
