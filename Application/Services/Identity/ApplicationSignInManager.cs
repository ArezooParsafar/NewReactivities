using Application.Interfaces;
using Domain.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Application.Services.Identity
{
    public class ApplicationSignInManager : SignInManager<AppUser>, IApplicationSignInManager
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ApplicationSignInManager(UserManager<AppUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<AppUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<AppUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<AppUser> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            _contextAccessor = contextAccessor;
        }



        #region BaseClass

        Task<bool> IApplicationSignInManager.IsLockedOut(AppUser user)
        {
            return base.IsLockedOut(user);
        }

        Task<SignInResult> IApplicationSignInManager.LockedOut(AppUser user)
        {
            return base.LockedOut(user);
        }

        Task<SignInResult> IApplicationSignInManager.PreSignInCheck(AppUser user)
        {
            return base.PreSignInCheck(user);
        }

        Task IApplicationSignInManager.ResetLockout(AppUser user)
        {
            return base.ResetLockout(user);
        }

        Task<SignInResult> IApplicationSignInManager.SignInOrTwoFactorAsync(AppUser user, bool isPersistent, string loginProvider, bool bypassTwoFactor)
        {
            return base.SignInOrTwoFactorAsync(user, isPersistent, loginProvider, bypassTwoFactor);
        }

        #endregion

        #region CustomMethods

        public bool IsCurrentUserSignedIn()
        {
            return IsSignedIn(_contextAccessor.HttpContext.User);
        }

        public Task<AppUser> ValidateCurrentUserSecurityStampAsync()
        {
            return ValidateSecurityStampAsync(_contextAccessor.HttpContext.User);
        }

        #endregion
    }
}
