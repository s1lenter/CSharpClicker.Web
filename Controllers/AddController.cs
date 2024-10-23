using CSharpClicker.Web.UseCases.Login;
using CSharpClicker.Web.UseCases.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CSharpClicker.Web.Controllers
{
    [Route("auth")]
    public class AddController : Controller
    {
        private readonly IMediator mediator;

        public AddController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("resgister")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            await mediator.Send(command);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            await mediator.Send(command);

            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LoginCommand command)
        {
            await mediator.Send(command);

            return Ok();
        }
    }
}
