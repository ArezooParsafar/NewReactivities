using Application.Interfaces;
using Domain.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserHandling
{
    public class SendResetPasswordEmail
    {

        public class Command : IRequest
        {
            public string Email { get; set; }
        }


        public class Handler : IRequestHandler<Command>
        {
            private readonly IApplicationUserManager _userManager;
            private readonly IEmailSender _emailSender;
            private readonly IOptions<SiteSettings> _siteSettings;

            public Handler(IApplicationUserManager userManager, IEmailSender emailSender, IOptions<SiteSettings> siteSettings)
            {
                _userManager = userManager;
                _emailSender = emailSender;
                _siteSettings = siteSettings;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
                {
                    throw new Exception("Error");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _emailSender.SendEmailAsync(
                   email: request.Email,
                   subject: "Reset Email",
                   model: new
                   {
                       UserId = user.Id,
                       Token = code,
                       EmailSignature = _siteSettings.Value.Smtp.FromName,
                       MessageDateTime = DateTime.UtcNow
                   });

                return Unit.Value;
            }
        }
    }
}
