using Microsoft.Extensions.Logging;
using Pathfinders.Foundation.CLI.Users.DevEx.Services;
using Sitecore.DevEx.Client.Logging;
using Sitecore.DevEx.Client.Tasks;
using Sitecore.DevEx.Configuration;
using Sitecore.DevEx.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinders.Foundation.CLI.Users.DevEx.Tasks
{
    public class UserTask
    {
        private readonly IRootConfigurationManager _rootConfigurationManager;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        private readonly IUserDataservice _userDataservice;

        public UserTask(
          IRootConfigurationManager rootConfigurationManager,
          ILoggerFactory loggerFactory,
          IUserDataservice userDataservice)
        {
            this._rootConfigurationManager = rootConfigurationManager ?? throw new ArgumentNullException(nameof(rootConfigurationManager));
            this._loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            this._logger = (ILogger)loggerFactory.CreateLogger<UserTask>();
            this._userDataservice = userDataservice;
        }

        public async Task Execute(UserTaskOptions options)
        {
            ((TaskOptionsBase)options).Validate();
            Stopwatch outerStopwatch = Stopwatch.StartNew();
            EnvironmentConfiguration environmentConfiguration;
            if (!(await this._rootConfigurationManager.ResolveRootConfiguration(options.Config)).Environments.TryGetValue(options.EnvironmentName, out environmentConfiguration))
                throw new InvalidConfigurationException("Environment " + options.EnvironmentName + " was not defined. Use the login command to define it.");
            await _userDataservice.GetUserData(environmentConfiguration, options.UserName);
            outerStopwatch.Stop();
            ColorLogExtensions.LogConsoleVerbose(this._logger, string.Empty, new ConsoleColor?(), new ConsoleColor?());
            ColorLogExtensions.LogConsoleVerbose(this._logger, string.Format("Publishing is finished in {0}ms.", (object)outerStopwatch.ElapsedMilliseconds), new ConsoleColor?(), new ConsoleColor?());
            outerStopwatch = (Stopwatch)null;
        }
    }
}
