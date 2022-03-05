using Sitecore.DevEx.Client.Tasks;

namespace Pathfinders.Foundation.CLI.Users.DevEx.Tasks
{
    public class UserTaskOptions : TaskOptionsBase
    {
        public string Config { get; set; }
        public string EnvironmentName { get; set; }
        public string UserName { get; set; }

        public override void Validate()
        {
            this.Require("Config");
            this.Default("EnvironmentName", (object)"default");
            this.Require("UserName");
        }
    }
}
