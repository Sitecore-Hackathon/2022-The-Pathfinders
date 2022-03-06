using Pathfinders.Foundation.CLI.Users.DevEx.Tasks;
using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System;
using System.CommandLine;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Pathfinders.Foundation.CLI.Users.DevEx.Commands
{
    [ExcludeFromCodeCoverage]
    public class GetUserCommand : SubcommandBase<UserTask, UserArgs>
    {
        public GetUserCommand(IServiceProvider container)
          : base("getUser", "Gets a user", container)
        {
            ((Command)this).AddOption(ArgOptions.Config);
            ((Command)this).AddOption(ArgOptions.EnvironmentName);
            ((Command)this).AddOption(ArgOptions.UserName);
            ((Command)this).AddOption(ArgOptions.Verbose);
            ((Command)this).AddOption(ArgOptions.Trace);
        }

        protected override async Task<int> Handle(UserTask task, UserArgs args)
        {
            await task.Execute((UserTaskOptions)args).ConfigureAwait(false);
            return 0;
        }
    }
}
