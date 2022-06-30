using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EditoraBLL.Models;
using EditoraWeb.Models;
using EditoraDAL.Repositories;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace EditoraWeb.Controllers
{
    public class PerfisController : Controller
    {

        // GET: Perfis
        public async Task<IActionResult> Index()
        {
            string access_token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(access_token))
            {
                return RedirectToAction("Login", "Account", null);
            }


            List<Perfil> perfis = new();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7141");
                client.DefaultRequestHeaders.Accept.Clear();

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                var response = await client.GetAsync("/api/Perfil");

                if (response.IsSuccessStatusCode)
                {
                    var perfilResponse = response.Content.ReadAsStringAsync().Result;
                    perfis = JsonConvert.DeserializeObject<List<Perfil>>(perfilResponse);

                    if (perfis.Count > 0)
                    {
                        ViewBag.Perfil = "Existe";
                    }
                }
            }

            return View(perfis);

        }

        // GET: Perfis/Details/5
        public async Task<IActionResult> Details(string username)
        {

            var perfil = GetPerfisById(username);
            return View(perfil.Result);
        }

        public async Task<Perfil> GetPerfisById(string username)
        {
            var perfil = new Perfil();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7141");
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.GetAsync($"/api/perfil/{username}");

                if (response.IsSuccessStatusCode)
                {
                    var reviewResponse = response.Content.ReadAsStringAsync().Result;
                    perfil = JsonConvert.DeserializeObject<Perfil>(reviewResponse);
                }
                return perfil;
            }
        }

        // GET: Perfis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Perfis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Perfil perfil)
        {
            //if (ModelState.IsValid)
            //{
                using (var client = new HttpClient())
                {
                perfil.Username = HttpContext.Session.GetString("username");

                client.BaseAddress = new Uri("https://localhost:7141");

                    client.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/json");

                    var loginJson = JsonConvert.SerializeObject(perfil);

                    var data = new StringContent(loginJson, Encoding.UTF8, "application/json");


                    var response = await client.PostAsync("/api/perfil/", data);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            //}

            //return View(perfil);
        }

        // GET: Perfis/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var perfil = GetPerfisById(id);
            return View(perfil.Result);

        }

        // POST: Perfis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome,SobreNome,DataNascimento,Telefone")] Perfil perfil)
        {

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("https://localhost:7141");

                    client.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/json");

                    var loginJson = JsonConvert.SerializeObject(perfil);

                    var data = new StringContent(loginJson, Encoding.UTF8, "application/json");


                    var response = await client.PutAsync($"/api/perfil/{id}", data);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }


            return View(perfil);
        }

        // GET: Perfis/Delete/5
        public async Task<IActionResult> Delete(string id)
        {

            var perfil = GetPerfisById(id);
            return View(perfil.Result);

        }

        // POST: Perfis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7141");
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.DeleteAsync($"/api/perfil/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var reviewResponse = response.Content.ReadAsStringAsync().Result;
                }
            }

            return RedirectToAction(nameof(Index)); ;
        }

    }
}
