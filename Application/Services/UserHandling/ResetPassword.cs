using Application.Interfaces;
using Domain.Identity;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Application.Services.UserHandling
{
    public class ResetPassword
    {

        public class Command : IRequest
        {
            public string Email { get; set; }
            public string Code { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Email).NotEmpty().EmailAddress();
                RuleFor(c => c.Code).NotEmpty();
                RuleFor(c => c.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<AppUser> _userManager;

            public Handler(UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new Exception("Error");
                }

                var result = await _userManager.ResetPasswordAsync(user, request.Code, request.Password);

                if (result.Succeeded)
                    return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}




