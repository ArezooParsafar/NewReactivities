using Application.ViewModels.UserPhotoDto;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserPhotoHandling
{
    public class PhotoDetail
    {

        public class Query : IRequest<UserPhotoItem>
        {
            public string PhotoId { get; set; }
        }

        public class Handler : IRequestHandler<Query, UserPhotoItem>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<UserPhotoItem> Handle(Query request, CancellationToken cancellationToken)
            {
                var photoDetail = await _context.UserPhotos.Where(c => c.Id == request.PhotoId).Select(c => new UserPhoto
                {
                    Id = c.Id,
                    Description = c.Description,
                    CreatedDate = c.CreatedDate,
                    Path = c.Path,
                    ImageType = c.ImageType,
                }).Include(c => c.Activity)
                .SingleOrDefaultAsync();

                if (photoDetail == null)
                {
                    throw new Exception("Oops,The image was not found.");
                }

                return _mapper.Map<UserPhoto, UserPhotoItem>(photoDetail);
            }
        }
    }
}
