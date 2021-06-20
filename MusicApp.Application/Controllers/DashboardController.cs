using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using music_app.Data;
using music_app.Models.ViewModels;

namespace music_app.Controllers
{
    public class DashboardController : Controller
    {

        private readonly AuthManagerData _auth;
        private readonly HttpManagerData _http;
        public DashboardController(AuthManagerData auth, HttpManagerData http)
        {
            _auth = auth;
            _http = http;
        }

        [Authorize(AuthenticationSchemes = "AuthCookie")]
        public async Task<IActionResult> Index()
        {
            var token = _auth.GetAuthToken(User.Identity);
            var result = await _http.Get<MusicsResponseViewModel>($"/music/0/9999999", token);

            ViewData["User"] = result.Response.Message.UserName;

            return View(result);
        }
    }
}
