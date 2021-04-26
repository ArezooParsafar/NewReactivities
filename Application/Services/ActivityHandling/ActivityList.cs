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
    public class ActivityList
    {
        public class ActivitiesEnvelope
        {
            public List<ActivityItem> ActivityItems { get; set; }
            public int TotalCount { get; set; }
        }


        public class Query : IRequest<Result<ActivitiesEnvelope>>
        {
            public Query(Pagination pagination, DateTime? startDate, string venue, string city)
            {
                Pagination = pagination ?? new Pagination();
                StartDate = startDate;
                Venue = venue;
                City = city;
            }

            public Pagination Pagination { get; set; }
            public DateTime? StartDate { get; }
            public string Venue { get; }
            public string City { get; }
        }

        public class Handler : IRequestHandler<Query, Result<ActivitiesEnvelope>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<ActivitiesEnvelope>> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.Activities
                    .Include(c => c.Category)
                    .Where(c => !c.IsDeleted)
                    .OrderBy(c => c.HoldingDate)
                    .AsQueryable();

                if (request.StartDate.HasValue)
                {
                    queryable = queryable.Where(c => c.HoldingDate >= request.StartDate);
                }

                if (!string.IsNullOrEmpty(request.City))
                {
                    queryable = queryable.Where(c => c.City.Contains(request.City));
                }

                if (!string.IsNullOrEmpty(request.Venue))
                {
                    queryable = queryable.Where(c => c.Venue.Contains(request.Venue));
                }

                var activities = await queryable
                  .Skip(request.Pagination.Offset ?? 0)
                  .Take(request.Pagination.Limit ?? 3)
                .ToListAsync();

                var value = new ActivitiesEnvelope
                {
                    ActivityItems = _mapper.Map<List<Activity>, List<ActivityItem>>(activities),
                    TotalCount = queryable.Count()
                };
                return Result<ActivitiesEnvelope>.Success(value);
            }
        }

    }
}
