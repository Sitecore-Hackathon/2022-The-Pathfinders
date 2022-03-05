using Sitecore.Security;
using Sitecore.Security.Accounts;
using System;
using System.Web.Security;

namespace Pathfinders.Foundation.CLI.Users.Service
{
    public class UserUpdateService
    {

        //From this: https://sitecore.stackexchange.com/questions/2671/update-user-password-programmatically
        public string ResetPassword(string userName)
        {
            var user = Membership.GetUser(userName);
            return user.ResetPassword();
        }
        public void UpdatePassword(string userName, string newPassword) 
        {
            var user = Membership.GetUser(userName);
            var oldPassword = user.ResetPassword();
            user.ChangePassword(oldPassword, newPassword);
        }
        //Many thanks to: http://sitecorecodesamples.blogspot.com/2010/11/programmatically-addedit-users.html
        public void AddUser(string domain, string firstName, string lastName, string email,
            string telephoneNumber = null, string jobTitle = null)
        {
            string userName = string.Concat(firstName, lastName);
            userName = string.Format(@"{0}\{1}", domain, userName);
            string newPassword = Membership.GeneratePassword(10, 3);
            try
            {
                if (!User.Exists(userName))
                {
                    Membership.CreateUser(userName, newPassword, email);

                    // Edit the profile information
                    User user = User.FromName(userName, true);
                    UserProfile userProfile = user.Profile;
                    userProfile.FullName = string.Format("{0} {1}", firstName, lastName);

                    // Assigning the user profile template
                    userProfile.SetPropertyValue("ProfileItemId", "{AE4C4969-5B7E-4B4E-9042-B2D8701CE214}");

                    // Telephone and Job Title are optional
                    if (telephoneNumber != null)
                    {
                        userProfile.SetCustomProperty("TelephoneNumber", telephoneNumber);
                    }
                    if (jobTitle != null)
                    {
                        userProfile.SetCustomProperty("JobTitle", jobTitle);
                    }
                    
                    userProfile.Save();
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error(string.Format("Error in Client.Project.Security.UserMaintenance (AddUser): Message: {0}; Source:{1}", ex.Message, ex.Source), this);
            }
        }
    }
}