using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.CommentHandling
{
    public class CommentCreate
    {

        public class Command : IRequest
        {
            public Guid? ActivityId { get; set; }
            public string UserPhotoId { get; set; }
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

                var comment = new Comment
                {
                    Body = request.Body,
                    ActivityId = request.ActivityId,
                    UserPhotoId = request.UserPhotoId,
                    CreatedTime = DateTime.Now,
                    AppUserId = _userAccessor.GetUserId()
                };

                _context.Comments.Add(comment);
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
