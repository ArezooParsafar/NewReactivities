using Application.Interfaces;
using MediatR;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserPhotoHandling
{
  public  class DeletePhoto
    {

        public class Command : IRequest
        {
            public string UserPhotoId { get; set; }
        }



        public class Handler : IRequestHandler<Command>
        {
            private readonly ApplicationDbContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IPhotoAccessor _photoAccessor;

            public Handler(ApplicationDbContext context, IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
                _photoAccessor = photoAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUserId = _userAccessor.GetUserId();
                var userPhoto = await _context.UserPhotos.FindAsync(request.UserPhotoId);
                if (userPhoto == null)
                {
                    throw new Exception("The photo was not found.");
                }

                if (userPhoto.AppUserId != currentUserId)
                {
                    throw new Exception("You do not have permission to delete this photo.");
                }

                if (_photoAccessor.DeletePhoto(userPhoto.Id) == null)
                {
                    throw new Exception("Problem deleting photo.");
                }

                _context.UserPhotos.Remove(userPhoto);
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
