using Application.Util;
using Application.ViewModels.ActivityDto;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ActivityHandling
{
    public class UserActivityList
    {

        public class Query : IRequest<Result<List<ActivityItem>>>
        {
            public string Predicate { get; set; }
            public string UserName { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<ActivityItem>>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<ActivityItem>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = await _context.Users
                .Where(c => c.UserName == request.UserName)
                .Select(x => x.Id).FirstOrDefaultAsync();

                if (userId == null)
                    return Result<List<ActivityItem>>.Failure("The user was not found.");

                var queryable = _context.Attendees
                    .Include(x => x.Activity)
                    .Where(c => c.AppUserId == userId)
                    .OrderBy(c => c.Activity.HoldingDate)
                    .Select(x => x.Activity)
                    .AsQueryable();

                switch (request.Predicate)
                {
                    case "host":
                        queryable = queryable.Where(x => x.AppUserId == userId);
                        break;
                    case "past":
                        queryable = queryable.Where(c => c.HoldingDate < DateTime.Now);
                        break;
                    default:
                        queryable = queryable.Where(c => c.HoldingDate >= DateTime.Now);
                        break;
                }

                var activities = await queryable.ToListAsync();
                var value = _mapper.Map<List<Activity>, List<ActivityItem>>(activities);
                return Result<List<ActivityItem>>.Success(value);
            }
        }
    }
}
