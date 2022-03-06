![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")
# Sitecore Hackathon 2022

- MUST READ: **[Submission requirements](SUBMISSION_REQUIREMENTS.md)**
- [Entry form template](ENTRYFORM.md)
- [Starter kit instructions](STARTERKIT_INSTRUCTIONS.md)
  

### ⟹ [Insert your documentation here](ENTRYFORM.md) <<

The purpose of this CLI extension was to get user information and perform common user tasks, such as resetting password, unlocking users, etc.
Unfortuantely, we were not able to finish. 

However, the plugin in 80% complete.  Once completed,  You'll need to register it:

add this key to your nuget.config:
'https://www.myget.org/F/waypath/api/v3/index.json'

then run:
'dotnet sitecore plugin add -n Pathfinders.Foundation.CLI.Users.DevEx'


This then enables the "user" command.

For example, we could run:
dotnet sitecore user getUser -u "sitecore\\admin" and it would return user data.
Other functions included, resetPassword, setPassword, unlockUser, getInfo, etc.
