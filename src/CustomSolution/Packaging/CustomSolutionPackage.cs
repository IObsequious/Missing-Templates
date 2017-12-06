using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Input;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.VisualStudio.Packaging
{
    [Guid(PackageGuid)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [PackageRegistration(AllowsBackgroundLoading = true, RegisterUsing = RegistrationMethod.CodeBase, UseManagedResourcesOnly = true)]
    public class CustomSolutionPackage : AsyncPackage
    {
        internal const string PackageGuid = "A93DDA80-8D73-400F-B274-3BCB18A34376";

        protected override async System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await base.InitializeAsync(cancellationToken, progress).ConfigureAwait(false);

            OleMenuCommandService service = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            CommandHandler handler = new CommandHandler();

            CommandRegistrar.RegisterCommands(service, handler);

        }
    }
}
