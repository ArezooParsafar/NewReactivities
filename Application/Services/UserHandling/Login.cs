using Application.Interfaces;
using Application.ViewModels.UserDto;
using Domain.Enums;
using Domain.Identity;
using Domain.Settings;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Persistence.Context;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserHandling
{
    public class Login
    {


        public class Query : IRequest<UserItem>
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, UserItem>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IOptions<SiteSettings> _siteSettings;
            private readonly ITokenFactoryService _tokenFactory;
            private readonly ITokenStoreService _tokenStoreService;
            private readonly ApplicationDbContext _context;

            public Handler(UserManager<AppUser> userManager
                , SignInManager<AppUser> signInManager
                , IOptions<SiteSettings> siteSettings
                , ITokenFactoryService tokenFactory
                , ITokenStoreService tokenStoreService
                , ApplicationDbContext context)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _siteSettings = siteSettings;
                _tokenFactory = tokenFactory;
                _tokenStoreService = tokenStoreService;
                _context = context;
            }

            public async Task<UserItem> Handle(Query request, CancellationToken cancellationToken)
            {

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new Exception("The username or password is wrong.");
                }

                if (!user.IsActive)
                {
                    throw new Exception("Your account has been deactivated.");
                }

                if (_siteSettings.Value.EnableEmailConfirmation &&
                    !await _userManager.IsEmailConfirmedAsync(user))
                {
                    throw new Exception("Please check your email and confirm the sent link.");
                }

                var result = await _signInManager.CheckPasswordSignInAsync(
                                        user,
                                        request.Password,
                                        request.RememberMe
                                       );
                if (result.Succeeded)
                {
                    var token = _tokenFactory.CreateJwtTokens(user);
                    return new UserItem
                    {
                        DisplayName = user.DisplayName,
                        Token = token.AccessToken,
                        RefreshToken = token.RefreshToken,
                        Username = user.UserName,
                        ProfileImage = null,//user.UserPhotos.FirstOrDefault(c => c.ImageType == ImageType.Profile)?.Path,
                        HeaderImage = null,//user.UserPhotos.FirstOrDefault(c => c.ImageType == ImageType.Header)?.Path
                    };

                }
                if (result.IsLockedOut)
                {
                    throw new Exception("This Account has been locked.");
                }

                if (result.IsNotAllowed)
                {
                    throw new Exception("!!!");
                }

                throw new Exception("You do not have permission.");
            }
        }
    }
}
