using Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ActivityHandling
{
    public class Update
    {

        public class Command : IRequest
        {
            public Guid ActivityId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string City { get; set; }
            public string Venue { get; set; }
            public DateTime HoldingDate { get; set; }
            public int CategoryId { get; set; }
            public IFormFile ImageFile { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
            }
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
                var activity = await _context.Activities.FindAsync(request.ActivityId);
                if (activity == null)
                {
                    throw new ArgumentNullException("activity", "activity was not found");
                }

                if (activity.AppUserId != _userAccessor.GetUserId())
                {
                    throw new Exception("This user account does not have permission to delete the activity");
                }

                // this part has to change later
                if (request.ImageFile != null)
                {
                    var image = await _context.UserPhotos.SingleOrDefaultAsync(c => c.ActivityId == request.ActivityId);
                    if (image != null)
                    {
                        _photoAccessor.DeletePhoto(image.Id);
                        var uploadResult = _photoAccessor.UpdatePhoto(request.ImageFile, image.Id);
                    }

                }

                activity.Title = request.Title ?? activity.Title;
                activity.HoldingDate = request.HoldingDate;
                activity.CategoryId = request.CategoryId;
                activity.City = request.City ?? activity.City;
                activity.Venue = request.Venue ?? activity.Venue;
                activity.Description = request.Description ?? activity.Description;

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
