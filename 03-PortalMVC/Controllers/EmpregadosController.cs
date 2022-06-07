using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using _01_DAL;
using _03_PortalMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _03_PortalMVC.Controllers
{
    public class EmpregadosController : Controller
    {
        Helper _api = new Helper();
        List<Empregado> emp = new List<Empregado>();

        public async Task<ActionResult> Index()
        {
           List<Empregado> empregado = new List<Empregado>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("api/empregados");

            if (response.IsSuccessStatusCode)
            {
                var resultado = response.Content.ReadAsStringAsync().Result;
                empregado = JsonConvert.DeserializeObject<List<Empregado>>(resultado);

            }

            return View(empregado);

        }

        //GET: Empregados/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            var empregado = new Empregado();

            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync($"api/empregados/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var resultado = response.Content.ReadAsStringAsync().Result;
                empregado = JsonConvert.DeserializeObject<Empregado>(resultado);
            }

            return View(empregado);
        }

        // GET: Empregados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empregados/Create
        [HttpPost]
        public async Task<ActionResult> Create(Empregado empregado)
        {
            HttpClient client = _api.Initial();
            var post = await client.PostAsJsonAsync<Empregado>("api/empregados", empregado);

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> Delete(int Id)
        {
            var empregado = new Empregado();

            if (empregado == null)
            {
                return NotFound("O empregado não foi encontrado.");
            }
            else
            {
                HttpClient client = _api.Initial();
                HttpResponseMessage response = await client.DeleteAsync($"api/empregados/{Id}");

            }
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Edit(int id)
        {
            Empregado empregados = new Empregado();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:30504/api/empregados/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    empregados = JsonConvert.DeserializeObject<Empregado>(apiResponse);
                }
            }
            return View(empregados);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromForm]Empregado empregado, int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:30504/api/empregados/" + id);

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Empregado>(client.BaseAddress, empregado);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(empregado);
        }

    }

  
}