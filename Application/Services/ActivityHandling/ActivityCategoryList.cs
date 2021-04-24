using Application.Util;
using Application.ViewModels.ActivityDto;
using Application.ViewModels.Common;
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
    public class ActivityCategoryList
    {

        public class Query : IRequest<Result<List<OptionData>>>
        {

        }

        public class Handler : IRequestHandler<Query, Result<List<OptionData>>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;

            }

            public async Task<Result<List<OptionData>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var output = await _context.ActivityCategories.ToListAsync();
                var value = _mapper.Map<List<ActivityCategory>, List<OptionData>>(output);
                return Result<List<OptionData>>.Success(value);
            }
        }

    }
}
