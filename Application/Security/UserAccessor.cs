using Application.Interfaces;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Infrastructure.Utils
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public string GetUserId()
        {
            string userId = null;
            var userIdValue = _httpContextAccessor.HttpContext?.User?.Identity?.GetUserClaimValue(ClaimTypes.UserData);
            if (!string.IsNullOrWhiteSpace(userIdValue))
            {
                userId = userIdValue;
            }
            return userId;
        }

        public string GetUserName()
        {
            var username = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return username;
        }
    }
}
