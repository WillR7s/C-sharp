using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Models;

namespace PrimeiroProjetoMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        //* A view index lê essa chave para exibir a mensagem
        ViewData["Mensagem"] = "Bem-vindo ao Sistema!";
        return View();
    }

//*Action que recebe parametro pela:/Home/Sobre/"Qualquer Numero"
public IActionResult Sobre(int? id)
{
    ViewBag.Versao = "1.0";
    ViewBag.ParametroId = id;
    return View();
        
}

//* Action Recebe parametro via query string: /Home/Saudacao?nome
 public IActionResult Saudacao(string? nome)
    {
        ViewData["Nome"] = nome ?? "Visitante";    //* Recebe o parametro nome e se "nome" for nulo ou não tiver nada o valor vai ser "Visitante"
        return View();
    }
   
    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
