using Pathfinders.Foundation.CLI.Users.DevEx.Tasks;
using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;

namespace Pathfinders.Foundation.CLI.Users.DevEx.Commands
{
    public class UserArgs : UserTaskOptions, IVerbosityArgs
    {
        public bool Verbose { get; set; }
        public bool Trace { get; set; }
    }
}
