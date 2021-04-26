using Application.Interfaces;
using Application.Util;
using MediatR;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ActivityHandling
{
    public class Delete
    {

        public class Command : IRequest<Result<Unit>>
        {
            public Guid ActivityId { get; set; }
        }


        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(ApplicationDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.ActivityId);
                if (activity == null)
                {
                    return Result<Unit>.Failure("The activtiy was not found.");
                }

                if (activity.AppUserId != _userAccessor.GetUserId())
                {
                    return Result<Unit>.Failure("This user account does not have permission to delete the activity.");
                }

                activity.IsDeleted = true;

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Result<Unit>.Success(Unit.Value);
                return Result<Unit>.Failure("Problem saving changes");

            }
        }
    }
}
