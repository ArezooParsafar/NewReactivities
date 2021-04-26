using Application.Util;
using Application.ViewModels.UserFollowingDto;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Services.UserFollowingHandling
{
    public class UserFollowingsList
    {
        public class UserFollowingsEnvelope
        {
            public List<UserFollowingItem> UserFollowingItems { get; set; }
            public int Count { get; set; }
        }

        public class Query : IRequest<UserFollowingsEnvelope>
        {
            public bool GetFollowers { get; set; }
            public string AppUserId { get; set; }
            public Pagination Pagination { get; set; }


            public Query(bool getFollowers, string appUserId, Pagination pagination)
            {
                GetFollowers = getFollowers;
                AppUserId = appUserId;
                Pagination = pagination ?? new Pagination();
            }
        }

        public class Handler : IRequestHandler<Query, UserFollowingsEnvelope>
        {
            private readonly ApplicationDbContext _context;
            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<UserFollowingsEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                int count = 0;
                IQueryable<UserFollowingItem> queryable;

                if (request.GetFollowers)
                {
                    queryable = _context.UserFollowings
                        .Where(c => c.ObserverId == request.AppUserId)
                        .Select(c => new UserFollowingItem
                        {
                            AppUserId = c.TargetId,
                            DisplayName = c.Target.DisplayName,
                            ProfileImagePath = c.Target.UserPhotos.FirstOrDefault(x => x.ImageType == ImageType.Profile) != null ?
                        c.Target.UserPhotos.FirstOrDefault(x => x.ImageType == ImageType.Profile).Path : ""
                        }).AsQueryable();
                }
                else
                {
                    queryable = _context.UserFollowings
                         .Where(c => c.TargetId == request.AppUserId)
                         .Select(c => new UserFollowingItem
                         {
                             AppUserId = c.ObserverId,
                             DisplayName = c.Target.DisplayName,
                             ProfileImagePath = c.Observer.UserPhotos.FirstOrDefault(x => x.ImageType == ImageType.Profile) != null ?
                         c.Observer.UserPhotos.FirstOrDefault(x => x.ImageType == ImageType.Profile).Path : ""
                         }).AsQueryable();
                }

                count = queryable.Count();
                var returnedList = await queryable
                    .Skip(request.Pagination.Offset ?? 0)
                    .Take(request.Pagination.Limit ?? 10)
                    .ToListAsync();

                return new UserFollowingsEnvelope
                {
                    Count = count,
                    UserFollowingItems = returnedList
                };
            }
        }
    }
}
