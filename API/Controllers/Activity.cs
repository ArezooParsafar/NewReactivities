using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services.ActivityHandling;
using Application.Util;
using Application.ViewModels.ActivityDto;
using Application.ViewModels.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Services.ActivityHandling.ActivityList;

namespace API.Controllers
{
    [Route("Activity")]
    [ApiController]
    public class Activity : BaseController

    {


        // GET: api/<Activity>
        [HttpGet]
        public async Task<IActionResult> List(int? limit, int? offset, DateTime? startDate, string venue, string city)
        {
            var result = await Mediator.Send(new ActivityList.Query(new Pagination(limit, offset), startDate, venue, city));
            return ProcessResult<ActivitiesEnvelope>(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(Guid id)
        {
            var result = await Mediator.Send(new Detail.Query { Id = id });
            return ProcessResult(result);
        }

        [HttpGet("{username}/Attendees/{predicate}")]
        public async Task<ActionResult> UserActivities(string username, string predicate)
        {
            var result = await Mediator.Send(new UserActivityList.Query { UserName = username, Predicate = predicate });
            return ProcessResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Create.Command command)
        {
            var result = await Mediator.Send(command);
            return ProcessResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Update.Command command, Guid id)
        {
            command.ActivityId = id;
            var result = await Mediator.Send(command);
            return ProcessResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new Delete.Command { ActivityId = id });
            return ProcessResult(result);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> ActivityCategories()
        {
            var result = await Mediator.Send(new ActivityCategoryList.Query());
            return ProcessResult(result);
        }
    }
}
