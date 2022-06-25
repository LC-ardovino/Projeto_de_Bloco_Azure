using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using EditoraWeb.Models;
using EditoraBLL.Models.Auth;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Net;

namespace SocialNetwork.Web.Controllers
{
    public class AccountController : Controller
    {
        private HttpContent? requestContent;



        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

     

        //POST: Account/Register
        [HttpPost, ActionName("Register")]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7141");

                    client.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/json");

                    var loginJson = JsonConvert.SerializeObject(model);

                    var data = new StringContent(loginJson, Encoding.UTF8, "application/json");


                    var response = await client.PostAsync("/api/Authenticate/register", data);

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
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7141");

                    client.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/json");

                    var loginJson = JsonConvert.SerializeObject(model);

                    var data = new StringContent(loginJson, Encoding.UTF8, "application/json");


                    var response = await client.PostAsync("/api/Authenticate/login", data);

                    var loginReponse = await response.Content.ReadFromJsonAsync<LoginReponse>();

                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetString("token", loginReponse.Token);

                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        //ViewBag.Message = response.StatusCode;
                        ViewBag.Message = "Login inválido!";

                    }
                }
            }
            return View();

        }

        //// GET: Account/ForgotPassword
        //public ActionResult ForgotPassword()
        //{
        //    return View();
        //}

        ////POST: Account/ForgotPassword
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri("https://localhost:7141");
        //            var response = await client.PostAsJsonAsync("api/Account/ForgotPassword", model);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                //TODO: redirecionar para a tela de aviso que email foi enviado
        //                return RedirectToAction("Login");
        //            }
        //            else
        //            {
        //                return View("Error");
        //            }
        //        }
        //    }
        //    return View();
        //}

        //
        //// POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri("https://localhost:7141");
        //            var response = await client.PostAsJsonAsync("api/Account/ResetPassword", model);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                //TODO: redirecionar para a tela de aviso que email foi enviado
        //                return RedirectToAction("ResetPasswordConfirmation");
        //            }
        //            else
        //            {
        //                return View("Error");
        //            }
        //        }
        //    }
        //    return View();
        //}

    }
}