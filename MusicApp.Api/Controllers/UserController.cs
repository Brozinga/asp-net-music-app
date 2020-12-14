using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Domain.ViewModels;
using MusicApp.Services.Handlers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserHandle _userHandle;

        public UserController(UserHandle userHandle)
        {
            _userHandle = userHandle;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateViewModel viewModel)
        {
           var response = await _userHandle.Execute(viewModel);
           return StatusCode(response.Status, response);
        }
    }
}
