using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;
using NS.WebApp.MVC.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Controllers
{
    public class IdentityController : MainController
    {
        private readonly IAuthenticatorService _authenticatorService;

        public IdentityController(IAuthenticatorService authenticatorService)
        {
            _authenticatorService = authenticatorService;
        }

        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);

            var response = await _authenticatorService.Register(userRegister);

            if (HasErrorsResponse(response.ResponseResult)) return View(userRegister);

            await RealizaLogin(response);

            return RedirectToAction("Index", "Catalog");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLogin userLogin, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(userLogin);

            var response = await _authenticatorService.Login(userLogin);

            if (HasErrorsResponse(response.ResponseResult)) return View(userLogin);

            await RealizaLogin(response);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Catalog");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("sair")]
        public async Task<IActionResult> Logout(UserLogin userLogin)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Catalog");
        }

        private async Task RealizaLogin(UserResponseLogin user)
        {
            var token = GetTokenFormated(user.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", user.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        private static JwtSecurityToken GetTokenFormated(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
        }
    }
}
