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
    public class ActivityCategoryList
    {

        public class Query : IRequest<List<ActivityCategory>>
        {

        }

        public class Handler : IRequestHandler<Query, List<ActivityCategory>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<ActivityCategory>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.ActivityCategories.ToListAsync();
            }
        }

    }
}
