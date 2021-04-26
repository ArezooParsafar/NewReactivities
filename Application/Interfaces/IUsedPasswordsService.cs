using Domain.Identity;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsedPasswordsService
    {
        Task<bool> IsPreviouslyUsedPasswordAsync(AppUser user, string newPassword);
        Task AddToUsedPasswordsListAsync(AppUser user);
        Task<bool> IsLastUserPasswordTooOldAsync(string userId);
        Task<DateTime?> GetLastUserPasswordChangeDateAsync(string userId);
    }
}
