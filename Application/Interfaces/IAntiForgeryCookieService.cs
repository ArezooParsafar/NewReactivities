using System.Collections.Generic;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface IAntiForgeryCookieService
    {
        void RegenerateAntiForgeryCookies(IEnumerable<Claim> claims);
        void DeleteAntiForgeryCookies();
    }
}
