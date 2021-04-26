using Application.Interfaces;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Application.Services.UserHandling
{
    public class ValidatePassword
    {
        public class ValidatePasswordResult
        {
            public bool Success { get; set; }
            public string ErrorDescription { get; set; }
        }
        public class Query : IRequest<ValidatePasswordResult>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Query, ValidatePasswordResult>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IPasswordValidator<AppUser> _passwordValidator;

            public Handler(UserManager<AppUser> userManager
                , IPasswordValidator<AppUser> passwordValidator)
            {
                _userManager = userManager;
                _passwordValidator = passwordValidator;
            }

            public async Task<ValidatePasswordResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new Exception("The email is not valid.");
                }

                var result = await _passwordValidator.ValidateAsync(
               (UserManager<AppUser>)_userManager, user, request.Password);

                var errorDesc = result.Succeeded ? "" : string.Join("/n", result.Errors.Select(c => c.Description).ToList());
                return new ValidatePasswordResult
                {
                    ErrorDescription = errorDesc,
                    Success = result.Succeeded
                };

            }
        }
    }
}
