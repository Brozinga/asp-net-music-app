using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Domain.HttpResponses;
using MusicApp.Domain.ViewModels;
using MusicApp.Services.Handlers;
using MusicApp.Services.Responses;

namespace MusicApp.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json", Type = typeof(BasicResponse<BasicObject>))]
    //[Produces("application/json", "application/xml", Type = typeof(BasicResponse<BasicObject>))]
    //[Consumes("application/json", "application/xml")]
    public class UserController : ControllerBase
    {
        private readonly UserHandle _userHandle;
        private readonly LoginHandle _loginHandle;

        public UserController(UserHandle userHandle, LoginHandle loginHandle)
        {
            _userHandle = userHandle;
            _loginHandle = loginHandle;
        }

        [HttpPost("create")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateViewModel viewModel)
        {
           var response = await _userHandle.Execute(viewModel);
           return StatusCode(response.Status, response);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel)
        {
            var response = await _loginHandle.Execute(viewModel);
            return StatusCode(response.Status, response);
        }
    }
}
