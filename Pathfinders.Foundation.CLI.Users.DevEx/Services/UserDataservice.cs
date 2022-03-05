using GraphQL.Common.Request;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sitecore.DevEx;
using Sitecore.DevEx.Configuration.Models;
using Sitecore.DevEx.Serialization.Client.Datasources.Sc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;



namespace Pathfinders.Foundation.CLI.Users.DevEx.Services
{
    internal class UserDataservice : IUserDataservice
    {
        private readonly Func<ISitecoreApiClient> _apiClientFactory;
        private readonly ILogger _logger;

        public UserDataservice(
        ILoggerFactory loggerFactory,
        IServiceProvider serviceProvider)
        {
            serviceProvider.ThrowIfNull<IServiceProvider>(nameof(serviceProvider));
            serviceProvider.ThrowIfNull<IServiceProvider>(nameof(loggerFactory));
            this._apiClientFactory = new Func<ISitecoreApiClient>(serviceProvider.GetRequiredService<ISitecoreApiClient>);
            this._logger = loggerFactory.CreateLogger<UserDataservice>();
        }

        public Task<string> GetUserData(EnvironmentConfiguration environmentConfig, string userName = "sitecore\\admin")
        {
            IDictionary<string, string> dictionary = (IDictionary<string, string>)new Dictionary<string, string>()
            {
                {
                    "$userName",
                    userName
                }
            };

            return this.CreateApiClient(environmentConfig).RunQuery<string>("/sitecore/api/graph/users", new GraphQLRequest()
            {
                Query = "\nquery{\n user(userName: \"$userName\")\n {userName\n domain\n}\n}",
                Variables = dictionary
            }, "user");
        }

        private ISitecoreApiClient CreateApiClient(EnvironmentConfiguration environmentConfig)
        {
            ISitecoreApiClient apiClient = this._apiClientFactory();
            apiClient.Endpoint = environmentConfig;
            return apiClient;
        }
    }
}
