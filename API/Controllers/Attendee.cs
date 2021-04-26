using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.AttendHandling;
using Application.Util;
using Application.ViewModels.ActivityDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class Attendee : BaseController
    {
        [HttpPost("attend")]
        public async Task<ActionResult<Unit>> AttendUnAttend(Guid activityId)
        {
            return await Mediator.Send(new AttendORSkipActivity.Command { ActivityId = activityId });
        }

        [HttpGet("{activityId}")]
        public async Task<ActionResult<ActivityAttendeeList.AttendeeEnvelope>> Attendees(Guid activityId, int? limit, int? offset)
        {
            return await Mediator.Send(new ActivityAttendeeList.Query(activityId, new Pagination(limit, offset)));
        }
    }
}
