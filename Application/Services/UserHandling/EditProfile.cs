using Application.Interfaces;
using MediatR;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserHandling
{
    public class EditProfile
    {

        public class Command : IRequest
        {
            public string AppUserId { get; set; }
            public string DisplayName { get; set; }
            public string Bio { get; set; }
            public bool IsPublicProfile { get; set; }
            public string PhoneNumber { get; set; }
        }


        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDbContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(ApplicationDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                var user = await _context.Users.FindAsync(request.AppUserId);
                if (user == null)
                {
                    throw new Exception("The user account was not found.");
                }

                user.DisplayName = request.DisplayName;
                user.Bio = request.Bio;
                user.IsPublicProfile = request.IsPublicProfile;
                user.PhoneNumber = request.PhoneNumber;

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
