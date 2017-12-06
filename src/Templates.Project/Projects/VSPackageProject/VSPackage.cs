using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

namespace Microsoft.VisualStudio.Packaging
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true, RegisterUsing = RegistrationMethod.CodeBase)]
    [Guid(Strings.PackageGuid)]
    public sealed class $ExtensionName$Package : AsyncPackage
    {

        protected override async System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await base.InitializeAsync(cancellationToken, progress).ConfigureAwait(false);
        }
    }
}
