// -----------------------------------------------------------------------
// <copyright file="CommandHandler.cs" company="Ollon, LLC">
//     Copyright © 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio.Input
{
    internal partial class CommandHandler
    {
        public override void OnExecuteCmdidNewSolution(object sender, EventArgs e)
        {
            IVsSolution solution = GlobalServices.Solution;
            IVsSolution4 solution4 = GlobalServices.Solution4;


        }
    }
}
