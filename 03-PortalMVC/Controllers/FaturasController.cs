using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using _01_DAL;
using _03_PortalMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace _03_PortalMVC.Controllers
{
    public class FaturasController : Controller
    {
        Helper _api = new Helper();
        List<Fatura> emp = new List<Fatura>();
        private readonly BD _context;

        public FaturasController(BD context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var bD = _context.Faturas.Include(p => p.Empregado);
            return View(await bD.ToListAsync());

        }


        //GET: faturas/Details/5

        public async Task<ActionResult> Details(int Id)
        {
            var fatura = new Fatura();

            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync($"api/faturas/{Id}");

            if (response.IsSuccessStatusCode)
            {
                ViewData["ID_Empregado"] = new SelectList(_context.Empregados, "ID", "Nome");

                var resultado = response.Content.ReadAsStringAsync().Result;
                fatura = JsonConvert.DeserializeObject<Fatura>(resultado);
            }

            return View(fatura);

        }


        // GET: faturas/Create
        public ActionResult Create()
        {
            ViewData["ID_Empregado"] = new SelectList(_context.Empregados, "ID", "Nome");
            return View();
        }

        // POST: faturas/Create
        [HttpPost]
        public async Task<ActionResult> Create(Fatura fatura)
        {
            HttpClient client = _api.Initial();
            var post = await client.PostAsJsonAsync<Fatura>("api/faturas", fatura);
            ViewData["ID_Empregado"] = new SelectList(_context.Empregados, "ID", "Nome", fatura.ID_Empregado);

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> Delete(int Id)
        {
            var fatura = new Fatura();
            string userId = User.Identity.GetUserName();
            var empregados = _context.Faturas.Any(x => x.Empregado.EMail == userId && x.ID == Id);

            if (fatura == null)
            {
                return NotFound("O fatura não foi encontrado.");
            }
            else
            {
                HttpClient client = _api.Initial();
                HttpResponseMessage response = await client.DeleteAsync($"api/faturas/{Id}");
                ViewData["ID_Empregado"] = new SelectList(_context.Empregados, "ID", "Nome");

            }
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Edit(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            string userId = User.Identity.GetUserName();
            Fatura faturas = new Fatura();

            using (var httpClient = new HttpClient())
            {
                Empregado emp = new Empregado();

                var empregados = _context.Produtos.Any(x => x.Empregado.EMail == userId && x.ID == Id);

                if (empregados == true)
                {

                    using (var response = await httpClient.GetAsync("http://localhost:30504/api/Faturas/" + Id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        faturas = JsonConvert.DeserializeObject<Fatura>(apiResponse);
                        ViewData["ID_Empregado"] = new SelectList(_context.Empregados, "ID", "Nome");
                    }
                    return View(faturas);
                }

                else
                {
                    return NotFound("Temos pena");
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromForm]Fatura fatura, int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:30504/api/faturas/" + id);

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Fatura>(client.BaseAddress, fatura);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(fatura);
        }

    }
}
