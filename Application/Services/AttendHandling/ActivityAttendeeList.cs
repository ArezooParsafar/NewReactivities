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

namespace Application.Services.AttendHandling
{
    public class ActivityAttendeeList
    {
        public class AttendeeEnvelope
        {
            public List<AttendeeItem> Attendees { get; set; }
            public int Count { get; set; }
        }

        public class Query : IRequest<AttendeeEnvelope>
        {
            public Guid ActivityId { get; set; }
            public Pagination Pagination { get; set; }

            public Query(Guid activityId, Pagination pagination)
            {
                ActivityId = activityId;
                Pagination = pagination ?? new Pagination();
            }
        }

        public class Handler : IRequestHandler<Query, AttendeeEnvelope>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<AttendeeEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {

                var queryable = _context.Attendees
                    .Where(c => c.ActivityId == request.ActivityId);

                var attendees = await queryable
                    .Skip(request.Pagination.Offset ?? 0)
                    .Take(request.Pagination.Limit ?? 5)
                    .ToListAsync();


                return new AttendeeEnvelope
                {
                    Attendees = _mapper.Map<List<Attendee>, List<AttendeeItem>>(attendees),
                    Count = queryable.Count()
                };
            }
        }
    }
}
