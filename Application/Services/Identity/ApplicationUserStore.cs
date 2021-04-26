using Application.Interfaces;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Persistence.Context;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.Identity
{
    public class ApplicationUserStore : UserStore<AppUser, Role, ApplicationDbContext, string, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>,
        IApplicationUserStore
    {
        public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber describer) : base(context, describer)
        {
        }

        #region BaseClass

        protected override UserClaim CreateUserClaim(AppUser user, Claim claim)
        {
            var userClaim = new UserClaim { UserId = user.Id };
            userClaim.InitializeFromClaim(claim);
            return userClaim;
        }

        protected override UserLogin CreateUserLogin(AppUser user, UserLoginInfo login)
        {
            return new UserLogin
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName
            };
        }

        protected override UserRole CreateUserRole(AppUser user, Role role)
        {
            return new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };
        }

        protected override UserToken CreateUserToken(AppUser user, string loginProvider, string name, string value)
        {
            return new UserToken
            {
                UserId = user.Id,
                LoginProvider = loginProvider,
                Name = name,
                Value = value
            };
        }

        Task IApplicationUserStore.AddUserTokenAsync(UserToken token)
        {
            return base.AddUserTokenAsync(token);
        }

        Task<Role> IApplicationUserStore.FindRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return base.FindRoleAsync(normalizedRoleName, cancellationToken);
        }

        Task<UserToken> IApplicationUserStore.FindTokenAsync(AppUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            return base.FindTokenAsync(user, loginProvider, name, cancellationToken);
        }

        Task<AppUser> IApplicationUserStore.FindUserAsync(string userId, CancellationToken cancellationToken)
        {
            return base.FindUserAsync(userId, cancellationToken);
        }

        Task<UserLogin> IApplicationUserStore.FindUserLoginAsync(string userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return base.FindUserLoginAsync(userId, loginProvider, providerKey, cancellationToken);
        }

        Task<UserLogin> IApplicationUserStore.FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return base.FindUserLoginAsync(loginProvider, providerKey, cancellationToken);
        }

        Task<UserRole> IApplicationUserStore.FindUserRoleAsync(string userId, string roleId, CancellationToken cancellationToken)
        {
            return base.FindUserRoleAsync(userId, roleId, cancellationToken);
        }

        Task IApplicationUserStore.RemoveUserTokenAsync(UserToken token)
        {
            return base.RemoveUserTokenAsync(token);
        }

        #endregion
    }
}
