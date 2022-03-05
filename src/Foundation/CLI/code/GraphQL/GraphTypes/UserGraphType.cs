using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Sitecore.Security.Accounts;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Services.GraphQL.Content.GraphTypes;


namespace Pathfinders.Foundation.CLI.Users.GraphQL.GraphTypes
{
    [DebuggerDisplay("{Name} (UserGraphType)")]
    public class UserGraphType : ObjectGraphType<User>
    {
        private readonly GraphQLUserResolver _userResolver;

        public UserGraphType()
            : this(ServiceProviderServiceExtensions.GetRequiredService<GraphQLUserResolver>(ServiceLocator.ServiceProvider))
        {
        }
        public UserGraphType(GraphQLUserResolver userResolver)
        {
            this._userResolver = userResolver;
            this.Name = nameof(UserGraphType);
            this.Field<StringGraphType>("localName", (string)null, (QueryArguments)null, new Func<ResolveFieldContext<User>, object>(this.ResolveLocalName), (string)null);
            this.Field<StringGraphType>("userName", (string)null, (QueryArguments)null, new Func<ResolveFieldContext<User>, object>(this.ResolveUserName), (string)null);
            this.Field<StringGraphType>("domain", (string)null, (QueryArguments)null, new Func<ResolveFieldContext<User>, object>(this.ResolveDomainName), (string)null);
            this.Field<BooleanGraphType>("isAdmin", (string)null, (QueryArguments)null, new Func<ResolveFieldContext<User>, object>(this.ResolveIsAdmin), (string)null);
            this.Field<StringGraphType>("roles", (string)null, (QueryArguments)null, new Func<ResolveFieldContext<User>, object>(this.ResolveRoles), (string)null);
        }

        private object ResolveLocalName(ResolveFieldContext<User> context) => (object)context.Source.LocalName;
        private object ResolveUserName(ResolveFieldContext<User> context) => (object)context.Source.Identity.Name;
        private object ResolveDomainName(ResolveFieldContext<User> context) => (object)context.Source.Domain;
        private object ResolveIsAdmin(ResolveFieldContext<User> context) => (object)context.Source.IsAdministrator;
        private object ResolveRoles(ResolveFieldContext<User> context) => (object)context.Source.Roles;


    }
}