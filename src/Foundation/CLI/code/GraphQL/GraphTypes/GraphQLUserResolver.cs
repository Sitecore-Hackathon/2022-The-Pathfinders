using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Security.Accounts;

namespace Pathfinders.Foundation.CLI.Users.GraphQL.GraphTypes
{
    public class GraphQLUserResolver
    {
        public virtual User GetUser(string username) => User.FromName(username, false);
    }
}