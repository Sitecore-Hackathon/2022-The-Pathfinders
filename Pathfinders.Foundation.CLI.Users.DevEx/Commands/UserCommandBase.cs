using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System.CommandLine;

namespace Pathfinders.Foundation.CLI.Users.DevEx.Commands
{
    public class UserCommandBase : Command, ISubcommand
    {
        public UserCommandBase(string name, string description = null)
            : base(name, description)
        {
        }
    }
}
