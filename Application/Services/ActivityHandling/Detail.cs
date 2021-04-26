using Application.Util;
using Application.ViewModels.ActivityDto;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ActivityHandling
{
    public class Detail
    {

        public class Query : IRequest<Result<ActivityItem>>
        {
            public Guid Id { get; set; }

        }

        public class Handler : IRequestHandler<Query, Result<ActivityItem>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<ActivityItem>> Handle(Query request, CancellationToken cancellationToken)
            {

                var activity = await _context.Activities.FindAsync(request.Id);
                if (activity == null)
                {
                    return Result<ActivityItem>.Failure("The activity was not found.");
                }

                var value = _mapper.Map<Activity, ActivityItem>(activity);
                return Result<ActivityItem>.Success(value);
            }
        }
    }
}
