using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Domain.HttpResponses;
using MusicApp.Domain.ViewModels;
using MusicApp.Domain.ViewModels.Responses;
using MusicApp.Services.Handlers;
using MusicApp.Services.Responses;

namespace MusicApp.Api.Controllers
{
    [Route("api/music")]
    [ApiController]

    [Consumes("application/json")]
    [Produces("application/json", Type = typeof(BasicResponse<BasicObject>))]
    public class MusicController : ControllerBase
    {

        private readonly MusicHandle _musicHandle;

        public MusicController(MusicHandle musicHandle)
        {
            _musicHandle = musicHandle;
        }

        [HttpPost]
        [Authorize(Policy = "Criador")]
        public async Task<IActionResult> Post([FromBody] AddMusicViewModel vieModel)
        {
            var identity = HttpContext.User.Identity;

            vieModel.Identity = identity;

            var response = await _musicHandle.Execute(vieModel);
            return StatusCode(response.Status, response);
        }

        [HttpPost("range")]
        [Authorize(Policy = "Criador")]
        public async Task<IActionResult> Post([FromBody] AddRangeMusicViewModel vieModel)
        {
            var identity = HttpContext.User.Identity;

            vieModel.Identity = identity;

            var response = await _musicHandle.Execute(vieModel);
            
            return StatusCode(response.Status, response);
        }

        [HttpGet ("{skip:int}/{take:int}")]
        [Authorize(Policy = "Criador")]
        public async Task<IActionResult> Get(int skip, int take)
          {
            var identity = HttpContext.User.Identity;

            var response = await _musicHandle.Execute(new GetAllMusicsViewModel { Skip = skip, Take = take, Identify = identity });

            if (response.Status == 200)
            {
                var basic = (BasicObject) response.Response;
                var respConverter = (UserBasicResponse) basic.Message;
                
                HttpContext.Response.Headers.Add("total", respConverter.MusicsTotal.ToString());
                HttpContext.Response.Headers.Add("protocol", HttpContext.Request.Protocol);
            }

              return StatusCode(response.Status, response);
          }
        }
}
