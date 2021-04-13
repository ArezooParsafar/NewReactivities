using Application.ViewModels.UserPhotoDto;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserPhotoHandling
{
    public class UserPhotoList
    {

        public class UserPhotoEnvelope
        {
            public List<UserPhotoItem> UserPhotos { get; set; }
            public int TotalCount { get; set; }
        }
        public class Query : IRequest<UserPhotoEnvelope>
        {
            public string UserId { get; set; }
            public int? Limit { get; }
            public int? Offset { get; }

            public Query(string userId, int? limit, int? offset)
            {
                UserId = userId;
                Limit = limit;
                Offset = offset;
            }
        }

        public class Handler : IRequestHandler<Query, UserPhotoEnvelope>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<UserPhotoEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.UserPhotos
                    .Where(c => c.AppUserId == request.UserId && (c.ImageType == ImageType.Activity || c.ImageType == ImageType.None))
                    .OrderBy(c => c.CreatedDate);

                var userPhotos = await queryable
                   .Skip(request.Offset ?? 0)
                   .Take(request.Limit ?? 6)
                   .Select(c => new UserPhoto
                   {
                       Id = c.Id,
                       Path = c.Path,
                       ImageType = c.ImageType
                   })
                   .ToListAsync();

                return new UserPhotoEnvelope
                {
                    UserPhotos = _mapper.Map<List<UserPhoto>, List<UserPhotoItem>>(userPhotos),
                    TotalCount = queryable.Count()
                };
            }
        }
    }
}
