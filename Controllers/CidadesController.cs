using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjetoCidades.Models;
using ProjetoCidades.Repositorio;

namespace ProjetoCidades.Controllers
{
    public class CidadesController : Controller
    {
        Cidade cidade = new Cidade();
        CidadeRep cidadeRep = new CidadeRep();
        public IActionResult Index()
        {
            var lista = cidadeRep.Listar();
            return View(lista);
        }

        public IActionResult CidadesEstados()
        {
            return View();
        }

        public IActionResult TodosOsDados()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Alterar(int id)
        {
            var dados = cidadeRep.Listar().Where(c => c.Id == id).FirstOrDefault();
            return View(dados);
        }

        [HttpPost]
        public IActionResult Alterar(Cidade cidade)
        {
            var dados = cidadeRep.AlterarCidade(cidade);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Cadastrar([Bind] Cidade cidade)
        {
            cidadeRep.Cadastar(cidade);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            cidadeRep.Delete(id);
            return RedirectToAction("Index");
        }
    }
}