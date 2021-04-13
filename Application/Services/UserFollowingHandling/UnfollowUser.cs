using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserFollowingHandling
{
    public class UnfollowUser
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

                var currentUserId = _userAccessor.GetUserId();
                var userFollowingItem = await _context.UserFollowings.
                    FirstOrDefaultAsync(x => x.TargetId == request.TargetAppUserId && x.ObserverId == currentUserId);

                if (userFollowingItem == null)
                {
                    throw new Exception("The record was not found");
                }

                _context.UserFollowings.Remove(userFollowingItem);
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
