using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GraphQL.Types;
using Pathfinders.Foundation.CLI.Users.GraphQL.GraphTypes;
using Sitecore.Data;
using Sitecore.Security.Accounts;
using Sitecore.Services.GraphQL.Content;
using Sitecore.Services.GraphQL.Schemas;
using Sitecore.Sites;

namespace Pathfinders.Foundation.CLI.Users.GraphQL.Queries
{
    public class UserQuery : RootFieldType<UserGraphType, User>, IContentSchemaRootFieldType
    {
        public UserQuery()
            : base("user", "Access to user information")
        {
            QueryArgument[] queryArgumentArray = new QueryArgument[1];
            QueryArgument<StringGraphType> queryArgumentName = new QueryArgument<StringGraphType>
            {
                Name = "userName",
                Description = "The username to retrieve",
                DefaultValue = (object)string.Empty
            };
            queryArgumentArray[0] = (QueryArgument)queryArgumentName;
            this.Arguments = new QueryArguments(queryArgumentArray);
        }

        public Database Database { get; set; }
        protected override User Resolve(ResolveFieldContext context)
        {
            string username = context.GetArgument<string>("userName");
            if (!string.IsNullOrEmpty(username) && User.Exists(username))
            {
                return User.FromName(username, false);
            }

            return null;
        }
    }
}