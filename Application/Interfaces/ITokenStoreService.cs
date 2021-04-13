using Domain.Identity;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITokenStoreService
    {
        Task AddUserTokenAsync(UserToken userToken);
        Task AddUserTokenAsync(AppUser user, string refreshTokenSerial, string accessToken, string refreshTokenSourceSerial);
        Task<bool> IsValidTokenAsync(string accessToken, string userId);
        Task DeleteExpiredTokensAsync();
        Task<UserToken> FindTokenAsync(string refreshTokenValue);
        Task DeleteTokenAsync(string refreshTokenValue);
        Task DeleteTokensWithSameRefreshTokenSourceAsync(string refreshTokenIdHashSource);
        Task InvalidateUserTokensAsync(string userId);
        Task RevokeUserBearerTokensAsync(string userIdValue, string refreshTokenValue);
    }

}
