using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Services.UserHandling;
using Application.ViewModels.UserDto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class User : BaseController
    {
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost("login")]
        public async Task<ActionResult<UserItem>> Login(Login.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("logout")]
        public async Task<ActionResult<Unit>> Logout(Logout.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("ResetPassword")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Unit>> ResetPassword(ResetPassword.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("SendEmail")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Unit>> SendResetEmail(SendResetPasswordEmail.Command command)
        {
            return await Mediator.Send(command);
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserItem>> Register(Register.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        [Authorize]
        public async Task<UserItem> GetCurrentUser()
        {
            var user = await Mediator.Send(new GetCurrent.Query(User.FindFirstValue(ClaimTypes.Email)));
            return user;

        }
    }
}
