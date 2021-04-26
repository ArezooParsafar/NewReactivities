using Application.Interfaces;
using Application.ViewModels.UserPhotoDto;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserPhotoHandling
{
    public class AddPhoto
    {

        public class Command : IRequest<UserPhotoItem>
        {
            public IFormFile File { get; set; }
            public string Description { get; set; }
            public ImageType ImageType { get; set; }

        }



        public class Handler : IRequestHandler<Command,UserPhotoItem>
        {
            private readonly ApplicationDbContext _context;
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IPhotoAccessor photoAccessor, IUserAccessor userAccessor,IMapper mapper)
            {
                _context = context;
                _photoAccessor = photoAccessor;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<UserPhotoItem> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUser = _userAccessor.GetUserId();
                var photoUploadResult = _photoAccessor.AddPhoto(request.File);
                var photoEntity = new Domain.Entities.UserPhoto
                {
                    AppUserId = currentUser,
                    CreatedDate = DateTime.Now,
                    Description = request.Description,
                    ImageType = request.ImageType,
                    Path = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId
                };
                _context.UserPhotos.Add(photoEntity);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return _mapper.Map<UserPhoto, UserPhotoItem>(photoEntity);

                throw new Exception("Problem saving changes");
            }
        }
    }
}
