using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.CommentHandling;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Util;
namespace API.Controllers
{
    public class Comment : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<CommentList.CommentsEnvelope>> Comments(Guid? activityId, string userPhotoId, int? limit, int? offset)
        {
            return await Mediator.Send(new CommentList.Query(new Pagination(limit, offset), activityId, userPhotoId));
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CommentCreate.Command comment)
        {
            return await Mediator.Send(comment);
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Delete(Guid commentId, Guid? activityId, string userPhotoId)
        {
            return await Mediator.Send(new CommentDelete.Command { ActivityId = activityId, CommentId = commentId, UserPhotoId = userPhotoId });
        }


        [HttpDelete]
        public async Task<ActionResult<Unit>> Edit(CommentEdit.Command comment)
        {
            return await Mediator.Send(comment);
        }

    }
}
