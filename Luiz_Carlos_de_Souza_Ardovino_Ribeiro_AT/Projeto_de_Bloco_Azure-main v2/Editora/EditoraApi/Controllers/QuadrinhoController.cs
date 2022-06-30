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
    public class QuadrinhoController : Controller
    {
        private readonly IQuadrinhoRepository _repository;

        public QuadrinhoController(IQuadrinhoRepository repository)
        {
            _repository = repository;
        }

        // GET api/quadrinho
        [HttpGet]
        public async Task<IEnumerable<Quadrinho>> Get()
        {
            return await _repository.GetAll();
        }

        // GET: api/quadrinho/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quadrinho>> Details(int id)
        {
            var quadrinho = await _repository.GetById(id);

            if (quadrinho == null)
            {
                return NotFound();
            }

            return quadrinho;
        }


        // POST: api/quadrinho/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Quadrinho quadrinho)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(quadrinho);
                await _repository.SaveChangesAsync();
                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        }


        // PUT: api/quadrinho/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Quadrinho quadrinho)
        {
            if (id != quadrinho.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (!_repository.EntityExists(quadrinho.Id)) return NotFound();
                _repository.Update(quadrinho);
                await _repository.SaveChangesAsync();
            }
            return Ok(new Response { Status = "Success", Message = "User updated successfully!" });

        }



        // DELETE: api/quadrinho/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var quadrinho = await _repository.GetById(id);
            if (quadrinho == null) return NotFound();

            _repository.Delete(quadrinho);
            await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
