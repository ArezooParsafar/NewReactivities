using Application.Interfaces;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Persistence.Context;
using System.Security.Claims;

namespace Application.Services.Identity
{
    public class ApplicationRoleStore :
         RoleStore<Role, ApplicationDbContext, string, UserRole, RoleClaim>,
         IApplicationRoleStore
    {

        public ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber describer)
            : base(context, describer)
        {
        }


        #region BaseClass

        protected override RoleClaim CreateRoleClaim(Role role, Claim claim)
        {
            return new RoleClaim
            {
                RoleId = role.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };
        }

        #endregion

        #region CustomMethods

        #endregion
    }
}
