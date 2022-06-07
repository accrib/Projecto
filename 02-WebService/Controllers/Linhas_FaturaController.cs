using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_DAL;
using _02_WebService.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _02_WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Linhas_FaturaController : ControllerBase
    {
        private readonly IDataRepository<Linha_Fatura> _dataRepositorio;

        public Linhas_FaturaController(IDataRepository<Linha_Fatura> context)
        {
            _dataRepositorio = context;
        }

        // GET: api/Linhas_Fatura
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Linha_Fatura> linhas_fatura = _dataRepositorio.GetAll();
            return Ok(linhas_fatura);
        }

        // GET: api/Linhas_Fatura/5
        [HttpGet("{id}")]
        public ActionResult GetLinhaFatura(int id)
        {
            Linha_Fatura linhas_fatura = _dataRepositorio.Get(id);

            if (linhas_fatura == null)
            {
                return NotFound("Linha de fatura inexistente.");
            }

            return Ok(linhas_fatura);
        }

        // PUT: api/Linhas_Fatura/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Linha_Fatura linhas_fatura)
        {
            if (linhas_fatura == null)
            {
                return BadRequest("Linha de fatura inexistente.");
            }

            Linha_Fatura linhasfaturaToUpdate = _dataRepositorio.Get(id);
            if (linhasfaturaToUpdate == null)
            {
                return NotFound("A linha de fatura não foi encontrada.");
            }

            _dataRepositorio.Update(linhasfaturaToUpdate, linhas_fatura);
            return NoContent();
        }

        // POST: api/Linhas_Fatura
        [HttpPost]
        public ActionResult PostLinhasFatura([FromBody]Linha_Fatura linhas_fatura)
        {
            if (linhas_fatura == null)
            {
                return BadRequest("A linha de fatura não existe.");
            }

            _dataRepositorio.Add(linhas_fatura);
            return CreatedAtRoute(
                  "Get",
                  new { Id = linhas_fatura.ID },
                  linhas_fatura);
        }

        // DELETE: api/Linhas_Fatura/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Linha_Fatura linhas_fatura = _dataRepositorio.Get(id);
            if (linhas_fatura == null)
            {
                return NotFound("A linhas de fatura não foi encontrada.");
            }

            _dataRepositorio.Delete(linhas_fatura);
            return NoContent();
        }
    }
}
