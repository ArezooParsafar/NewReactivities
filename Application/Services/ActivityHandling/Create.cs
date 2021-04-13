using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ActivityHandling
{

    public class Create
    {

        public class Command : IRequest
        {
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
                RuleFor(x => x.Venue).NotEmpty();
                RuleFor(x => x.City).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.HoldingDate).NotEmpty();
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

                var photoUploadResult = _photoAccessor.AddPhoto(request.ImageFile);
                var currentUser = _userAccessor.GetUserId();

                var activity = new Activity
                {
                    Title = request.Title,
                    Description = request.Description,
                    Venue = request.Venue,
                    City = request.City,
                    CategoryId = request.CategoryId,
                    HoldingDate = request.HoldingDate,
                    Id = Guid.NewGuid(),
                    AppUserId = currentUser,
                    IsDeleted = false
                };

                activity.UserPhotos.Add(new UserPhoto
                {
                    AppUserId = currentUser,
                    ImageType = ImageType.Activity,
                    CreatedDate = DateTime.Now,
                    Path = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId,
                });

                activity.Attendees.Add(new Attendee
                {
                    DateJoined = DateTime.Now
                });

                _context.Activities.Add(activity);
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }


}
