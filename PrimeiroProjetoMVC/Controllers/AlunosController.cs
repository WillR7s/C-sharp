using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrimeiroProjetoMVC.Models;

namespace PrimeiroProjetoMVC.Controllers
{
    [Route("[controller]")]
    public class AlunosController : Controller
    {
        private static List<Aluno> _alunos =new()
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

        [HttpGet]
         public IActionResult Criar(Aluno aluno)
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Criar(Aluno aluno)
        {
            
        }
    }
}