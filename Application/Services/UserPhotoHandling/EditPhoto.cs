using Application.Interfaces;
using Application.ViewModels.UserPhotoDto;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserPhotoHandling
{
    public class EditPhoto
    {

        public class Command : IRequest<UserPhotoItem>
        {
            public string PhotoId { get; set; }
            public string Description { get; set; }
        }



        public class Handler : IRequestHandler<Command,UserPhotoItem>
        {
            private readonly ApplicationDbContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IUserAccessor userAccessor,IMapper mapper)
            {
                _context = context;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<UserPhotoItem> Handle(Command request, CancellationToken cancellationToken)
            {
                var photo = await _context.UserPhotos.FindAsync(request.PhotoId);
                if (photo == null)
                {
                    throw new Exception("The photo was not found.");
                }

                if (photo.AppUserId != _userAccessor.GetUserId())
                {
                    throw new Exception("You do not have the permission to edit the photo.");
                }

                photo.Description = request.Description;
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return _mapper.Map<UserPhoto, UserPhotoItem>(photo);

                throw new Exception("Problem saving changes");
            }
        }
    }
}
