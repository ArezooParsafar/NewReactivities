using Application.Interfaces;
using Application.ViewModels.UserDto;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Services.UserHandling
{
    public class UserProfile
    {

        public class Query : IRequest<UserProfileDetail>
        {
            public string UserId { get; set; }

        }

        public class Handler : IRequestHandler<Query, UserProfileDetail>
        {
            private readonly ApplicationDbContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(ApplicationDbContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<UserProfileDetail> Handle(Query request, CancellationToken cancellationToken)
            {
                var observerUserId = _userAccessor.GetUserId();

                var user = await _context.Users.Where(x => x.Id == request.UserId)
                            .Select(c => new UserProfileDetail
                            {
                                AppUserId = c.Id,
                                Bio = c.Bio,
                                DisplayName = c.DisplayName,
                                IsPublicProfile = c.IsPublicProfile,
                                FollowersCount = c.Followers.Count(),
                                FollowingsCount = c.Followings.Count(),
                                PendingRequestsCount = c.Followers.Count(s => !s.IsAccepted),
                                IsFollowing = c.Followings.Any(s => s.ObserverId == observerUserId && s.TargetId == request.UserId && s.IsAccepted),
                                HeaderImage = c.UserPhotos.SingleOrDefault(x => x.ImageType == ImageType.Header) != null ?
                                    c.UserPhotos.SingleOrDefault(x => x.ImageType == ImageType.Header).Path : "",
                                ProfileImage = c.UserPhotos.SingleOrDefault(x => x.ImageType == ImageType.Profile) != null ?
                                    c.UserPhotos.SingleOrDefault(x => x.ImageType == ImageType.Profile).Path : "",

                            }).SingleOrDefaultAsync();

                return user;
            }
        }
    }
}
