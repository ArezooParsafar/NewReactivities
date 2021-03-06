using Domain.AuditableEntity;
using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Identity
{
    public class UserToken : IdentityUserToken<string>, IAuditableEntity
    {
        public int Id { get; set; }

        public string AccessTokenHash { get; set; }

        public DateTimeOffset AccessTokenExpiresDateTime { get; set; }

        public string RefreshTokenIdHash { get; set; }

        public string RefreshTokenIdHashSource { get; set; }

        public DateTimeOffset RefreshTokenExpiresDateTime { get; set; }
        public virtual AppUser User { get; set; }
    }
}
