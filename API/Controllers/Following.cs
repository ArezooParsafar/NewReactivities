using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.UserFollowingHandling;
using Application.Util;
using Application.ViewModels.UserFollowingDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Following : BaseController
    {
        [HttpPost("{id}/follow")]
        public async Task<ActionResult<Unit>> Follow(string targetAppUserId)
        {
            return await Mediator.Send(new FollowUser.Command { TargetAppUserId = targetAppUserId });
        }

        [HttpDelete("{id}/follow")]
        public async Task<ActionResult<Unit>> UnFollow(string targetAppUserId)
        {
            return await Mediator.Send(new UnfollowUser.Command { TargetAppUserId = targetAppUserId });
        }

        [HttpGet]
        public async Task<ActionResult<UserFollowingsList.UserFollowingsEnvelope>> GetFollowings(string appUserId,
            bool getFollowers, int? limit, int? offset)
        {
            return await Mediator.Send(new UserFollowingsList.Query(getFollowers, appUserId, new Pagination(limit, offset)));
        }

    }

}
