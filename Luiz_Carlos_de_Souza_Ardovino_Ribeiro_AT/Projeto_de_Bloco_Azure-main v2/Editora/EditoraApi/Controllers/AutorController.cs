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
    public class AutorController : Controller
    {
        private readonly IAutorRepository _repository;

        public AutorController(IAutorRepository repository)
        {
            _repository = repository;
        }

        // GET api/autor
        [HttpGet]
        public async Task<IEnumerable<Autor>> Get()
        {
            return await _repository.GetAll();
        }

        // GET: api/autor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> Details(int id)
        {
            var autor = await _repository.GetById(id);

            if (autor == null)
            {
                return NotFound();
            }

            return autor;
        }


        // POST: api/autor/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Autor autor)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(autor);
                await _repository.SaveChangesAsync();
                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        }

        // PUT: api/autor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Autor autor)
        {

            if (id != autor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (!_repository.EntityExists(autor.Id)) return NotFound();
                _repository.Update(autor);
                await _repository.SaveChangesAsync();
            }
            return Ok(new Response { Status = "Success", Message = "User updated successfully!" });
        
        }

     

        // DELETE: api/autor/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var autor = await _repository.GetById(id);
            if (autor == null) return NotFound();

            _repository.Delete(autor);
            await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
