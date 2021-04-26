using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.AttendHandling
{
    public class AttendORSkipActivity
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
                var userId = _userAccessor.GetUserId();

                if (activity == null)
                {
                    throw new Exception("Activity was not found.");
                }


                var attendee = await _context.Attendees.FirstOrDefaultAsync(c => c.ActivityId == request.ActivityId && c.AppUserId == userId);
                if (attendee == null)
                {
                    _context.Attendees.Add(new Attendee
                    {
                        ActivityId = request.ActivityId,
                        AppUserId = _userAccessor.GetUserId(),
                        DateJoined = DateTime.Now
                    });
                }
                else
                {
                    var isHost = activity.AppUserId == userId;
                    if (isHost)
                    {
                        throw new Exception("You cannot remove yourself as host");
                    }

                    _context.Attendees.Remove(attendee);
                }

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
