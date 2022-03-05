using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace Pathfinders.Foundation.CLI.Users.DevEx.Commands
{
    [ExcludeFromCodeCoverage]
    internal static class ArgOptions
    {
        internal static readonly Option Verbose = new Option(new string[2]
        {
      "--verbose",
      "-v"
        }, "Write some additional diagnostic and performance data")
        {
            Argument = (Argument)new Argument<bool>((Func<bool>)(() => false))
        };
        internal static readonly Option Trace = new Option(new string[2]
        {
      "--trace",
      "-t"
        }, "Write more additional diagnostic and performance data")
        {
            Argument = (Argument)new Argument<bool>((Func<bool>)(() => false))
        };
        internal static readonly Option UserName;

        static ArgOptions()
        {
            ArgOptions.UserName = new Option(new string[2]
            {
        "--user",
        "-u"
            }, "Sitecore UserName.");
        }
    }
}
