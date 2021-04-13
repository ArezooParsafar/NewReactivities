
using Application.Util;
using Application.ViewModels.CommentDto;
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

namespace Application.Services.CommentHandling
{
    public class CommentList
    {

        public class CommentsEnvelope
        {
            public int TotalCount { get; set; }
            public List<CommentItem> Comments { get; set; }
        }

        public class Query : IRequest<CommentsEnvelope>
        {
            public Guid? ActivityId { get; set; }
            public string UserPhotoId { get; set; }
            public Pagination Pagination { get; set; }

            public Query(Pagination pagination, Guid? activityId, string userPhotoId)
            {
                Pagination = pagination ?? new Pagination();
                ActivityId = activityId;
                UserPhotoId = userPhotoId;
            }
        }

        public class Handler : IRequestHandler<Query, CommentsEnvelope>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<CommentsEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {

                var queryable = _context.Comments
                    .Where(c => c.ActivityId == request.ActivityId || c.UserPhotoId == request.UserPhotoId);

                var totalCount = queryable.Count();

                var comments = await queryable
                    .Skip(request.Pagination.Offset ?? 0)
                    .Take(request.Pagination.Limit ?? 5)
                    .ToListAsync();
                var returnedObject = new CommentsEnvelope
                {
                    TotalCount = totalCount,
                    Comments = _mapper.Map<List<Comment>, List<CommentItem>>(comments)
                };

                return returnedObject;
            }
        }
    }
}
