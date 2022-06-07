using System;
using System.Collections;
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
    public class ProdutosController : Controller
    {
        Helper _api = new Helper();
        private readonly BD _context;

        public ProdutosController(BD context)
        {
            _context = context;
        }

        // Qualquer empregado pode ver produtos.
        // GET: teste_produtos
        public async Task<IActionResult> Index()
        {
            var bD = _context.Produtos.Include(p => p.Empregado);
            return View(await bD.ToListAsync());
        }

        // Qualquer empregado pode editar produtos.
        public async Task<ActionResult> Details(int Id)
        {
            var produto = new Produto();

            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync($"api/produtos/{Id}");

            if (response.IsSuccessStatusCode)
            {
                ViewData["ID_Empregado"] = new SelectList(_context.Empregados, "ID", "Nome");

                var resultado = response.Content.ReadAsStringAsync().Result;
                produto = JsonConvert.DeserializeObject<Produto>(resultado);
            }

            return View(produto);
        }

        // GET: produtos/Create
        public ActionResult Create()
        {
            ViewData["ID_Empregado"] = new SelectList(_context.Empregados, "ID", "Nome");
            return View();
        }

        // Qualquer empregado pode criar produtos.
        // POST: produtos/Create
        [HttpPost]
        public async Task<ActionResult> Create(Produto produto)
        {
            HttpClient client = _api.Initial();

            var post = await client.PostAsJsonAsync<Produto>("api/produtos", produto);
            ViewData["ID_Empregado"] = new SelectList(_context.Empregados, "ID", "Nome", produto.ID_Empregado);

            return RedirectToAction("Index");
        }

        // Método para eliminar produtos. Contudo, só o empregado que criou o produto o poderá fazer.
        public async Task<ActionResult> Delete(int Id)
        {
            var produto = new Produto();
            string userId = User.Identity.GetUserName();
            var empregados = _context.Produtos.Any(x => x.Empregado.EMail == userId && x.ID == Id);

            if (empregados != true)
            {
                return NotFound("Fica para a próxima.");
            }

            else
            {
                HttpClient client = _api.Initial();
                HttpResponseMessage response = await client.DeleteAsync($"api/produtos/{Id}");
            }
            return RedirectToAction("Index");
        }

        // Método para edita produtos. Contudo, só o empregado que criou o produto o poderá fazer.
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            string userId = User.Identity.GetUserName();
            Produto produtos = new Produto();

            using (var httpClient = new HttpClient())
            {
                Empregado emp = new Empregado();

                var empregados = _context.Produtos.Any(x => x.Empregado.EMail == userId && x.ID == Id);

                if (empregados == true)
                {

                    using (var response = await httpClient.GetAsync("http://localhost:30504/api/produtos/" + Id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        produtos = JsonConvert.DeserializeObject<Produto>(apiResponse);
                        ViewData["ID_Empregado"] = new SelectList(_context.Empregados, "ID", "Nome");
                    }
                    return View(produtos);     
                }
                                      
                else
                {
                    return NotFound("Temos pena");
                }
            }

        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromForm]Produto produto, int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:30504/api/produtos/" + id);

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Produto>(client.BaseAddress, produto);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }

            return View(produto);



        }

    }
}