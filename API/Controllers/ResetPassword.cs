using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services.UserHandling;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetPasswordController : BaseController
    {
       

        [HttpPost, ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ActionResult<ValidatePassword.ValidatePasswordResult>> ValidatePassword(ValidatePassword.Query validatePassword)
        {
            return await Mediator.Send(validatePassword);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Unit>> SendResetEmail(SendResetPasswordEmail.Command command)
        {
            return await Mediator.Send(command);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Unit>> ResetUserPassword(ResetPassword.Command command)
        {
            return await Mediator.Send(command);
        }

    }
}
