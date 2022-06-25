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
    public class AutoresController : Controller
    {
      
        // GET: Autores
        public async Task<IActionResult> Index()
        {
            string access_token =  HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(access_token))
            {
                return RedirectToAction("Login", "Account", null);
            }


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

            return View(autores);

        }

        // GET: Autores/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (1==1)//_apiHttpClient.IsTokenInSession(HttpContext)
            {
                var autor = GetAutoresById(id);
                return View(autor.Result);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<Autor> GetAutoresById(int id)
        {
            var autor = new Autor();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7141");
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.GetAsync($"/api/autor/{id}"); 

                if (response.IsSuccessStatusCode)
                {
                    var reviewResponse = response.Content.ReadAsStringAsync().Result;
                    autor = JsonConvert.DeserializeObject<Autor>(reviewResponse);
                }
                return autor;
            }
        }

        // GET: Autores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create( Autor autor)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7141");

                    client.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/json");

                    var loginJson = JsonConvert.SerializeObject(autor);

                    var data = new StringContent(loginJson, Encoding.UTF8, "application/json");


                    var response = await client.PostAsync("/api/autor/", data);

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

            return View(autor);
        }

        // GET: Autores/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
    
            var autor = GetAutoresById(id);
            return View(autor.Result);

        }

        // POST: Autores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,SobreNome,DataNascimento")] Autor autor)
        {

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7141");

                    client.DefaultRequestHeaders.Add(HttpRequestHeader.ContentType.ToString(), "application/json");

                    var loginJson = JsonConvert.SerializeObject(autor);

                    var data = new StringContent(loginJson, Encoding.UTF8, "application/json");


                    var response = await client.PutAsync($"/api/autor/{id}", data);

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


            return View(autor);
        }

        // GET: Autores/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
    
            var autor = GetAutoresById(id);
            return View(autor.Result);

        }

        // POST: Autores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7141");
                client.DefaultRequestHeaders.Accept.Clear();

                var response = await client.DeleteAsync($"/api/autor/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var reviewResponse = response.Content.ReadAsStringAsync().Result;
                }
            }

            return RedirectToAction(nameof(Index));;
        }

    }
}
