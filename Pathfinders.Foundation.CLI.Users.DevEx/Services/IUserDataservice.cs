using Sitecore.DevEx.Configuration.Models;
using System.Threading.Tasks;

namespace Pathfinders.Foundation.CLI.Users.DevEx.Services
{
    public interface IUserDataservice
    {
        Task<string> GetUserData(EnvironmentConfiguration configuration, string name);
    }
}
