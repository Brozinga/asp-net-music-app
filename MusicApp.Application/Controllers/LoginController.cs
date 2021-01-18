using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MusicApp.Domain.ViewModels;
using Newtonsoft.Json.Linq;

namespace music_app.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logar(LoginViewModel login)
        {
            var url = _configuration["Api:Url"].ToString();
            var uri = new Uri($"{url}/user/login");

            var client = new HttpClient();
            var result = await client.PostAsJsonAsync(uri, login);

            if (result.IsSuccessStatusCode)
            {
                dynamic res = JObject.Parse(await result.Content.ReadAsStringAsync());

                var Token = res.response.message.token;

                return View("Index");
            }

            return View("Error");
        }
    }
}
