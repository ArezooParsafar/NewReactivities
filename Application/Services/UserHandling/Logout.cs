using Application.Interfaces;
using MediatR;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserHandling
{
    public class Logout
    {

        public class Command : IRequest
        {
            public string RefreshToken { get; set; }
        }



        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDbContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IAntiForgeryCookieService _antiForgeryCookieService;
            private readonly ITokenStoreService _tokenStoreService;

            public Handler(ApplicationDbContext context
                , IUserAccessor userAccessor
                , IAntiForgeryCookieService antiForgeryCookieService
                , ITokenStoreService tokenStoreService)
            {
                _context = context;
                _userAccessor = userAccessor;
                _antiForgeryCookieService = antiForgeryCookieService;
                _tokenStoreService = tokenStoreService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                var userId = _userAccessor.GetUserId();
                await _tokenStoreService.RevokeUserBearerTokensAsync(userId, request.RefreshToken);

                var success = await _context.SaveChangesAsync() > 0;
                if (success)
                {
                    _antiForgeryCookieService.DeleteAntiForgeryCookies();
                    return Unit.Value;
                }

                throw new Exception("Problem saving changes");
            }
        }
    }
}
