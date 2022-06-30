using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using EditoraDAL.Repositories;
using EditoraBLL.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using ActionNameAttribute = System.Web.Http.ActionNameAttribute;

namespace EditoraApi.Controllers
{
    [ApiController]
    public class AmigoController : Controller
    {
        private readonly IRepository _repository;

        public AmigoController(IRepository repository)
        {
            _repository = repository;
        }

        // GET api/amigo
        [Route("api/amigo")]
        [HttpGet]
        public async Task<IEnumerable<Amigo>> Get()
        {
            return await _repository.GetAll();
        }

        // GET: api/amigo/5
        [Route("api/amigo/Details/5")]
        [HttpGet]
        public async Task<ActionResult<Amigo>> Details(int id)
        {
            var amigo = await _repository.GetById(id);


            if (amigo == null)
            {
                return NotFound();
            }

            return amigo;
        }


        // POST: api/amigo/Create
        [Route("api/amigo/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Amigo amigo)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(amigo);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(amigo);
        }

        // GET: api/amigo/Edit/5
        [Route("api/amigo/Edit/5")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var amigo = await _repository.GetById((int)id);
            if (amigo == null) return NotFound();

            return View(amigo);
        }

        // POST: api/amigo/Edit/
        [Route("api/amigo/Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Amigo amigo)
        {
            if (id != amigo.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (!_repository.EntityExists(amigo.Id)) return NotFound();
                _repository.Update(amigo);
                await _repository.SaveChangesAsync();
            }
            return View(amigo);
        }

        // GET: api/amigo/Delete/5
        [Route("api/amigo/Delete/5")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var amigo = await _repository.GetById((int)id);
            if (amigo == null) return NotFound();

            return View(amigo);
        }

        // POST: api/amigo/Delete/5
        [Route("api/amigo/Delete/5")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amigo = await _repository.GetById(id);
            if (amigo == null) return NotFound();

            _repository.Delete(amigo);
            await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
