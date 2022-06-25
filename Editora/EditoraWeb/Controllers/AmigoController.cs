using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EditoraWeb.Models;
using Newtonsoft.Json;
using EditoraBLL.Models;

namespace EditoraWeb.Controllers
{
    public class AmigoController : Controller
    {
        // GET: AmigoEmail
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                
                client.BaseAddress = new Uri("https://localhost:7105");
                client.DefaultRequestHeaders.Accept.Clear();

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                var objResponse = client.GetAsync("/api/amigo").Result
    .Content.ReadAsStringAsync().Result;


                if (objResponse != null)
                {

                    List<AmigoViewModel> listaVM = new List<AmigoViewModel>();

                    var model = JsonConvert.DeserializeObject<List<Amigo>>(objResponse);

                    foreach (var item in model)
                    {
                        AmigoViewModel cliVM = new AmigoViewModel(); //ViewModel
                        cliVM.Nome = item.Nome;
                        cliVM.Sobrenome = item.Sobrenome;
                        cliVM.Email = item.Email;
                        cliVM.DataNascimento = item.DataNascimento;
                        cliVM.Telefone = item.Telefone;
                        listaVM.Add(cliVM);
                    }
                    return View("../Amigos/ListAmigoView", listaVM);

                }

                return View("Error");

            }
        }
    }
}
