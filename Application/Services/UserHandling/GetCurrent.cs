using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.UserDto;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.UserHandling
{
    public class GetCurrent
    {
        public class Query : IRequest<UserItem>
        {
            public string Email { get; }
            public Query(string email)
            {
                this.Email = email;

            }

        }

        public class Handler : IRequestHandler<Query, UserItem>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly ITokenFactoryService _tokenFactoryService;
            public Handler(UserManager<AppUser> userManager, ITokenFactoryService tokenFactoryService)
            {
                this._tokenFactoryService = tokenFactoryService;
                this._userManager = userManager;

            }
            public async Task<UserItem> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                var token = _tokenFactoryService.CreateJwtTokens(user);
                return new UserItem
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    HeaderImage = null,
                    ProfileImage = null,
                    RefreshToken = token.RefreshToken,
                    Token = token.AccessToken,
                };
            }
        }
    }
}