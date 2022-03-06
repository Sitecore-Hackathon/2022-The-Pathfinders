using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Devex.Client.Cli.Extensibility;
using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System.Diagnostics.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.CommandLine;
using Pathfinders.Foundation.CLI.Users.DevEx.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pathfinders.Foundation.CLI.Users.DevEx.Services;
using Pathfinders.Foundation.CLI.Users.DevEx.Tasks;
using Sitecore.DevEx.Serialization.Client;
using Sitecore.DevEx.Serialization.Client.Datasources.Sc;

namespace Pathfinders.Foundation.CLI.Users.DevEx
{
    public class RegisterExtension : ISitecoreCliExtension
    {
       
        public IEnumerable<ISubcommand> AddCommands(IServiceProvider container) => (IEnumerable<ISubcommand>)new ISubcommand[1]
        {
             CreateUserCommand(container)
        };

        [ExcludeFromCodeCoverage]
        public void AddConfiguration(IConfigurationBuilder configBuilder)
        {
        }

        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<GetUserCommand>()
                .AddSingleton<UserTask>()
                .AddSingleton<UserCommandBase>()
                .AddSingleton<IUserDataservice, UserDataservice>()
                .AddSingleton<ISitecoreApiClient, SitecoreApiClient>()
                .AddSerialization()
                 .AddSingleton(z => z.GetService<ILoggerFactory>().CreateLogger<UserTask>());
        }
        private static ISubcommand CreateUserCommand(IServiceProvider container)
        {
            UserCommandBase command = new UserCommandBase("user", "Tools to gather user information");
            command.AddAlias("user");

            command.AddCommand((Command)container.GetRequiredService<GetUserCommand>());

            return command;
        }
    }
}