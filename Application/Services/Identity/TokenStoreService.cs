using Application.Interfaces;
using Domain.Settings;
using Microsoft.Extensions.Options;
using Persistence.Context;

namespace Application.Services.Identity
{
    public class TokenStoreService : ITokenStoreService
    {
        private readonly ISecurityService _securityService;
        private readonly ITokenFactoryService _tokenFactoryService;
        private readonly IOptionsSnapshot<BearerTokensOptions> _configuration;

        public TokenStoreService(ApplicationDbContext context,
            ISecurityService securityService,
            ITokenFactoryService tokenFactoryService,
            IOptionsSnapshot<BearerTokensOptions> configuration)
        {

            _securityService = securityService;
            _tokenFactoryService = tokenFactoryService;
            _configuration = configuration;
        }





    }
}
