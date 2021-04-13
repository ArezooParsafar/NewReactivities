using Application.Interfaces;
using MediatR;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserFollowingHandling
{
    public class FollowUser
    {

        public class Command : IRequest
        {
            public string TargetAppUserId { get; set; }

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

                var target = await _context.Users.FindAsync(request.TargetAppUserId);
                var currentUserId = _userAccessor.GetUserId();

                _context.UserFollowings.Add(new Domain.Entities.UserFollowing
                {
                    IsAccepted = target.IsPublicProfile,
                    ObserverId = currentUserId,
                    TargetId = target.Id
                });
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
