using Application.Interfaces;
using MediatR;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ActivityHandling
{
    public class Delete
    {

        public class Command : IRequest
        {
            public Guid ActivityId { get; set; }
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
                var activity = await _context.Activities.FindAsync(request.ActivityId);
                if (activity == null)
                {
                    throw new ArgumentNullException("activity", "activity was not found");
                }

                if (activity.AppUserId != _userAccessor.GetUserId())
                {
                    throw new Exception("This user account does not have permission to delete the activity");
                }

                activity.IsDeleted = true;

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
