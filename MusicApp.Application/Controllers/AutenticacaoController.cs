using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using music_app.Data;
using music_app.Models.ViewModels;
using MusicApp.Domain.ViewModels;

namespace music_app.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly AuthManagerData _auth;
        private readonly HttpManagerData _http;
        public AutenticacaoController(AuthManagerData auth, HttpManagerData http)
        {
            _auth = auth;
            _http = http;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logar([FromForm] LoginViewModel login)
        {
            if (login.Invalid)
                return View("Index", login);
            
            var result = await _http.Post<LoginResponseViewModel, LoginViewModel>("/user/login", login);

            ViewData["ErroMessage"] = "";

            if (result.Error)
            {
                ViewData["ErroMessage"] = result.Response.Title;
                return View("Index", login);
            }


            if (!string.IsNullOrEmpty(result.Response.Message.Token))
            {
                await HttpContext.SignInAsync(_auth.SCHEMA_NAME, _auth.SetToken(result.Response.Message.Token));
                return RedirectToAction("Index", "Dashboard");
            }


            return View("Error");
        }

        public async Task<IActionResult> Logoff()
        {
            await HttpContext.SignOutAsync(_auth.SCHEMA_NAME);
            return RedirectToAction("Index");
        }
    }
}
