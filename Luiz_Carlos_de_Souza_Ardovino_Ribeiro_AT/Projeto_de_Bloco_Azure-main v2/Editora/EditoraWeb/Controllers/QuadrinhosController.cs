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
using System.Net;
using System.Text;

namespace EditoraWeb.Controllers
{
    public class QuadrinhosController : Controller
    {

        // GET: Quadrinhos
        public async Task<IActionResult> Index()
        {
            string access_token = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(access_token))
            {
                return RedirectToAction("Login", "Account", null);
            }

            List<Quadrinho> quadrinhos = new();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7141");
                client.DefaultRequestHeaders.Accept.Clear();

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                var response = await client.GetAsync("/api/Quadrinho");

                if (response.IsSuccessStatusCode)
                {
                    var quadrinhoResponse = response.Content.ReadAsStringAsync().Result;
                    quadrinhos = JsonConvert.DeserializeObject<List<Quadrinho>>(quadrinhoResponse);
                }
            }

            return View(quadrinhos);


        }

        public async Task<List<Autor>> GetAutores()
        {
            List<Autor> autores = new();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7141");
                client.DefaultRequestHeaders.Accept.Clear();

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                var response = await client.GetAsync("/api/Autor");

                if (response.IsSuccessStatusCode)
                {
                    var autorResponse = response.Content.ReadAsStringAsync().Result;
                    autores = JsonConvert.DeserializeObject<List<Autor>>(autorResponse);
                }
            }

            return autores;
        }

        // GET: Quadrinhos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (1 == 1)//_apiHttpClient.IsTokenInSession(HttpContext)
            {
                var quadrinho = GetQuadrinhosById(id);

                List<Autor> autores = new();
                autores = GetAutores().Result;

                ViewData["AutorId"] = new SelectList(autores, "Id", "Nome", quadrinho.Result.Autor.Id);


                return View(quadrinho.Result);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<Quadrinho> GetQuadrinhosById(int id)
        {
            var quadrinho = new Quadrinho();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7141");
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.GetAsync($"/api/quadrinho/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var reviewResponse = response.Content.ReadAsStringAsync().Result;
                    quadrinho = JsonConvert.DeserializeObject<Quadrinho>(reviewResponse);
                }

                return quadrinho;
            }
        }

        // GET: Quadrinhos/Create
        public async Task<IActionResult> Create()
        {

            List<Autor> autores = new();
            autores = GetAutores().Result;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:7141");
                client.DefaultRequestHeaders.Accept.Clear();

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                var response = await client.GetAsync("/api/Autor");

                if (response.IsSuccessStatusCode)
                {
                    var autorResponse = response.Content.ReadAsStringAsync().Result;
                    autores = JsonConvert.DeserializeObject<List<Autor>>(autorResponse);
                    ViewData["AutorId"] = new SelectList(autores, "Id", "Nome", null);

                }
            }

            return View();
        }



        // POST: Quadrinhos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Quadrinho quadrinho)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7141");

                    client.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/json");

                    var loginJson = JsonConvert.SerializeObject(quadrinho);

                    var data = new StringContent(loginJson, Encoding.UTF8, "application/json");


                    var response = await client.PostAsync("/api/quadrinho/", data);

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

            return View(quadrinho);
        }

        // GET: Quadrinhos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (1 == 1)//_apiHttpClient.IsTokenInSession(HttpContext)
            {

                var quadrinho = GetQuadrinhosById(id);

                List<Autor> autores = new();
                autores = GetAutores().Result;

                ViewData["AutorId"] = new SelectList(autores, "Id", "Nome", quadrinho.Result.Autor.Id);

                return View(quadrinho.Result);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        // POST: Quadrinhos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,SobreNome,DataNascimento")] Quadrinho quadrinho)
        {

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7141");

                    client.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/json");

                    var loginJson = JsonConvert.SerializeObject(quadrinho);

                    var data = new StringContent(loginJson, Encoding.UTF8, "application/json");


                    var response = await client.PutAsync($"/api/quadrinho/{id}", data);

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


            return View(quadrinho);
        }


        // GET: Quadrinhos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (1 == 1)//_apiHttpClient.IsTokenInSession(HttpContext)
            {
                var quadrinho = GetQuadrinhosById(id);
                return View(quadrinho.Result);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Quadrinhos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7141");
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.DeleteAsync($"/api/quadrinho/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var reviewResponse = response.Content.ReadAsStringAsync().Result;
                }
            }

            return RedirectToAction(nameof(Index)); ;
        }

    }
}
