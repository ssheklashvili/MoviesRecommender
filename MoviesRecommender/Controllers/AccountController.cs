using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieRecommender.Core.Interfaces.Services;
using MovieRecommender.Core.Models;
using MoviesRecommender.WEB.ViewModels;

namespace MoviesRecommender.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = _userService.GetUser(model.Email, model.Password);
            if (user == null)
            {
                ViewBag.LoginError = "Email or Password is not Valid";
            }
            else
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.FirstName)
                    };
                DateTimeOffset? expiresUtc = null;
                bool isPersistent = false;
                if (true)
                {
                    expiresUtc = DateTimeOffset.UtcNow.AddDays(7); // on remembemrMe, an user is stayed logged in for 7 days
                    isPersistent = true;
                }
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = expiresUtc,
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = isPersistent,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = login.ReturnUrl
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };
                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                //   await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        public async Task<IActionResult> LogOut()
        {
            return null;
            //await HttpContext.SignOutAsync();

            //return RedirectToAction(nameof(Index));
        }

        public IActionResult Register()
        {
            var dictionaries = _userService.GetDictionaries();

            var registerViewModel = new RegisterViewModel
            {
                Genres = dictionaries.Where(i => i.IsGenre).Select(i =>
                new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.ID.ToString()
                }),
                Artists = dictionaries.Where(i => i.IsArtist).Select(i =>
                new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.ID.ToString()
                }),
                Directors = dictionaries.Where(i => i.IsDirector).Select(i =>
                new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.ID.ToString()
                })
            };

            return View(registerViewModel);
        }
    }
}
