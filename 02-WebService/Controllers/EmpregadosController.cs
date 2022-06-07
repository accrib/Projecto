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
    public class EmpregadosController : ControllerBase
    {
        // Declaração do repositório que faz a ligação á bd e dizendo que todos os metodos irão aceder á class Empregado
        private readonly IDataRepository<Empregado> _dataRepositorio;

        // construtor
        public EmpregadosController(IDataRepository<Empregado> context)
        {
            _dataRepositorio = context;
        }

        // método que vai retornar todos os empregados
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Empregado> empregados = _dataRepositorio.GetAll();
            return Ok(empregados);
        }

        // método que retorna o empregado por id
        [HttpGet("{id}")]
        public ActionResult GetEmpregado(int id)
        {
            Empregado empregado = _dataRepositorio.Get(id);

                return Ok(empregado);
           
        }

        // método que edita os detalhes do empregado
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Empregado empregado)
        {
            if (empregado == null)
            {
                return BadRequest("Empregado inexistente.");
            }

            Empregado empregadoToUpdate = _dataRepositorio.Get(id);
            if (empregadoToUpdate == null)
            {
                return NotFound("O empregado não foi encontrado.");
            }

            _dataRepositorio.Update(empregadoToUpdate, empregado);
            return NoContent();
        }

        // método que cria um empregado novo
        [HttpPost]
        public ActionResult PostEmpregado([FromBody]Empregado empregado)
        {
            if (empregado == null)
            {
                return BadRequest("Empregado não existe.");
            }

            _dataRepositorio.Add(empregado);
            return CreatedAtRoute(
                  "Get",
                  new { Id = empregado.ID },
                  empregado);
        }

        // método que elimina um empregado
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Empregado employee = _dataRepositorio.Get(id);
            if (employee == null)
            {
                return NotFound("O empregado não foi encontrado.");
            }

            _dataRepositorio.Delete(employee);
            return NoContent();
        }
    }
}
