using Application.Security;
using Domain.Identity;
using System.Threading.Tasks;

namespace Application.Interfaces
{

    public interface ITokenFactoryService
    {
        string CreateToken(AppUser user);
        string GetRefreshTokenSerial(string refreshTokenValue);
        JwtTokensData CreateJwtTokens(AppUser user);
    }
}
