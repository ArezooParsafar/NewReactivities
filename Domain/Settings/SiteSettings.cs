using DNTCommon.Web.Core;
using Domain.Enums;
using Domain.Logger;
using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Settings
{
    public class SiteSettings
    {
        //public AdminUserSeed AdminUserSeed { get; set; }
        public BearerTokensOptions BearerTokensOptions { get; set; }
        public Logging Logging { get; set; }
        public SmtpConfig Smtp { get; set; }
        public bool EnableEmailConfirmation { get; set; }
        public TimeSpan EmailConfirmationTokenProviderLifespan { get; set; }
        public ActiveDatabase ActiveDatabase { get; set; }
        public DatabaseConnectionString ConnectionStrings { get; set; }
        public int NotAllowedPreviouslyUsedPasswords { get; set; }
        public int ChangePasswordReminderDays { get; set; }
        public PasswordOptions PasswordOptions { get; set; }
        public string UsersAvatarsFolder { get; set; }
        public string UserDefaultPhoto { get; set; }
        public string ContentSecurityPolicyErrorLogUri { get; set; }
        public CookieOptions CookieOptions { get; set; }
        public DataProtectionOptions DataProtectionOptions { get; set; }
        public LockoutOptions LockoutOptions { get; set; }
        public UserAvatarImageOptions UserAvatarImageOptions { get; set; }
        public string[] EmailsBanList { get; set; }
        public string[] PasswordsBanList { get; set; }

        public CloudinarySettings CloudinarySettings { get; set; }
    }
}
