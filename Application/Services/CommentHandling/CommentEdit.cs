using Application.Interfaces;
using MediatR;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.CommentHandling
{
    public class CommentEdit
    {

        public class Command : IRequest
        {
            public Guid CommentId { get; set; }
            public string Body { get; set; }
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

                var comment = await _context.Comments.FindAsync(request.CommentId);
                if (comment.AppUserId != _userAccessor.GetUserId())
                {
                    throw new Exception("You do not have the permission to edit the comment");
                }

                comment.Body = request.Body;
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
