using Application.Interfaces;
using Domain.AuditableEntity;
using Domain.Identity;
using Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Identity
{
    public class UsedPasswordsService : IUsedPasswordsService
    {
        private readonly int _changePasswordReminderDays;
        private readonly int _notAllowedPreviouslyUsedPasswords;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly ApplicationDbContext _context;
        private readonly DbSet<UserUsedPassword> _userUsedPasswords;

        public UsedPasswordsService(
             IPasswordHasher<AppUser> passwordHasher,
            IOptionsSnapshot<SiteSettings> configurationRoot,
            ApplicationDbContext context)
        {
            _context = context;
            _userUsedPasswords = _context.Set<UserUsedPassword>() ?? throw new ArgumentNullException(nameof(_userUsedPasswords));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(_passwordHasher));
            if (configurationRoot == null) throw new ArgumentNullException(nameof(configurationRoot));
            var configurationRootValue = configurationRoot.Value;
            if (configurationRootValue == null) throw new ArgumentNullException(nameof(configurationRootValue));
            _notAllowedPreviouslyUsedPasswords = configurationRootValue.NotAllowedPreviouslyUsedPasswords;
            _changePasswordReminderDays = configurationRootValue.ChangePasswordReminderDays;
        }

        public async Task AddToUsedPasswordsListAsync(AppUser user)
        {
            await _userUsedPasswords.AddAsync(new UserUsedPassword
            {
                UserId = user.Id,
                HashedPassword = user.PasswordHash
            });
            await _context.SaveChangesAsync();
        }

        public async Task<DateTime?> GetLastUserPasswordChangeDateAsync(string userId)
        {
            var lastPasswordHistory =
                await _userUsedPasswords//.AsNoTracking() --> removes shadow properties
                                        .OrderByDescending(userUsedPassword => userUsedPassword.Id)
                                        .FirstOrDefaultAsync(userUsedPassword => userUsedPassword.UserId == userId);
            if (lastPasswordHistory == null)
            {
                return null;
            }

            var createdDateValue = _context.GetShadowPropertyValue(lastPasswordHistory, AuditableShadowProperties.CreatedDateTime);
            return createdDateValue == null ?
                      (DateTime?)null :
                      DateTime.SpecifyKind((DateTime)createdDateValue, DateTimeKind.Utc);
        }

        public async Task<bool> IsLastUserPasswordTooOldAsync(string userId)
        {
            var createdDateTime = await GetLastUserPasswordChangeDateAsync(userId);
            if (createdDateTime == null)
            {
                return false;
            }
            return createdDateTime.Value.AddDays(_changePasswordReminderDays) < DateTime.UtcNow;
        }

        /// <summary>
        /// This method will be used by CustomPasswordValidator automatically,
        /// every time a user wants to change his/her info.
        /// </summary>
        public async Task<bool> IsPreviouslyUsedPasswordAsync(AppUser user, string newPassword)
        {
            if (string.IsNullOrEmpty(user.Id))
            {
                // A new user wants to register at our site
                return false;
            }

            var userId = user.Id;
            var usedPasswords = await _userUsedPasswords
                                .AsNoTracking()
                                .Where(userUsedPassword => userUsedPassword.UserId == userId)
                                .OrderByDescending(userUsedPassword => userUsedPassword.Id)
                                .Select(userUsedPassword => userUsedPassword.HashedPassword)
                                .Take(_notAllowedPreviouslyUsedPasswords)
                                .ToListAsync();
            return usedPasswords.Any(hashedPassword => _passwordHasher.VerifyHashedPassword(user, hashedPassword, newPassword) != PasswordVerificationResult.Failed);
        }
    }
}
