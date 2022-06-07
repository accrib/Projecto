using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using _01_DAL;
using _03_PortalMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace _03_PortalMVC.Controllers
{
    public class Linhas_FaturaController : Controller
    {
        Helper _api = new Helper();

        public async Task<ActionResult> Index()
        {
            List<Linha_Fatura> fatura = new List<Linha_Fatura>();
            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync("api/linhas_fatura");

            if (response.IsSuccessStatusCode)
            {
                var resultado = response.Content.ReadAsStringAsync().Result;
                fatura = JsonConvert.DeserializeObject<List<Linha_Fatura>>(resultado);

            }

            return View(fatura);

        }


        //GET: linhas_fatura/Details/5

        public async Task<ActionResult> Details(int Id)
        {
            var fatura = new Linha_Fatura();

            HttpClient client = _api.Initial();
            HttpResponseMessage response = await client.GetAsync($"api/linhas_fatura/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var resultado = response.Content.ReadAsStringAsync().Result;
                fatura = JsonConvert.DeserializeObject<Linha_Fatura>(resultado);
            }

            return View(fatura);

        }


        // GET: linhas_fatura/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: linhas_fatura/Create
        [HttpPost]
        public async Task<ActionResult> Create(Linha_Fatura fatura)
        {
            HttpClient client = _api.Initial();
            var post = await client.PostAsJsonAsync<Linha_Fatura>("api/linhas_fatura", fatura);

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> Delete(int Id)
        {
            var linha_fatura = new Linha_Fatura();

            if (linha_fatura == null)
            {
                return NotFound("A linha_fatura não foi encontrada.");
            }
            else
            {
                HttpClient client = _api.Initial();
                HttpResponseMessage response = await client.DeleteAsync($"api/linhas_fatura/{Id}");

            }
            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Edit(int id)
        {
            Linha_Fatura linhas_fatura = new Linha_Fatura();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:30504/api/linhas_fatura/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    linhas_fatura = JsonConvert.DeserializeObject<Linha_Fatura>(apiResponse);
                }
            }
            return View(linhas_fatura);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromForm]Linha_Fatura linha_fatura, int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:30504/api/linhas_fatura/" + id);

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Linha_Fatura>(client.BaseAddress, linha_fatura);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(linha_fatura);
        }
    }
}