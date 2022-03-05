using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using Pathfinders.Foundation.CLI.Users.GraphQL.GraphTypes;
using Sitecore.Configuration;
using Sitecore.DependencyInjection;
using Sitecore.Services.GraphQL.Content.GraphTypes;
using Sitecore.Services.GraphQL.Content.Mutations;
using Sitecore.Services.GraphQL.Hosting;

namespace Pathfinders.Foundation.CLI.Users.DI
{
    public class DependencyConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<GraphQLUserResolver>();
        }
    }
}