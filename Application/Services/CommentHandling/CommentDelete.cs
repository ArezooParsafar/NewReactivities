using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.CommentHandling
{
    public class CommentDelete
    {

        public class Command : IRequest
        {
            public Guid CommentId { get; set; }
            public Guid? ActivityId { get; set; }
            public string UserPhotoId { get; set; }
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
                string targetUserId;
                if (request.ActivityId != null)
                {
                    var activity = await _context.Activities.FirstOrDefaultAsync(x => x.Id == request.ActivityId);
                    if (activity == null)
                    {
                        throw new Exception("The activity was not found");
                    }

                    targetUserId = activity.AppUserId;

                }
                else if (!string.IsNullOrEmpty(request.UserPhotoId))
                {
                    var userPhoto = await _context.UserPhotos.FirstOrDefaultAsync(x => x.Id == request.UserPhotoId);
                    if (userPhoto == null)
                    {
                        throw new Exception("The photo was not found.");
                    }

                    targetUserId = userPhoto.AppUserId;
                }
                else
                {
                    throw new Exception();
                }

                var comment = await _context.Comments.FindAsync(request.CommentId);
                if (comment == null)
                {
                    throw new Exception("The comment was not found");
                }

                var senderUserId = comment.AppUserId;
                if (currentUserId != targetUserId || currentUserId != senderUserId)
                {
                    throw new Exception("You do not have permission to delete this comment");
                }

                _context.Comments.Remove(comment);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
