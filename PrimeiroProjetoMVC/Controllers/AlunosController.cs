using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrimeiroProjetoMVC.Models;
using PrimeiroProjetoMVC.ViewModels;

namespace PrimeiroProjetoMVC.Controllers
{
    // [Route("[controller]")]
    public class AlunosController : Controller
    {
        private static List<Aluno> _alunos = new()
        {
            new Aluno {Id =1, Nome = "Daniel Vorcaro", Email = "Contato.daniel.vorcaro@gmail.com", Idade = 43 },
            new Aluno {Id =1, Nome = "Diogo Chelles", Email = "pi.diogo.@gmail.com", Idade = 18 },
        };
       
       //GET/Alunos
        public IActionResult Index()
        {
            return View(_alunos);
        }
        public IActionResult Detalhes(int id)
        {
            var aluno= _alunos.FirstOrDefault(a => a.Id == id);

            if(aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }
        // GET/ Alunos /criar - exibir a view com todos os alunos
        [HttpGet]
         public IActionResult Criar()
        {
            return View();
        }
        
        //Post /Alunos/Criar - receber dados da View por meio de um formulario de Cadastro para adicionar um novo aluno
        [HttpPost]
        public IActionResult Criar(Aluno aluno)
        {
            if (!ModelState.IsValid)  // Verifica se as validações atribuidas na Model foram cumpridas
            {
                return View(aluno); // retorna com erros de validação
            }

            aluno.Id = _alunos.Any()? _alunos.Max(a=>a.Id)+1:1;
            _alunos.Add(aluno);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Buscar()
        {
            return View (new BuscaAlunoViewModel());

        }

        [HttpPost]
        public IActionResult Buscar(BuscaAlunoViewModel vm)
        {
            vm.Resultados = _alunos;  //Carrega a lista de Alunos na propriedade Resultados do Objeto vm


            if (string.IsNullOrEmpty(vm.TermoBusca))
            {
                vm.Resultados =_alunos.Where(a=> a.Nome.Contains(vm.TermoBusca!,StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return View(vm);
        }

    }
}