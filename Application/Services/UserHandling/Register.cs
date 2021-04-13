using Application.Interfaces;
using Application.ViewModels.UserDto;
using Domain.Identity;
using FluentValidation;
using MediatR;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserHandling
{
    public class Register
    {

        public class Command : IRequest<UserItem>
        {
            public string DisplayName { get; set; }
            public string Username { get; set; }
            public string Bio { get; set; }
            public string Email { get; set; }
            public bool IsPublicProfile { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password);
            }
        }

        public class Handler : IRequestHandler<Command, UserItem>
        {
            private readonly IApplicationUserManager _userManager;
            private readonly ITokenFactoryService _tokenFactoryService;

            public Handler(IApplicationUserManager userManager, ITokenFactoryService tokenFactoryService)
            {
                _userManager = userManager;
                _tokenFactoryService = tokenFactoryService;
            }

            public async Task<UserItem> Handle(Command request, CancellationToken cancellationToken)
            {

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    throw new Exception("Email already exists.");
                }

                user = await _userManager.FindByNameAsync(request.Username);
                if (user != null)
                {
                    throw new Exception("Username already exists.");
                }

                var appuser = new AppUser
                {
                    DisplayName = request.DisplayName,
                    Bio = request.Bio,
                    Email = request.Email,
                    UserName = request.Username,
                    IsActive = true,
                    IsPublicProfile = request.IsPublicProfile,
                };

                var result = await _userManager.CreateAsync(appuser, request.Password);
                if (result.Succeeded)
                {
                    var token = _tokenFactoryService.CreateJwtTokens(user);
                    return new UserItem
                    {
                        DisplayName = request.DisplayName
                       ,
                        RefreshToken = token.RefreshToken
                       ,
                        Token = token.AccessToken
                       ,
                        Username = request.Username

                    };
                }



                throw new Exception("Problem saving changes");
            }
        }
    }
}
