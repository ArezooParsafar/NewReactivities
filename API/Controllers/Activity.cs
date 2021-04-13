using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.ActivityHandling;
using Application.Services.AttendHandling;
using Application.Services.CommentHandling;
using Application.Util;
using Application.ViewModels.ActivityDto;
using Application.ViewModels.CommentDto;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Activity : BaseController

    {


        // GET: api/<Activity>
        [HttpGet]
        public async Task<ActionResult<ActivityList.ActivitiesEnvelope>> List(int? limit, int? offset, DateTime? startDate, string venue, string city)
        {
            var data = await Mediator.Send(new ActivityList.Query(new Pagination(limit, offset), startDate, venue, city));
            return data;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityItem>> Details(Guid id)
        {
            return await Mediator.Send(new Detail.Query { Id = id });
        }

        [HttpGet("{username}/Attendees/{predicate}")]
        public async Task<ActionResult<List<ActivityItem>>> UserActivities(string username, string predicate)
        {
            return await Mediator.Send(new UserActivityList.Query { UserName = username, Predicate = predicate });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Update.Command command, Guid id)
        {
            command.ActivityId = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Command { ActivityId = id });
        }

        [HttpGet("/categories")]
        public async Task<ActionResult<List<ActivityCategory>>> ActivityCategories()
        {
            return await Mediator.Send(new ActivityCategoryList.Query());
        }
    }
}
