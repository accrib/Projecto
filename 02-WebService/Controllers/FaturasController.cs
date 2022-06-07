using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _01_DAL;
using _02_WebService.Models.Repository;

namespace _02_WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturasController : ControllerBase
    {

        private readonly IDataRepository<Fatura> _dataRepositorio;

        public FaturasController(IDataRepository<Fatura> context)
        {
            _dataRepositorio = context;
        }

        // GET: api/Fatura
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Fatura> fatura = _dataRepositorio.GetAll();
            return Ok(fatura);
        }

        // GET: api/Fatura/5
        [HttpGet("{id}")]
        public ActionResult GetFatura(int id)
        {
            Fatura fatura = _dataRepositorio.Get(id);

            if (fatura == null)
            {
                return NotFound("Fatura inexistente.");
            }

            return Ok(fatura);
        }

        // PUT: api/Linhas_Fatura/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Fatura fatura)
        {
            if (fatura == null)
            {
                return BadRequest("Fatura inexistente.");
            }

            Fatura faturaToUpdate = _dataRepositorio.Get(id);
            if (faturaToUpdate == null)
            {
                return NotFound("A fatura não foi encontrada.");
            }

            _dataRepositorio.Update(faturaToUpdate, fatura);
            return NoContent();
        }

        // POST: api/Fatura
        [HttpPost]
        public ActionResult PostFatura([FromBody]Fatura fatura)
        {
            if (fatura == null)
            {
                return BadRequest("A não existe.");
            }

            _dataRepositorio.Add(fatura);
            return CreatedAtRoute(
                  "Get",
                  new { Id = fatura.ID },
                  fatura);
        }

        // DELETE: api/Fatura/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Fatura fatura = _dataRepositorio.Get(id);
            if (fatura == null)
            {
                return NotFound("A fatura não foi encontrada.");
            }

            _dataRepositorio.Delete(fatura);
            return NoContent();
        }
    }

}
