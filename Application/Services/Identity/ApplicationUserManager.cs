using Application.Interfaces;
using DNTCommon.Web.Core;
using Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Identity
{
    public class ApplicationUserManager : UserManager<AppUser>, IApplicationUserManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUsedPasswordsService _usedPasswordsService;
        private readonly ApplicationDbContext _context;
        private readonly DbSet<AppUser> _users;
        private readonly DbSet<Role> _roles;
        private AppUser _currentUserInScope;


        public ApplicationUserManager(IUserStore<AppUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<AppUser> passwordHasher,
            IEnumerable<IUserValidator<AppUser>> userValidators,
            IEnumerable<IPasswordValidator<AppUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<AppUser>> logger,
            IHttpContextAccessor contextAccessor,
            IUsedPasswordsService usedPasswordsService,
            ApplicationDbContext context)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _contextAccessor = contextAccessor;
            _usedPasswordsService = usedPasswordsService;
            _context = context;
            _users = context.Set<AppUser>();
            _roles = context.Set<Role>();
        }

        #region BaseClass

        string IApplicationUserManager.CreateTwoFactorRecoveryCode()
        {
            return base.CreateTwoFactorRecoveryCode();
        }

        Task<PasswordVerificationResult> IApplicationUserManager.VerifyPasswordAsync(IUserPasswordStore<AppUser> store, AppUser user, string password)
        {
            return base.VerifyPasswordAsync(store, user, password);
        }

        public override async Task<IdentityResult> CreateAsync(AppUser user)
        {
            var result = await base.CreateAsync(user);
            if (result.Succeeded)
            {
                await _usedPasswordsService.AddToUsedPasswordsListAsync(user);
            }
            return result;
        }

        public override async Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
            var result = await base.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _usedPasswordsService.AddToUsedPasswordsListAsync(user);
            }
            return result;
        }

        public override async Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
        {
            var result = await base.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
            {
                await _usedPasswordsService.AddToUsedPasswordsListAsync(user);
            }
            return result;
        }

        public override async Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword)
        {
            var result = await base.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
            {
                await _usedPasswordsService.AddToUsedPasswordsListAsync(user);
            }
            return result;
        }

        #endregion

        #region CustomMethods

        public AppUser FindById(string userId)
        {
            return _users.Find(userId);
        }

        public Task<AppUser> FindByIdIncludeUserRolesAsync(string userId)
        {
            return _users.Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.Id == userId);
        }

        public Task<List<AppUser>> GetAllUsersAsync()
        {
            return Users.ToListAsync();
        }

        public AppUser GetCurrentUser()
        {
            if (_currentUserInScope != null)
            {
                return _currentUserInScope;
            }

            var currentUserId = GetCurrentUserId();
            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                return null;
            }

            var userId = currentUserId;
            return _currentUserInScope = FindById(userId);
        }

        public async Task<AppUser> GetCurrentUserAsync()
        {
            return _currentUserInScope ??
                (_currentUserInScope = await GetUserAsync(_contextAccessor.HttpContext.User));
        }

        public string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.Identity.GetUserId();
        }

        public int? CurrentUserId
        {
            get
            {
                var userId = _contextAccessor.HttpContext.User.Identity.GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    return null;
                }

                return !int.TryParse(userId, out int result) ? (int?)null : result;
            }
        }

        IPasswordHasher<AppUser> IApplicationUserManager.PasswordHasher { get => base.PasswordHasher; set => base.PasswordHasher = value; }

        IList<IUserValidator<AppUser>> IApplicationUserManager.UserValidators => base.UserValidators;

        IList<IPasswordValidator<AppUser>> IApplicationUserManager.PasswordValidators => base.PasswordValidators;

        IQueryable<AppUser> IApplicationUserManager.Users => base.Users;

        public string GetCurrentUserName()
        {
            return _contextAccessor.HttpContext.User.Identity.GetUserName();
        }

        public async Task<bool> HasPasswordAsync(string userId)
        {
            var user = await FindByIdAsync(userId.ToString());
            return user?.PasswordHash != null;
        }

        public async Task<bool> HasPhoneNumberAsync(string userId)
        {
            var user = await FindByIdAsync(userId.ToString());
            return user?.PhoneNumber != null;
        }

        public async Task<byte[]> GetEmailImageAsync(string userId)
        {
            if (userId == null)
                return "?".TextToImage(new TextToImageOptions());

            var user = await FindByIdAsync(userId);
            if (user == null)
                return "?".TextToImage(new TextToImageOptions());


            return user.Email.TextToImage(new TextToImageOptions());
        }


        public async Task<IdentityResult> UpdateUserAndSecurityStampAsync(string userId, Action<AppUser> action)
        {
            var user = await FindByIdIncludeUserRolesAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "کاربر مورد نظر یافت نشد."
                });
            }

            action(user);

            var result = await UpdateAsync(user);
            if (!result.Succeeded)
            {
                return result;
            }
            return await UpdateSecurityStampAsync(user);
        }

        public async Task<IdentityResult> AddOrUpdateUserRolesAsync(string userId, IList<string> selectedRoleIds, Action<AppUser> action = null)
        {
            var user = await FindByIdIncludeUserRolesAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "کاربر مورد نظر یافت نشد."
                });
            }

            var currentUserRoleIds = user.UserRoles.Select(x => x.RoleId).ToList();

            if (selectedRoleIds == null)
            {
                selectedRoleIds = new List<string>();
            }

            var newRolesToAdd = selectedRoleIds.Except(currentUserRoleIds).ToList();
            foreach (var roleId in newRolesToAdd)
            {
                user.UserRoles.Add(new UserRole { RoleId = roleId, UserId = user.Id });
            }

            var removedRoles = currentUserRoleIds.Except(selectedRoleIds).ToList();
            foreach (var roleId in removedRoles)
            {
                var userRole = user.UserRoles.SingleOrDefault(ur => ur.RoleId == roleId);
                if (userRole != null)
                {
                    user.UserRoles.Remove(userRole);
                }
            }

            action?.Invoke(user);

            var result = await UpdateAsync(user);
            if (!result.Succeeded)
            {
                return result;
            }
            return await UpdateSecurityStampAsync(user);
        }

        Task<IdentityResult> IApplicationUserManager.UpdatePasswordHash(AppUser user, string newPassword, bool validatePassword)
        {
            return base.UpdatePasswordHash(user, newPassword, validatePassword);
        }

        #endregion
    }
}
