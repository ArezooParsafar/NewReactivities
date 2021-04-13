using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.UserPhotoHandling;
using Application.ViewModels.UserPhotoDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.Services.UserPhotoHandling.UserPhotoList;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Photo : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<UserPhotoItem>> Add([FromBody] AddPhoto.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<ActionResult<Unit>> Delete(DeletePhoto.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<UserPhotoItem>> Edit(EditPhoto.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("list")]
        public async Task<ActionResult<UserPhotoEnvelope>> List(UserPhotoList.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserPhotoItem>> Detail(PhotoDetail.Query query)
        {
            return await Mediator.Send(query);
        }
    }
}
