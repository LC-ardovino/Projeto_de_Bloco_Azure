using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using EditoraDAL.Repositories;
using EditoraBLL.Models;
using ActionNameAttribute = System.Web.Http.ActionNameAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using EditoraBLL.Models.Auth;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;

namespace EditoraApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : Controller
    {
        private readonly IPerfilRepository _repository;

        public PerfilController(IPerfilRepository repository)
        {
            _repository = repository;
        }

        // GET api/perfil
        [HttpGet]
        public async Task<IEnumerable<Perfil>> Get()
        {
            return await _repository.GetAll();
        }

        // GET: api/perfil/5
        [HttpGet("{username}")]
        public async Task<ActionResult<Perfil>> Details(string username)
        {
            var perfil = await _repository.GetById(username);

            if (perfil == null)
            {
                return NotFound();
            }

            return perfil;
        }


        // POST: api/perfil/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Perfil perfil)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(perfil);
                await _repository.SaveChangesAsync();
                return Ok(new Response { Status = "Success", Message = "Perfil criado com sucesso!" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Falha na criação do Perfil! Por favor verifique os detalhes e tente novamente." });
        }

        // PUT: api/perfil/5
        [HttpPut("{username}")]
        public async Task<IActionResult> Edit(string username, Perfil perfil)
        {

            if (username != perfil.Username) return NotFound();

            if (ModelState.IsValid)
            {
                if (!_repository.EntityExists(perfil.Username)) return NotFound();
                _repository.Update(perfil);
                await _repository.SaveChangesAsync();
            }
            return Ok(new Response { Status = "Success", Message = "Perfil atualizado com sucesso!" });

        }



        // DELETE: api/perfil/Delete/5
        [HttpDelete("{username}")]
        public async Task<IActionResult> Delete(string username)
        {
            var perfil = await _repository.GetById(username);
            if (perfil == null) return NotFound();

            _repository.Delete(perfil);
            await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
