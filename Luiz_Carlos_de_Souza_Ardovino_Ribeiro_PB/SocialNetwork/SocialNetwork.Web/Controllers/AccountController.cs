using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using SocialNetwork.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Web.Controllers
{
    public class AccountController : Controller
    {
        private HttpContent requestContent;

       

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }

        // GET: Account/ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }

      

        //POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44394");
                    var response = await client.PostAsJsonAsync("api/Account/Register", model);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            return View();
        }

        //POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new Dictionary<string, string>
                {
                    { "grant_type", "password"},
                    { "username", model.Username},
                    { "password", model.Password }
                };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44394");

                    using (var requestContent = new FormUrlEncodedContent(data))
                    {
                        var response = await client.PostAsync("/Token", requestContent);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var tokenData = JObject.Parse(responseContent);
                            Session.Add("access_token", tokenData["access_token"]);

                            return RedirectToAction("Index", "Home");
                        }
                    }
                    return View("Error");
                }
            };

            return View();
        }

        //POST: Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44394");
                    var response = await client.PostAsJsonAsync("api/Account/ForgotPassword", model);

                    if (response.IsSuccessStatusCode)
                    {
                        //TODO: redirecionar para a tela de aviso que email foi enviado
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44394");
                    var response = await client.PostAsJsonAsync("api/Account/ResetPassword", model);

                    if (response.IsSuccessStatusCode)
                    {
                        //TODO: redirecionar para a tela de aviso que email foi enviado
                        return RedirectToAction("ResetPasswordConfirmation");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            return View();
        }

    }
}